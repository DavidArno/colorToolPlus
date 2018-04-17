using System;

namespace ConsolePlus
{
    public class ConcreteConsolePlus : IConsolePlus
    {
        private ConsoleColor _savedBackgroundColor = Console.BackgroundColor;
        private ConsoleColor _savedForegroundColor = Console.ForegroundColor;

        public ConsoleColorIndexes BackgroundColor
        {
            get => (ConsoleColorIndexes)Console.BackgroundColor;
            set => Console.BackgroundColor = (ConsoleColor)value;
        }

        public ConsoleColorIndexes ForegroundColor
        {
            get => (ConsoleColorIndexes)Console.ForegroundColor;
            set => Console.ForegroundColor = (ConsoleColor)value;
        }

        public void NewLine() => Console.Write("\n");

        public void SaveBackgroundColor() => _savedBackgroundColor = Console.BackgroundColor;

        public void RestoreBackgroundColor() => Console.BackgroundColor = _savedBackgroundColor;

        public void SaveForegroundColor() => _savedForegroundColor = Console.ForegroundColor;

        public void RestoreForegroundColor() => Console.ForegroundColor = _savedForegroundColor;

        public void Write(string text) => Console.Write(text);

        public void WriteLine(string text) => Console.WriteLine(text);

        public void WriteError(string text) => ColoredWrite(ConsoleColor.Red, text);

        public void WriteErrorLine(string text) => ColoredWriteLine(ConsoleColor.Red, text);

        public void WriteInfo(string text) => ColoredWrite(ConsoleColor.Green, text);

        public void WriteInfoLine(string text) => ColoredWriteLine(ConsoleColor.Green, text);

        public void WriteWarning(string text) => ColoredWrite(ConsoleColor.Yellow, text);

        public void WriteWarningLine(string text) => ColoredWriteLine(ConsoleColor.Yellow, text);

        private static void ColoredWrite(ConsoleColor color, string text)
        {
            var savedForeground = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.Write(text);
            Console.ForegroundColor = savedForeground;
        }

        private static void ColoredWriteLine(ConsoleColor color, string text)
        {
            var savedForeground = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = savedForeground;
        }
    }
}
