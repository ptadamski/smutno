using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace scheduler
{
    static class Program
    {
        const int MaxPopulationCount = 100;

        static void InitializeFromDataBase(BazaDanychDataContext db,
            string semestr,
            out IList<Zajecia> genotyp,
            out IDictionary<Przedmiot, IList<Prowadzacy>> prowadzacyZajecia)
        {                    
            prowadzacyZajecia = new Dictionary<Przedmiot, IList<Prowadzacy>>();

            //filtr przedmiotow
            var przedmiotyWSemestrze = db.Przedmiots.Select(x => x).Where(x => x.semestr.Equals(semestr)).ToList();

            //ustalenie granic dla locus
            var wszystkieSale = db.Salas.Select(x => x).ToList();
            TimetableLocus.SetBoundry(8 * 14, wszystkieSale.Count);

            //genotyp = (from przydzial in db.Przypisany_przedmiots
            //         join prowadzacy in db.Prowadzacies on przydzial.id_prowadzacego equals prowadzacy.id
            //         join przedmiot in przedmiotyWSemestrze on przydzial.id_przedmiotu equals przedmiot.id
            //         join grupa in db.Grupas on new {przedmiot.kierunek, przedmiot.rok} equals new { grupa.kierunek, grupa.rok }
            //         select new Zajecia() {Grupa=grupa, Prowadzacy=prowadzacy, Przedmiot=przedmiot} ).ToList();   


            //do ustalenia genotypu
            var listaObligatoryjnychPrzedmiotow = (from przedmiot in db.Przedmiots
                       join grupa in db.Grupas on new { przedmiot.rok, przedmiot.kierunek } equals new { grupa.rok, grupa.kierunek }
                       select new { Grupa = grupa, Przedmiot = przedmiot});
                                     
            genotyp = new List<Zajecia>();
            foreach (var e in listaObligatoryjnychPrzedmiotow)              
            {                                       
                for (int i = 0,length =  e.Przedmiot.cwiczenia/15; i < length; i++)
                    genotyp.Add(new Zajecia(){Typ=TypZajec.Cwiczenia, Grupa = e.Grupa, Przedmiot = e.Przedmiot, Prowadzacy=null, Index=i});     

                for (int i = 0, length = e.Przedmiot.laboratoria/15; i < length; i++)    
                    genotyp.Add(new Zajecia(){Typ=TypZajec.Laboratoria, Grupa = e.Grupa, Przedmiot = e.Przedmiot, Prowadzacy=null, Index=i});     
            }       
            

            //do mutacji                   
            var prowadzacyWszyscy = db.Prowadzacies.Select(x => x);
            foreach (var e in przedmiotyWSemestrze)
            {
                var przypisaneZajecia = (from przydzial in db.Przypisany_przedmiots 
                                         where przydzial.Przedmiot.Equals(e.id)
                                         select przydzial.Prowadzacy).ToList();
                prowadzacyZajecia.Add(e, przypisaneZajecia);
            }

        }

        static void Main(string[] Args)
        {                                        
            //---pobranie danych z bazy danych---    
            var db = new BazaDanychDataContext();                          
            IDictionary<Przedmiot, IList<Prowadzacy>> prowadzacyZajecia;
            IList<Zajecia> genotyp;

            InitializeFromDataBase(db, "zimowy", out genotyp, out prowadzacyZajecia);
                              
            //---przypisanie genotypu---
            Timetable.Genome = genotyp;

            //---randomy---
            DoubleRandomGenerator randMutation = new DoubleRandomGenerator();
            DoubleRandomGenerator randSelector = new DoubleRandomGenerator();
            IntegerRandomGenerator randGen = new IntegerRandomGenerator();
            IntegerRandomGenerator randMateUp = new IntegerRandomGenerator();
            IntegerRandomGenerator randGenSelector = new IntegerRandomGenerator();  
   
                        
            //---komponenty algorytmu---                                               
            IList<Timetable> population = new List<Timetable>();
            IMutation<Timetable, TimetableLocus> mutation = new TimetableTeacherMutation(prowadzacyZajecia, randMutation, randGenSelector, 0.002);
            IFactory<Timetable, Timetable> breeder = new TimetableFactory(mutation, randMateUp);    
            IEvaluator<double, Timetable> evalSelector = new TimetableEvaluator();

            //Timetable primeChromosome = breeder.CreateNew();

            IReproducer<Timetable> reproducer = new CrossOverReproducer<Timetable, TimetableLocus, Zajecia>(randMateUp, breeder, new CrossOverReproducer<Timetable, TimetableLocus, Zajecia>.Config() { ChildCount = 2, ParentCount = 2 });
            ISelector<Timetable> selector = new RouletSelector<Timetable, TimetableLocus, Zajecia>(evalSelector, randSelector);

            //---inicjacja populacji---  (calosciowy plan dla uczelni)
            for (int i = 0; i < MaxPopulationCount; i++)
                population.Add(breeder.CreateNew());


            //moze zrobic tak...
            //populacje duzych planow rozpic na N populacjie mniejszych planow

            //dla kazdego duzego planu
                //duzy plan uzyskany z populacji rozbic na mniejsze juz teraz
                    //stworzyc populacje pomiejszych planow
                    //przeprowadzic iteracje GA
                //potem wszystkie te plany skonkatenowac 
                //odtworzyc populacje duzych planow
                //i ocenic...


            do
            {
                //wydzielenie podzbiorow            
                var planyGrup = new Dictionary<Grupa, IList<Timetable>>();

                foreach (var osobnik in population)
                {

                    var lista_grup = osobnik.Gens.Select(x => x.Grupa).Distinct();
                    foreach (var grupa in lista_grup)
                    {
                        var planGrupy = new Timetable(mutation);

                        IList<TimetableLocus> loci =
                            (from locus in osobnik.Loci
                             where osobnik[locus].Grupa.Equals(grupa)
                             select locus).ToList();

                        foreach (var locus in loci)
                            planGrupy[locus] = osobnik[locus];

                        IList<Timetable> lista;
                        if (!planyGrup.TryGetValue(grupa, out lista))
                            lista = new List<Timetable>();

                        lista.Add(planGrupy);
                    }
                }

                var listaGrup = planyGrup.Keys.ToList();

                for (int i = 0; i < listaGrup.Count; i++)
                {
                    IGeneticAlgorithmEngine<Timetable> ga_grupy;
                    ga_grupy = new GeneticAlgorithmEngine<Timetable, TimetableLocus, Zajecia>(planyGrup[listaGrup[i]], selector, reproducer, 20);
                    ga_grupy.Iterate(() =>
                    {
                        //planGrupy.Value = ga_grupy.Population;
                        return ga_grupy.Population.Select(x => evalSelector.Evaluate(x)).Average() > 0.95;
                    });
                    planyGrup[listaGrup[i]] = ga_grupy.Population;
                }

                //polaczenie podzbiorow
                var nowaPopulacja = new List<Timetable>();
                for (int i = 0; i < population.Count; i++)
                {
                    var osobnik = new Timetable(mutation);
                    foreach (var planGrupy in planyGrup)
                        osobnik.Concat(planGrupy.Value[i]);
                }
            } while (population.Select(x => evalSelector.Evaluate(x)).Average() < 0.95);

        }

        //static void Main(string[] Args)
        //{                                                                               
        //    DoubleRandomGenerator randMutation = new DoubleRandomGenerator();
        //    DoubleRandomGenerator randSelector = new DoubleRandomGenerator();
        //    IntegerRandomGenerator randGen = new IntegerRandomGenerator();
        //    IntegerRandomGenerator randMateUp = new IntegerRandomGenerator();
        //    IntegerRandomGenerator randGenSelector = new IntegerRandomGenerator();

        //    IMutation<MyChromosome, int> mutation = new MyMutation(randMutation, randGenSelector, 0.002);   
        //    IEvaluator<double, MyChromosome> evalSelector = new MyFitnessFunc();
                                                                                              
        //    IFactory<MyChromosome, MyChromosome> breeder = new MyBreeder(randGen, mutation);
        //    breeder.Last.Genome = "qwertyuiopasdfghjklzxcvbnm ".ToList();
        //    MyChromosome primeChromosome = breeder.CreateNew();


        //    IReproducer<MyChromosome> reproducer = new CrossOverReproducer<MyChromosome, int, char>(randMateUp, breeder, new CrossOverReproducer<MyChromosome, int, char>.Config() { ChildCount = 2, ParentCount = 2 });
        //    ISelector<MyChromosome> selector = new RouletSelector<MyChromosome, int, char>(evalSelector, randSelector); ;
        //    MyFitnessFunc fitness = new MyFitnessFunc();
        //    IList<MyChromosome> population = new List<MyChromosome>();

        //    for (int i = 0; i < 100; i++)
        //        population.Add(breeder.CreateNew());

        //    IGeneticAlgorithmEngine<MyChromosome> ga = new GeneticAlgorithmEngine<MyChromosome, int, char>(population, selector, reproducer, 20);
        //    int it = 0;
        //    Console.WriteLine("zaczawszy");
        //    while (ga.Iterate(() =>
        //    {
        //        Console.WriteLine(" iteracja nr {0} ", it++);

        //        foreach (var locus in ga.Population[0].Loci)
        //        {
        //            Console.Write("{0}", ga.Population[0][locus]);

        //        }
        //        Console.WriteLine();

        //        return ga.Population.Select(x=> evalSelector.Evaluate(x)).Average() > 0.95;

        //    })) ;
        //    foreach (var item in ga.Population)
        //    {
        //        if (fitness.Evaluate(item) > 0.0f)
        //        {
        //            Console.Write("{0} ", evalSelector.Evaluate(item));
        //            foreach (var locus in item.Loci)
        //            {
        //                //Console.Write("{1} {0}", item[locus], evalSelector.Evaluate(item));
        //                Console.Write("{0}", item[locus]);

        //            }
        //            Console.WriteLine();
        //        }
        //    }

        //    Console.WriteLine("--------------end----------------");
        //    Console.ReadKey();
        //}

        class MyBreeder : IFactory<MyChromosome, MyChromosome>
        {
            private MyChromosome _args;
            private MyChromosome _last;
            private IRandomGenerator<int> _randGen;
            private IMutation<MyChromosome, int> _mutation;

            public MyBreeder(IRandomGenerator<int> randGen, IMutation<MyChromosome,int> mutation)
            {                                                                                   
                _randGen = randGen;
                _mutation = mutation;
                _last = _args = new MyChromosome(new Dictionary<int, char>(), '\0', _mutation);
            }

            public MyChromosome CreateNew()
            {
                //randomly
                var item = _args.Clone();
                for (int i = 0; i < 35; i++)
                    _mutation.Mutate(item, i);
                return _last = item;
            }

            public MyChromosome CreateNew(MyChromosome args)
            {
                //clone
                return _last = args.Clone();
            }

            public MyChromosome Args
            {
                get { return _args; }
                set { _args = value; }
            }

            public MyChromosome Last { get { return _last; } }
        }

        class MyMutation : IMutation<MyChromosome, int>
        {
            private IRandomGenerator<double> _randMutationRate;
            private IRandomGenerator<int> _randGenSelector;
            private double _mutationSuccessRate;

            public MyMutation(IRandomGenerator<double> randMutationRate,
                IRandomGenerator<int> randGenSelector, double mutationSuccessRate)
            {
                _randMutationRate = randMutationRate;
                _randGenSelector = randGenSelector;
                _mutationSuccessRate = mutationSuccessRate;
            }

            public void Mutate(MyChromosome chromosome, int locus)
            {
                chromosome[locus] = chromosome.Genome[_randGenSelector.Next(chromosome.Genome.Count)];
            }   
  
            public void TryMutate(MyChromosome chromosome, int locus)
            {
                if (_randMutationRate.Next() <= _mutationSuccessRate)
                    Mutate(chromosome, locus);
            }

            public double SuccessRate
            {
                get { return _mutationSuccessRate; }
            }
        }

        class MyChromosome : IChromosome<int, char, MyChromosome>
        {
            protected static IList<char> _genome = new List<char>();

            protected IDictionary<int, char> _items;
            protected char _sentry;
            protected IMutation<MyChromosome, int> _mutation;              

            public MyChromosome(IDictionary<int, char> items, char sentry, IMutation<MyChromosome, int> mutation)
            {
                _items = items;
                _sentry = sentry;
                _mutation = mutation;
            }

            public char this[int index]
            {
                get
                {
                    char item;
                    if (_items.TryGetValue(index, out item))
                    {
                        return item;
                    }
                    else
                    {
                        return _sentry;
                    }
                }
                set
                {
                    char item;
                    if (_items.TryGetValue(index, out item))
                        _items[index] = value;
                    else
                        _items.Add(index, value);
                }
            }

            public MyChromosome Concat(MyChromosome other)
            {
                MyChromosome result = this;
                foreach (var index in other._items.Keys)
                    _items.Add(index, other._items[index]);
                return result ;
            }

            public MyChromosome Substract(MyChromosome other)
            {
                MyChromosome result = this;
                foreach (var index in other._items.Keys)
                    if (!other._items.ContainsKey(index))
                        result._items.Add(index, other._items[index]);
                return result;
            }

            public MyChromosome[] Split(int index)
            {
                throw new NotImplementedException();
            }

            public MyChromosome Clone()
            {
                MyChromosome result = new MyChromosome(new Dictionary<int,char>(), _sentry, _mutation);
                foreach (var index in _items.Keys)
                    result._items.Add(index, _items[index]);
                return result;
            }
                                    
            public ICollection<int> Loci
            {
                get { return _items.Keys; }
            }

            public ICollection<char> Gens
            {
                get { return _items.Values; }
            }
                          
            public MyChromosome Mix(/*IDictionary<int, int> indices,*/ IList<MyChromosome> parents, IRandomGenerator<int> randomParent)
            {
                for (int i = 0; i < _items.Count; i++)
                {
                    _items[i] = parents[randomParent.Next(parents.Count)][i];
                    _mutation.TryMutate(this, i);
                }
                return this;
            }

            public IList<char> Genome
            {
                get { return _genome; }
                set { _genome = value; }
            }
        }

        //class int
        //{
        //    public int()
        //    {
        //        Value = 0;
        //    }

        //    public int(int x)
        //    {
        //        Value = x;
        //    }

        //    public int Value { get; set; }

        //    public int Add(int other)
        //    {
        //        Value += other.Value;
        //        return this; ;
        //    }

        //    public int Sub(int other)
        //    {
        //        Value -= other.Value;
        //        return this; ;
        //    }

        //    public int Mul(int other)
        //    {
        //        Value *= other.Value;
        //        return this; ;
        //    }

        //    public int Div(int other)
        //    {
        //        Value /= other.Value;
        //        return this; ;
        //    }

        //    public int CompareTo(int other)
        //    {
        //        return Value.CompareTo(other.Value);
        //    }

        //    public override bool Equals(object x)
        //    {
        //        return ((int)x).Value == Value;
        //    }

        //    public override int GetHashCode()
        //    {
        //        return Value + 1;
        //    }
        //}

        class MyFitnessFunc : IEvaluator<double, MyChromosome>
        {
            public double Evaluate(MyChromosome item)
            {
                //zawiera podciag "ala" 
                float result = 0.0f;
                var chromosome = item;
                for (int i = 0, length = chromosome.Loci.Count; i < length - 2; i++)
                {
                    if (chromosome[i] == 'a') result += 0.01f;
                    if (chromosome[i + 1] == 'l') result += 0.01f;
                    if (chromosome[i + 2] == 'a') result += 0.01f;
                    if (chromosome[i] == 'a' && chromosome[i + 1] == 'l' && chromosome[i + 2] == 'a') result = 1.0f;
                }
                return result;
            }
        }

        //static void Main(string[] Args)
        //{
        //    IList<Timetable> population = new List<Timetable>();
        //    IRandomGenerator<double> rand_Selector = new DoubleRandomGenerator();
        //    IRandomGenerator<int> rand_Reproducer = new IntegerRandomGenerator();
        //    IEvaluator<bool, IList<Timetable>> eval_GA = null;
        //    IEvaluator<double, Timetable> eval_Chromosome = null;
        //    IFactory<Timetable, Timetable> chromosome_Factory = new TimetableFactory();
        //    IReproducer<Timetable> reproducer = new CrossOverReproducer<Timetable, Pozycja, Zajecia>(rand_Reproducer, chromosome_Factory, new CrossOverReproducer<Timetable, Pozycja, Zajecia>.Config() { ChildCount = 2, ParentCount = 2 });
        //    ISelector<Timetable> selector = new RouletSelector<Timetable, Pozycja, Zajecia>(eval_Chromosome, rand_Selector);

        //    var GA_eng = new GeneticAlgorithmEngine<Timetable, Pozycja, Zajecia>(population, selector, reproducer);
        //    GA_eng.Iterate(() =>
        //    {
        //        return eval_GA.Evaluate(GA_eng.Population);
        //    });

        //    //Process.Start("c:\\");
        //    //Console.WriteLine("Calculator started, please press RETURN key to continue...");
        //    Console.ReadLine();
        //}
    }

}