using System;
using System.Reflection;
using ColorToolPlusInternals;
using ConsolePlus;

namespace ColorToolPlus
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var assembly = typeof(Program).GetTypeInfo().Assembly;
            var version = assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
            var location = assembly.Location;
            Console.WriteLine(version);
            Console.WriteLine(location);

            var actions = new ColorToolActions(new ConcreteConsolePlus());
            actions.DrawCurrentColorTable();

            Console.WriteLine(
                $"Popup colours:{ConsoleColors.PopupBackgroundColor},{ConsoleColors.PopupForegroundColor}");

            ConsoleColors.PopupForegroundColor = ConsoleColor.Green;
            ConsoleColors.PopupBackgroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine(
                $"Popup colours:{ConsoleColors.PopupBackgroundColor},{ConsoleColors.PopupForegroundColor}");
        }
    }
}
