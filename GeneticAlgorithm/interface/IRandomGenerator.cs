using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    interface IRandomGenerator<_T>
    {
        _T Next();
        _T Next(_T hi);
        _T Next(_T lo, _T hi);
    }
}
