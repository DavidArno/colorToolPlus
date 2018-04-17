using ConsolePlus;

namespace ColorToolPlusInternals
{
    public class ColorToolActions
    {
        private readonly IConsolePlus _console;

        public ColorToolActions(IConsolePlus console) => _console = console;

        public void DrawCurrentColorTable()
        {
            var printer = new ScreenshotTablePrinter(_console);
            printer.DrawTable();
        }
    }
}
