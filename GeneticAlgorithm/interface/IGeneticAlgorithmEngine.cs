using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    public interface IGeneticAlgorithmEngine<_Individuum>
    {
        bool Iterate(Func<bool> stopCondition);
        bool Stop { get; }
        IList<_Individuum> Population { get; }
    }
}
