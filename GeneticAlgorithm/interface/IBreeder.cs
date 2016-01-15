using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    public interface IFactory<_Args, _Type>
    {
        _Type CreateNew(_Args args);
        _Type CreateNew();
        _Args Args { get; set; }
        _Type Last { get; }
    }

    public interface IBreeder<_Chromosome, _Individuum> : IFactory<_Chromosome, _Individuum> { }
}
