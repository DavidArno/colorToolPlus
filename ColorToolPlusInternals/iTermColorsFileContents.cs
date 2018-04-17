using System;
using System.Collections.Generic;
using ConsolePlus;

namespace ColorToolPlusInternals
{
    // ReSharper disable once InconsistentNaming - following file extension convention rather than C# conventions here
    internal class iTermColorsFileContents
    {
        private readonly Rgb[] _consoleColors = new Rgb[16];
        private readonly Dictionary<string, Action<Rgb>> _colorSetters;

        internal iTermColorsFileContents()
        {
            _colorSetters = new Dictionary<string, Action<Rgb>>
            {
                ["Ansi 0 Color"] = rgb => SetConsoleColor(ConsoleColor.Black, rgb),
                ["Ansi 4 Color"] = rgb => SetConsoleColor(ConsoleColor.DarkBlue, rgb),
                ["Ansi 2 Color"] = rgb => SetConsoleColor(ConsoleColor.DarkGreen, rgb),
                ["Ansi 6 Color"] = rgb => SetConsoleColor(ConsoleColor.DarkCyan, rgb),
                ["Ansi 1 Color"] = rgb => SetConsoleColor(ConsoleColor.DarkRed, rgb),
                ["Ansi 5 Color"] = rgb => SetConsoleColor(ConsoleColor.DarkMagenta, rgb),
                ["Ansi 3 Color"] = rgb => SetConsoleColor(ConsoleColor.DarkYellow, rgb),
                ["Ansi 7 Color"] = rgb => SetConsoleColor(ConsoleColor.Gray, rgb),
                ["Ansi 8 Color"] = rgb => SetConsoleColor(ConsoleColor.DarkGray, rgb),
                ["Ansi 12 Color"] = rgb => SetConsoleColor(ConsoleColor.Blue, rgb),
                ["Ansi 10 Color"] = rgb => SetConsoleColor(ConsoleColor.Green, rgb),
                ["Ansi 14 Color"] = rgb => SetConsoleColor(ConsoleColor.Cyan, rgb),
                ["Ansi 9 Color"] = rgb => SetConsoleColor(ConsoleColor.Red, rgb),
                ["Ansi 13 Color"] = rgb => SetConsoleColor(ConsoleColor.Magenta, rgb),
                ["Ansi 11 Color"] = rgb => SetConsoleColor(ConsoleColor.Yellow, rgb),
                ["Ansi 15 Color"] = rgb => SetConsoleColor(ConsoleColor.White, rgb),
                ["Background Color"] = rgb => BackGroundColor = rgb,
                ["Bold Color"] = rgb => BoldColor = rgb,
                ["Cursor Color"] = rgb => CursorColor = rgb,
                ["Cursor Text Color"] = rgb => CursorTextColor = rgb,
                ["Foreground Color"] = rgb => ForeGroundColor = rgb,
                ["Selected Text Color"] = rgb => SelectedTextColor = rgb,
                ["Selection Color"] = rgb => SelectionColor = rgb
            };
        }

        internal Rgb this[ConsoleColorIndexes color] => _consoleColors[(int)color];

        internal Rgb BackGroundColor { get; private set; }
        internal Rgb BoldColor { get; private set; }
        internal Rgb CursorColor { get; private set; }
        internal Rgb CursorTextColor { get; private set; }
        internal Rgb ForeGroundColor { get; private set; }
        internal Rgb SelectedTextColor { get; private set; }
        internal Rgb SelectionColor { get; private set; }

        internal bool TrySetColor(string colorName, Rgb rgb, Action reportError)
        {
            if (_colorSetters.ContainsKey(colorName))
            {
                _colorSetters[colorName](rgb);
                return true;
            }

            reportError();
            return false;
        }
        private void SetConsoleColor(ConsoleColor color, Rgb rgb) => _consoleColors[(int)color] = rgb;
    }
}
