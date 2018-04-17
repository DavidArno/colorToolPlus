namespace ColorToolPlusInternals
{
    public static class RgbExtensions
    {
        public static uint ToColorTableValue(this Rgb rgb) => rgb.Red + (rgb.Green << 8) + (rgb.Blue << 16);

        public static Rgb ToRgb(this uint colorTableValue)
        {
            var red = colorTableValue & 0x0000FF;
            var green = (colorTableValue & 0x00FF00) >> 8;
            var blue = (colorTableValue & 0xFF0000) >> 16;
            return new Rgb(red, green, blue);
        }

        public static Rgb ToRgb(this (double red, double green, double blue) color) =>
            new Rgb(FractionColorToUInt(color.red), FractionColorToUInt(color.green), FractionColorToUInt(color.blue));

        private static uint FractionColorToUInt(double color) => (uint)(255 * color);
    }
}
