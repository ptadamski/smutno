using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    /// <summary>
    /// Sekwencja zajec w planie zajec
    /// na jakiej pozycji jest zadany gen
    /// </summary>
    class Chromosome<_Locus, _Gen> : IChromosome<_Locus, _Gen, Chromosome<_Locus, _Gen>>
    {
        protected IDictionary<_Locus, _Gen> _items;
        protected _Gen _sentry;

        public Chromosome(_Gen sentry)
        {
            _items = new Dictionary<_Locus, _Gen>();
            _sentry = default(_Gen);
        }

        public Chromosome(IDictionary<_Locus, _Gen> items, _Gen sentry)
        {
            _items = items;
            _sentry = sentry;
        }

        public _Gen this[_Locus index]
        {
            get
            {
                _Gen item;
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
                _Gen item;
                if (_items.TryGetValue(index, out item))
                    _items[index] = value;
            }
        }

        public Chromosome<_Locus, _Gen> Concat(Chromosome<_Locus, _Gen> other)
        {
            Chromosome<_Locus, _Gen> result = this.Clone();
            foreach (var index in other._items.Keys)
                _items.Add(index, other._items[index]);
            return result;
        }

        public Chromosome<_Locus, _Gen> Substract(Chromosome<_Locus, _Gen> other)
        {
            Chromosome<_Locus, _Gen> result = this.Clone();
            foreach (var index in other._items.Keys)
                if (!other._items.ContainsKey(index))
                    result._items.Add(index, other._items[index]);
            return result;
        }

        public Chromosome<_Locus, _Gen>[] Split(_Locus index)
        {
            throw new NotImplementedException();
        }

        public Chromosome<_Locus, _Gen> Clone()
        {
            Chromosome<_Locus, _Gen> result = new Chromosome<_Locus, _Gen>(_sentry);
            foreach (var index in _items.Keys)
                result._items.Add(index, _items[index]);
            return result;
        }

        public Chromosome<_Locus, _Gen> Mix(/*IDictionary<_Locus, int> indices,*/ IList<Chromosome<_Locus, _Gen>> parents, IRandomGenerator<int> randomParent)
        {
            //Chromosome<_Locus, _Gen> result = this.Clone();
            //foreach (var index in indices.Keys)
            //{
            //    if (_items.ContainsKey(index))
            //        result._items[index] = parents[indices[index]][index];
            //    else
            //        result._items.Add(index, parents[indices[index]][index]);
            //}
            //return result;         
            throw new NotImplementedException();
        }

        public ICollection<_Locus> Loci
        {
            get { return _items.Keys; }
        }

        public ICollection<_Gen> Gens
        {
            get { return _items.Values; }
        }

        public IList<_Gen> Genome
        {
            get { throw new NotImplementedException(); }
        }
    }
}


