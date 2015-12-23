using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public interface IGeneticAlgorithm<_Individual>
    {
        bool Iterate(Func<bool> force);
        void Select(IList<_Individual> population, IList<float> fitnessFactors, 
            out IList<_Individual> selectedPopulation);
        void Populate(IList<_Individual> oldPopulation, int populationLimit, 
            out IList<_Individual> newPopulation);
        bool Evaluate(IList<_Individual> population,
            out IList<float> fitnessFactors, 
            out float fitnessOverall);
        bool Stop { get; }
        IList<_Individual> Population { get; }
        float Fitness { get; }
    }
}
