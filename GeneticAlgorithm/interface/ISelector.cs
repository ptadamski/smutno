using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    public interface ISelector<_Chromosome>
    {
        void Select(out IList<_Chromosome> individuals, int count, IList<_Chromosome> fromPopulation);
    }
}
