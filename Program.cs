using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace scheduler
{
    static class Program
    {
        void Init(BazaDanychDataContext db, string semestr) 
        {
            var przedmioty = db.Przedmiots.Select(x=>x).Where(x=>x.semestr.Equals(semestr)).ToList();
            //
        }

        static void Main(string[] Args)
        {                                                                               
            DoubleRandomGenerator randMutation = new DoubleRandomGenerator();
            DoubleRandomGenerator randSelector = new DoubleRandomGenerator();
            IntegerRandomGenerator randGen = new IntegerRandomGenerator();
            IntegerRandomGenerator randMateUp = new IntegerRandomGenerator();
            IntegerRandomGenerator randGenSelector = new IntegerRandomGenerator();

            IMutation<MyChromosome, int> mutation = new MyMutation(randMutation, randGenSelector, 0.002);   
            IEvaluator<double, MyChromosome> evalSelector = new MyFitnessFunc();
                                                                                              
            IFactory<MyChromosome, MyChromosome> breeder = new MyBreeder(randGen, mutation);
            breeder.Last.Genome = "qwertyuiopasdfghjklzxcvbnm ".ToList();
            MyChromosome primeChromosome = breeder.CreateNew();


            IReproducer<MyChromosome> reproducer = new CrossOverReproducer<MyChromosome, int, char>(randMateUp, breeder, new CrossOverReproducer<MyChromosome, int, char>.Config() { ChildCount = 2, ParentCount = 2 });
            ISelector<MyChromosome> selector = new RouletSelector<MyChromosome, int, char>(evalSelector, randSelector); ;
            MyFitnessFunc fitness = new MyFitnessFunc();
            IList<MyChromosome> population = new List<MyChromosome>();

            for (int i = 0; i < 100; i++)
                population.Add(breeder.CreateNew());

            IGeneticAlgorithmEngine<MyChromosome> ga = new GeneticAlgorithmEngine<MyChromosome, int, char>(population, selector, reproducer, 20);
            int it = 0;
            Console.WriteLine("zaczawszy");
            while (ga.Iterate(() =>
            {
                Console.WriteLine(" iteracja nr {0} ", it++);

                foreach (var locus in ga.Population[0].Loci)
                {
                    Console.Write("{0}", ga.Population[0][locus]);

                }
                Console.WriteLine();

                return ga.Population.Select(x=> evalSelector.Evaluate(x)).Average() > 0.95;

            })) ;
            foreach (var item in ga.Population)
            {
                if (fitness.Evaluate(item) > 0.0f)
                {
                    Console.Write("{0} ", evalSelector.Evaluate(item));
                    foreach (var locus in item.Loci)
                    {
                        //Console.Write("{1} {0}", item[locus], evalSelector.Evaluate(item));
                        Console.Write("{0}", item[locus]);

                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("--------------end----------------");
            Console.ReadKey();
        }

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