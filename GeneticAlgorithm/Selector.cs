using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    class Boundry : IComparer<Boundry>
    {
        public Boundry(double lowerBoundry, double upperBoundry)
        {
            LowerBoundry = lowerBoundry;
            UpperBoundry = upperBoundry;
        }

        public double LowerBoundry { get; set; }
        public double UpperBoundry { get; set; }

        public int Compare(Boundry x, Boundry y)
        {
            if (x.UpperBoundry.CompareTo(y.LowerBoundry) < 0) // x<=y
                return -1;
            else if (y.UpperBoundry.CompareTo(x.LowerBoundry) < 0) // y<=x
                return 1;
            else //x~y
                return 0;
        }
    }

    /// <summary>
    /// Mechanizm wybierajacy najlepsze plany zajec
    /// </summary>
    class RouletSelector<_Chromosome, _Locus, _Gen> : ISelector<_Chromosome>
        where _Chromosome : IChromosome<_Locus, _Gen, _Chromosome>
    {
        private IEvaluator<double, _Chromosome> _evalator;
        private static IRandomGenerator<double> _randomizer;

        public RouletSelector(IEvaluator<double, _Chromosome> evaluator, IRandomGenerator<double> randomizer)
        {
            _evalator = evaluator;
            _randomizer = randomizer;
        }

        public void Select(out IList<_Chromosome> individuals, int count, IList<_Chromosome> fromPopulation)
        {
            if (fromPopulation.Count==0)
                throw new Exception("Empty population.");

            var little_const = 1.0 / (fromPopulation.Count * fromPopulation.Count);    
            var eval_list = fromPopulation.Select(x => _evalator.Evaluate(x) + little_const).ToList();
            var eval_sum = eval_list.Sum();

            var aggr_list = new List<Boundry>();

            var last_value = 0.0;
            for (int i = 0; i < fromPopulation.Count; i++)
			{
                aggr_list.Add(new Boundry(last_value, last_value + (eval_list[i] / eval_sum)));
                last_value = aggr_list[i].UpperBoundry;
			}     

            individuals = new List<_Chromosome>();
                                         
            for (int i = 0; i < count; i++)
            {              
                var random = _randomizer.Next();        
                var bondry = new Boundry(random,random); 
                var index = aggr_list.BinarySearch(bondry, bondry);
                individuals.Add(fromPopulation[index]);
            }
        }
    }
}
