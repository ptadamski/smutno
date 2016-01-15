using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    public interface IEvaluator<_R, _T>
    {
        _R Evaluate(_T item);
    }
}
