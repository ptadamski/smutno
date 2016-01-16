using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    public class GeneticAlgorithmEngine<_Chromosome, _Locus, _Gen> : IGeneticAlgorithmEngine<_Chromosome>
        where _Chromosome : IChromosome<_Locus, _Gen, _Chromosome>
    {
        ISelector<_Chromosome> _selector;
        IReproducer<_Chromosome> _reproducer;
        IList<_Chromosome> _population;
        int _populationLimit;
        bool _stop;

        public GeneticAlgorithmEngine(IList<_Chromosome> population, ISelector<_Chromosome> selector,
            IReproducer<_Chromosome> reproducer, int populationLimit)
        {
            _selector = selector;
            _reproducer = reproducer;
            _population = population;
            _populationLimit = populationLimit;
        }

        public bool Iterate(Func<bool> stopCondition)
        {
            _stop = stopCondition();
            IList<_Chromosome> individuals;     
                                      
            if (!_stop)
            {
                _selector.Select(out individuals, _populationLimit, _population);
                _reproducer.Reproduce(out _population,  individuals, _population.Count);
            }

            return !_stop;
        }

        public bool Stop
        {
            get { return _stop; }
        }

        public IList<_Chromosome> Population
        {
            get { return _population; }
        }
    }
}
