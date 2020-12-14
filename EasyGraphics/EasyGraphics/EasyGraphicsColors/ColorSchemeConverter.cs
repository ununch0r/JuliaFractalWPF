using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace EasyGraphics.EasyGraphicsColors
{
    public class ColorSchemeConverter
    {
        public static void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public static Color ColorFromHSV(double hue, double saturation, double value)
        {
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static double[] RgbToCmyk(byte red, byte green, byte blue)
        {
            var black = (Math.Min(1.0 - red / 255.0, Math.Min(1.0 - green / 255.0, 1.0 - blue / 255.0)));
            var roundedBlack = RoundValue(black);

            var cyan = (1.0 - (red / 255.0) - black) / (1.0 - black);
            var roundedCyan = RoundValue(cyan);

            var magenta = (1.0 - (green / 255.0) - black) / (1.0 - black);
            var roundedMagenta = RoundValue(magenta);

            var yellow = (1.0 - (blue / 255.0) - black) / (1.0 - black);
            var roundedYellow = RoundValue(yellow);

            return new[] { roundedCyan, roundedMagenta, roundedYellow, roundedBlack};
        }

        public static double RoundValue(double value, int precision = 0)
        {
            value *= 100;
            var roundedValue = Math.Round(value, precision);
            return roundedValue;
        }
    }
}
