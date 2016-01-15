using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    interface IMutation<_Chromosome, _Locus>
    {
        void Mutate(_Chromosome chromosome, _Locus locus);
        void TryMutate(_Chromosome chromosome, _Locus locus);
        double SuccessRate { get; }
    }
}
