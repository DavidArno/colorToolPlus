using System;
using ConsolePlus;

namespace ColorToolPlusInternals
{
    public struct ConsoleColorSet
    {
        private readonly Rgb[] _colors;

        public ConsoleColorSet(Rgb[] colors)
        {
            if (colors.Length != 16)
                throw new ArgumentException($"Expected 16 colors; got {colors.Length}", nameof(colors));

            _colors = colors;
        }

        public Rgb this[ConsoleColorIndexes color] => _colors[(int)color];
    }
}
