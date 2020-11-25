using System;
using System.Numerics;

namespace EasyGraphics.Fractals.Interfaces
{
    public interface IFractal : IDisposable
    {
        ushort[] CreatePixelArray(
            Complex bottomLeft,
            Complex topRight,
            int numPointsWide,
            int numPointsHigh,
            int maxIterations);
    }
}
