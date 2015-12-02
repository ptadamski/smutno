using System;

namespace GeneticAlgorithm
{
    public interface IFitnessFunc<_Chromosome>
    {
        float Fit(_Chromosome chromosome);
    }
}