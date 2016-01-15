using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scheduler
{
    class DoubleRandomGenerator : IRandomGenerator<double>
    {
        private Random _random = new Random();

        public double Next()
        {
            return _random.NextDouble();
        }

        public double Next(double hi)
        {
            return _random.NextDouble();
        }

        public double Next(double lo, double hi)
        {
            return _random.NextDouble();
        }
    }

    class IntegerRandomGenerator : IRandomGenerator<int>
    {
        private Random _random = new Random();

        public int Next()
        {
            return _random.Next();
        }

        public int Next(int hi)
        {
            return _random.Next(hi);
        }

        public int Next(int lo, int hi)
        {
            return _random.Next(lo,hi);
        }
    }

}
