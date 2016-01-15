using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{


    public class TimetableEvaluator : IEvaluator<double, Timetable>
    { //to do: evaluation tree
        public double Evaluate(Timetable item)
        {
            return 0.0;
        }
    }

    public class TimetablePopupationEvaluator : IEvaluator<bool, IList<Timetable>>
    {
        TimetableEvaluator _chromosomeEvaluator;

        public TimetablePopupationEvaluator(TimetableEvaluator chromosomeEvaluator)
        {
            _chromosomeEvaluator = chromosomeEvaluator;
        }

        public bool Evaluate(IList<Timetable> item)
        {
            var result = item.Average(x => _chromosomeEvaluator.Evaluate(x));
            return result >= 0.95;
        }
    }

    public class Timetable : IChromosome<TimetableLocus, Zajecia, Timetable>
    {                                                                                  
        private static IList<Zajecia> _genome;

        private IDictionary<TimetableLocus, Zajecia> _items;
        //private Zajecia[,] _items;

        private Zajecia _sentry;

        public Timetable()
        {
            //_items = new Zajecia[TimetableLocus.MaxTimeLength, TimetableLocus.MaxClassRoomCount];
            _items = new Dictionary<TimetableLocus, Zajecia>();
            _sentry = null;
        }

        public Timetable Concat(Timetable other)
        {
            //concant malych planow w duzy plan, z uwzglednieniem konfliktow
            //metoda : dodaj w pierwsze wolne miejsce
            //ogolnie metoda mozna by podawac jako delegate w konstruktorze (TO DO)
            var result = this.Clone();
            var orphants = new Queue<Zajecia>(_items.Count);
            foreach (var index in other._items.Keys)
            {
                var value = other._items[index];
                if (_items.ContainsKey(index))
                    orphants.Enqueue(other._items[index]);
                else
                    _items.Add(index, value);
            }
            SolveMajorConflicts(orphants);
            return result;
        }

        private void SolveMajorConflicts(Queue<Zajecia> orphants)
        {
            //pierwsze wolne miejsce  
            int time = 0, classRoom = 0;
            while (orphants.Count > 0)  //co gdy skonczy sie miejsce w planie, natomiast wciaz beda jakies sieroty?
            {
                var item = orphants.Dequeue();
                var found = false;

                for (; !found &&  time < TimetableLocus.MaxTimeLength; time++)
                {
                    for (; classRoom < TimetableLocus.MaxClassRoomCount; classRoom++)
                    {
                        var locus = new TimetableLocus(time, classRoom);
                        if (_items[locus] == null)
                        {
                            _items[locus] = item;
                            found = true;
                        }
                    }
                }
            }
        }

        private void SolveMinorConflicts(Queue<Zajecia> orphants, ICollection<TimetableLocus> accomodation)
        {
            //pierwsza wolna sposrod 
            foreach (var locus in accomodation)
                if (orphants.Count > 0)
                    _items[locus] = orphants.Dequeue();
                else
                    break;
        }

        public Timetable Substract(Timetable other)
        {
            throw new NotImplementedException();
        }

        public Timetable[] Split(TimetableLocus index)
        {
            throw new NotImplementedException();
        }

        public Timetable Clone()
        {
            var result = new Timetable();
            foreach (var index in _items.Keys)
                result._items.Add(index, _items[index]);
            return result;
        }

        public Timetable Mix(IList<Timetable> parents, IRandomGenerator<int> randomParent)
        {
            //mix malych planow
                   
            var parenthood = new Dictionary<Zajecia,Timetable>(); //wybor od ktorego rodzica bedzie pobierany gen  
            var loci = new HashSet<TimetableLocus>();


            //zadecydowac, ktory rodzic jest wlascicielem genu
            foreach (var gen in _items.Values)
            {
                var randParent = randomParent.Next();
                parenthood.Add(gen, parents[randParent]);
            }
                             
            //wiem z ktorego rodzica, ale nie wiem na ktorej pozycji jest dany gen
            //zatem lepiej dla kazdego rodzica zrobic liste genow ktore chce z niego pobrac... 
            //przejsc przez wsyzstkie mozliwe miejsca i wydostac gdzie jest dany gen (czyli trzeba umiec sprawdzic)
                                                     

            //wszystkie pozycje, na ktorych moga pojawic sie geny
            _items.Clear(); //zeby nie byc czyims klonem!!
            foreach (var parent in parents)
                foreach (var locus in parent._items.Keys)  
                {                
                    if(parent == parenthood[parent[locus]])
                    {
                        var gen = parent[locus];
                        //tzn ze to jest szukany locus!!! dla parenthood[parent[locus]]
                        //powinienem go po prostu dodac... z tym ze jezeli bedzie klon ktoregos rodzica, to trzeba pomyslec nad ... dodaj/usun
                        _items[locus] = gen;
                        parenthood.Remove(gen);
                    }  
                    else
                        loci.Add(locus); 
                }

            var orphants = new Queue<Zajecia>(parenthood.Keys);

            SolveMinorConflicts(orphants, loci);
          
            //teoretycznie mam o czynienia z klonem, ktoregos rodzica...mozna to wykorzystac do optymalizacji ?
            //skleic powyzsze
            //wyrzucic z obecnego wszystko to, co nie pasuje

            return this;
        }

        public Zajecia this[TimetableLocus index]
        {
            get
            {
                Zajecia item;
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
                Zajecia item;
                if (_items.TryGetValue(index, out item))
                    _items[index] = value;
            }
        }

        public ICollection<TimetableLocus> Loci
        {
            get { return _items.Keys; }
        }

        public ICollection<Zajecia> Gens
        {
            get { return _items.Values; }
        }

        public IList<Zajecia> Genome
        {
            get { return _genome; }
            set { _genome = value; }
        }
    }
}
