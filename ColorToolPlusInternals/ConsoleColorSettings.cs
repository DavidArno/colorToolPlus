using ConsolePlus;
using SuccincT.Options;

namespace ColorToolPlusInternals
{
    public readonly struct ConsoleColorSettings
    {
        public ConsoleColorSet ColorTable { get; }
        public Option<ConsoleColorIndexes> ScreenTextIndex { get; }
        public Option<ConsoleColorIndexes> ScreenBackgroundIndex { get; }
        public Option<ConsoleColorIndexes> PopupTextIndex { get; }
        public Option<ConsoleColorIndexes> PopupBackgroundIndex { get; }

        public ConsoleColorSettings(Rgb[] colorTable,
                                    Option<ConsoleColorIndexes> screenBackgroundIndex,
                                    Option<ConsoleColorIndexes> screenTextIndex,
                                    Option<ConsoleColorIndexes> popupBackgroundIndex,
                                    Option<ConsoleColorIndexes> popupTextIndex)
        {
            ColorTable = new ConsoleColorSet(colorTable);
            ScreenBackgroundIndex = screenBackgroundIndex;
            ScreenTextIndex = screenTextIndex;
            PopupBackgroundIndex = popupBackgroundIndex;
            PopupTextIndex = popupTextIndex;
        }
    }
}
