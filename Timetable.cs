using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{    
    public class Timetable : IChromosome<TimetableLocus, Zajecia, Timetable>
    {                                                                                  
        private static IList<Zajecia> _genome;

        private IDictionary<TimetableLocus, Zajecia> _items;
        private IMutation<Timetable, TimetableLocus> _mutation;
        private Zajecia _sentry;

        public Timetable(IDictionary<TimetableLocus, Zajecia> items, IMutation<Timetable, TimetableLocus> mutation)
        {
            _items = items;
            _mutation = mutation;
            _sentry = null;
        }

        public Timetable(IMutation<Timetable, TimetableLocus> mutation)
        {
            _items = new Dictionary<TimetableLocus, Zajecia>();
            _mutation = mutation;
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

            if (orphants.Count>0)
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

        private void SolveMinorConflicts(Queue<Zajecia> orphants, ICollection<TimetableLocus> accomodation, out IList<TimetableLocus> usedAccomodation)
        {
            usedAccomodation = new List<TimetableLocus>();
            //pierwsza wolna 
            foreach (var locus in accomodation)
                if (orphants.Count > 0)
                {
                    _items[locus] = orphants.Dequeue();
                    usedAccomodation.Add(locus);
                }
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
            var result = new Timetable(_mutation);
            foreach (var index in _items.Keys)
                result._items.Add(index, _items[index]);
            return result;
        }

        public Timetable Mix(IList<Timetable> parents, IRandomGenerator<int> randomParent)
        {                     
            //mix malych planow

            var parenthood = new Dictionary<Zajecia, Timetable>(); //wybor od ktorego rodzica bedzie pobierany gen  
            var freeLoci = new HashSet<TimetableLocus>();

            //zadecydowac, ktory rodzic jest wlascicielem genu
            foreach (var gen in _items.Values)
            {
                var randParent = randomParent.Next(parents.Count);
                parenthood.Add(gen, parents[randParent]);
            }

            //wiem z ktorego rodzica, ale nie wiem na ktorej pozycji jest dany gen
            //zatem lepiej dla kazdego rodzica zrobic liste genow ktore chce z niego pobrac... 
            //przejsc przez wsyzstkie mozliwe miejsca i wydostac gdzie jest dany gen (czyli trzeba umiec sprawdzic)

            _items.Clear(); //zeby nie byc czyims klonem!!
            var dodaneZajecia = new List<Zajecia>();
            //wyszukiwanie jakie locus zajmuje dany gen w chromosomie rodzica
            foreach (var parent in parents)
                foreach (var locus in parent._items.Keys)
                {
                    if (parent == parenthood[parent[locus]])
                    {
                        var gen = parent[locus];
                        //tzn ze to jest szukany locus!!! dla parenthood[parent[locus]]
                        //powinienem go po prostu dodac... z tym ze jezeli bedzie klon ktoregos rodzica, to trzeba pomyslec nad ... dodaj/usun
                        _items[locus] = gen;
                        _mutation.TryMutate(this, locus); //mutacje - tam gdzie nie ma konfliktow   
                        dodaneZajecia.Add(gen);

                        //parenthood.Remove(gen);
                    }
                    else //konflikt
                        freeLoci.Add(locus);
                }

            
            var orphants = new Queue<Zajecia>(parenthood.Keys.Except(dodaneZajecia));
            IList<TimetableLocus> usedLoci = new List<TimetableLocus>();

            SolveMinorConflicts(orphants, freeLoci, out usedLoci);

            ////mutacje - tam gdzie wystapily konflikty
            foreach (var locus in usedLoci)
                _mutation.TryMutate(this, locus);

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
                else
                    _items.Add(index, value);
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

        public static IList<Zajecia> Genome
        {
            get { return _genome; }
            set { _genome = value; }
        }

        IList<Zajecia> IChromosome<TimetableLocus, Zajecia, Timetable>.Genome
        {
            get { return _genome; }
        }

    }
}
