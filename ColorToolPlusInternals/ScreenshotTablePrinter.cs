// https://github.com/mbadolato/iTerm2-Color-Schemes/blob/master/tools/screenshotTable.sh

using System;
using System.Collections.Generic;
using System.Linq;
using ConsolePlus;

namespace ColorToolPlusInternals
{
    internal class ScreenshotTablePrinter
    {
        private const string ScreenShotTableTestString = " gYw ";

        private readonly IConsolePlus _console;

        private readonly List<(Func<ConsoleColorIndexes> color, string sequence)> _bgColorsAndEscapeSequences;

        private readonly List<(Func<ConsoleColorIndexes> color, string sequence)> _colorRowsAndEscapeSequences;

        internal ScreenshotTablePrinter(IConsolePlus console)
        {
            _console = console;
            _bgColorsAndEscapeSequences = new List<(Func<ConsoleColorIndexes>, string)>
            {
                (() => _console.BackgroundColor, "    "),
                (() => ConsoleColorIndexes.DarkBlack, " 40m"),
                (() => ConsoleColorIndexes.DarkRed, " 41m"),
                (() => ConsoleColorIndexes.DarkGreen, " 42m"),
                (() => ConsoleColorIndexes.DarkYellow, " 43m"),
                (() => ConsoleColorIndexes.DarkBlue, " 44m"),
                (() => ConsoleColorIndexes.DarkMagenta, " 45m"),
                (() => ConsoleColorIndexes.DarkCyan, " 46m"),
                (() => ConsoleColorIndexes.DarkWhite, " 47m")
            };

            _colorRowsAndEscapeSequences = new List<(Func<ConsoleColorIndexes>, string)>
            {
                (() => _console.ForegroundColor, "m"),
                (() => ConsoleColorIndexes.BrightWhite, "1m"),
                (() => ConsoleColorIndexes.DarkBlack, "30m"),
                (() => ConsoleColorIndexes.BrightBlack, "1;30m"),
                (() => ConsoleColorIndexes.DarkRed, "31m"),
                (() => ConsoleColorIndexes.BrightRed, "1;31m"),
                (() => ConsoleColorIndexes.DarkGreen, "32m"),
                (() => ConsoleColorIndexes.BrightGreen, "1;32m"),
                (() => ConsoleColorIndexes.DarkYellow, "33m"),
                (() => ConsoleColorIndexes.BrightYellow, "1;33m"),
                (() => ConsoleColorIndexes.DarkBlue, "34m"),
                (() => ConsoleColorIndexes.BrightBlue, "1;34m"),
                (() => ConsoleColorIndexes.DarkMagenta, "35m"),
                (() => ConsoleColorIndexes.BrightMagenta, "1;35m"),
                (() => ConsoleColorIndexes.DarkCyan, "36m"),
                (() => ConsoleColorIndexes.BrightCyan, "1;36m"),
                (() => ConsoleColorIndexes.DarkWhite, "37m"),
                (() => ConsoleColorIndexes.BrightWhite, "1;37m")
            };
        }

        public void DrawTable()
        {
            _console.SaveBackgroundColor();
            _console.SaveForegroundColor();

            DrawBackgroundEscapeSequenceTitleRow();

            foreach (var (color, sequence) in _colorRowsAndEscapeSequences)
            {
                _console.RestoreBackgroundColor();
                _console.RestoreForegroundColor();

                _console.Write($"{sequence}\t");
                _console.ForegroundColor = color();

                foreach (var column in _bgColorsAndEscapeSequences)
                {
                    _console.BackgroundColor = column.color();
                    _console.Write($" {ScreenShotTableTestString} ");
                    _console.RestoreBackgroundColor();
                    _console.Write(" ");
                }

                _console.NewLine();
            }

            _console.NewLine();
            _console.RestoreBackgroundColor();
            _console.RestoreForegroundColor();
        }

        private void DrawBackgroundEscapeSequenceTitleRow() => _console.WriteLine($"\t{FormatLine()}");

        private string FormatLine() => 
            _bgColorsAndEscapeSequences.Aggregate("", (current, column) => current + $" {column.sequence}   ");
    }
}
