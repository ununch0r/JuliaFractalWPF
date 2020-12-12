using System.Linq;
using System.Numerics;
using EasyGraphics.Fractals.Interfaces;
using MathNet.Numerics;

namespace EasyGraphics.Fractals.FractalStrategies
{
    public class JuliaSet : IFractal
    {
        public ushort[] CreatePixelArray(
            Complex bottomLeft,
            Complex topRight,
            int numPointsWide,
            int numPointsHigh,
            int maxIterations)
        {
            var realValues = Generate.LinearSpaced(numPointsWide, bottomLeft.Real, topRight.Real);
            var imaginaryValues = Generate.LinearSpaced(numPointsHigh, bottomLeft.Imaginary, topRight.Imaginary);

            var pixels =
                from imaginary in imaginaryValues
                from real in realValues
                let pt = new Complex(real, imaginary)
                select BeginsToDivergeAt(pt, maxIterations);

            return pixels.ToArray();
        }

        public void Dispose()
        {
        }

        private static ushort BeginsToDivergeAt(Complex z, int maxIterations)
        {
            foreach (var iterations in Enumerable.Range(0, maxIterations))
            {
                z = 1 / Complex.Tan(z* z);
                if (z.Real * z.Real + z.Imaginary * z.Imaginary >= 4d) return (ushort)iterations;
            }

            return (ushort)maxIterations;
        }
    }
}