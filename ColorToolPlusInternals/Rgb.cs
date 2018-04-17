namespace ColorToolPlusInternals
{
    public readonly struct Rgb
    {
        public uint Red { get; }
        public uint Green { get; }
        public uint Blue { get; }
        public bool ValidColor { get; }

        public Rgb(uint red, uint green, uint blue) => (Red, Green, Blue, ValidColor) = (red, green, blue, false);
    }
}
