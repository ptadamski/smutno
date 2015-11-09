using System;

namespace Algebra
{
    public interface IArithmetic<T> : IEquatable<T>, IComparable<T>
    {
        T Add(T other);
        T Sub(T other);
        T Mul(T other);
        T Div(T other);
    }
}
