using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    public interface IChromosome<_Locus, _Gen, _Implt>
        where _Implt :  IChromosome<_Locus, _Gen, _Implt>
    {
        _Implt Concat(_Implt other);
        _Implt Substract(_Implt other);
        _Implt[] Split(_Locus index);
        _Implt Clone();
        _Implt Mix(/*IDictionary<_Locus, int> indices,*/ IList<_Implt> parents, IRandomGenerator<int> randomParent);

        _Gen this[_Locus index] { get; set; }
        ICollection<_Locus> Loci { get; }
        ICollection<_Gen> Gens { get; }
        IList<_Gen> Genome { get; }
    }
}
