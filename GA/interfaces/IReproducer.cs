using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public interface IReproducer<_Individual>
    {
        void Reproduce(IList<_Individual> parents,
            out IList<_Individual> children);
        void CreatePacks(IList<_Individual> population, int populationLimit,
            out IList<_Individual>[] packs);
    }
}