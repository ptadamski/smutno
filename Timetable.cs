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

    public class Pozycja
    {
    }

    public class Timetable : IChromosome<Pozycja, Zajecia, Timetable>
    {                                                                                  
        private static IList<Zajecia> _genome;
        public delegate Pozycja TOnConflict(Timetable sender, Pozycja locus, Zajecia gen);

        public TOnConflict OnConflict { get; set; }

        private IDictionary<Pozycja, Zajecia> _items;
        private Zajecia _sentry;                

        public Timetable Concat(Timetable other)
        {
            var result = this.Clone();
            foreach (var index in other._items.Keys)
            {
                var value = other._items[index];
                if (_items.ContainsKey(index))          
                    _items[OnConflict(this, index, value)] = value;
                else                              
                    _items.Add(index, value);
            }
            return result;
        }
                          
        public Timetable Substract(Timetable other)
        {
            throw new NotImplementedException();
        }

        public Timetable[] Split(Pozycja index)
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

        public Timetable Mix(/*IDictionary<Pozycja, int> indices,*/ IList<Timetable> parents, IRandomGenerator<int> randomParent)
        {
            //Timetable result = this;
            //foreach (var index in indices.Keys)
            //{
            //    if (_items.ContainsKey(index))
            //        result._items[index] = parents[indices[index]][index];
            //    else
            //        result._items.Add(index, parents[indices[index]][index]);
            //}
            return this;
        }

        public Zajecia this[Pozycja index]
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

        public ICollection<Pozycja> Loci
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
