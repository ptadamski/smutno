using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public interface ISelector<_Individual>
    {
        void Select(IList<_Individual> population, IList<float> fitnessFactors, int populationLimit,
            out IList<_Individual> selectedPopulation);
    }
}