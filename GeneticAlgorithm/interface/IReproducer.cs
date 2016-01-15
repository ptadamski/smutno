using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    public interface IReproducer<_Chromosome>
    {
        void PairUp(IList<_Chromosome> individuals, out IDictionary<int, IList<_Chromosome>> intoMates);
        void Reproduce(out IList<_Chromosome> newPopulation, IList<_Chromosome> fromIndividuals, int uptoCount);
    }
}
