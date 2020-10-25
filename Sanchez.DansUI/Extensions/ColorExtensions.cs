using System.Drawing;

namespace Sanchez.DansUI.Extensions
{
    public static class ColorExtensions
    {
        public static Color ChangeBrightness(this Color color, float correctionFactor)
        {
            var red = (float)color.R;
            var green = (float)color.G;
            var blue = (float)color.B;

            if (correctionFactor < 0)
            {
                correctionFactor += 1;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        }
        public static Color Lighten(this Color color, float lightness) => color.ChangeBrightness(lightness);
        public static Color Darken(this Color color, float darkness) => color.ChangeBrightness(darkness * -1);

        public static Color Mix(this Color color, Color mixer) => Color.FromArgb((color.R + mixer.R) / 2, (color.G + mixer.G) / 2, (color.B + mixer.B) / 2);
        public static Color Mix(this Color color, Color mixer, float mixAmount)
        {
            var inverse = 1 - mixAmount;
            var r = (inverse * color.R + mixAmount * mixer.R) / 2;
            var g = (inverse * color.G + mixAmount * mixer.G) / 2;
            var b = (inverse * color.B + mixAmount * mixer.B) / 2;
            return Color.FromArgb((int)r, (int)g, (int)b);
        }

        public static Color SetOpacity(this Color color, float opacity)
        {
            return Color.FromArgb((int)(opacity * 100), color);
        }

        public static string ToHexString(this Color color) => string.Format("rgba({0}, {1}, {2}, {3})", color.R, color.G, color.B, color.A / 100f);

        public static float Luminance(this Color color) => (0.2126f * color.R) + (0.7152f * color.G) + (0.0722f * color.B);
        public static bool IsDarkColor(this Color color) => color.Luminance() < 140;

        public static Color RelativeBrightness(this Color color, float correctionFactor)
        {
            if (color.IsDarkColor())
                return color.Lighten(correctionFactor);
            else return color.Darken(correctionFactor);
        }

        public static Color GetTextColoring(this Color color)
        {
            if (color.IsDarkColor())
                return Color.White;
            return Color.Black;
        }
    }
}
