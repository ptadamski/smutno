using System;

namespace GeneticAlgorithm
{
    public interface IFactory<_T, _A>
    {
        _T Create();
        // _T Create(_T copy);
        _T Create(_A args);

        _A Args { get; set; }
        _T Last { get; }
    }
}