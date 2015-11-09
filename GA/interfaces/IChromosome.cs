using System;
using System.Collections.Generic;

namespace GeneticAlgorithm
{
    public interface IChromosome<_Locus, _Gen> : ICloneable
    {
        void Concatenate(IList<_Locus> loci, IList<_Gen> gens);
        void Concatenate(IChromosome<_Locus, _Gen> chromosome);
        void Split(_Locus locus, 
            out IChromosome<_Locus, _Gen> chromosome);
        void Mutate(_Locus locus);
        void Mutate(IList<_Locus> loci);
        void Populate(IList<_Locus> loci, _Gen sentry);
        void Mix(ICollection<_Locus> loci, IList<IChromosome<_Locus, _Gen>> genePool, IList<int> indices);

        ICollection<_Locus> Loci { get; }
        _Gen this[_Locus index] { get; set; }
    }
}