namespace AdventOfCode2024
{
    internal class Input(string text)
    {
        public string Text { get; } = text;
        public string[] Lines { get; } = text.Split('\n');
    }
}
