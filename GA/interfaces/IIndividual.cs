using System;

namespace GeneticAlgorithm
{
    public interface IIndividual<_Chromosome> : ICloneable
    {
        _Chromosome Chromosome { get; set; }
    }
}