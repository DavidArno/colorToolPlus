namespace ConsolePlus
{
    public interface IConsolePlus
    {
        ConsoleColorIndexes BackgroundColor { get; set; }
        ConsoleColorIndexes ForegroundColor { get; set; }

        void NewLine();

        void SaveBackgroundColor();
        void RestoreBackgroundColor();

        void SaveForegroundColor();
        void RestoreForegroundColor();

        void Write(string text);
        void WriteLine(string text);

        void WriteError(string text);
        void WriteErrorLine(string text);

        void WriteInfo(string text);
        void WriteInfoLine(string text);

        void WriteWarning(string text);
        void WriteWarningLine(string text);
    }
}
