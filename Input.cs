namespace AdventOfCode2024
{
    internal class Input(string text)
    {
        public string Text { get; } = text;
        public string[] Lines { get; } = text.Split('\n');
    }

    internal static class InputTransformationExtensions
    {
        public static Matrix<char> ToSquareMatrix(this Input input)
        {
            var height = input.Lines.Length;
            var width = input.Lines[0].Length;
            var matrix = new Matrix<char>(width, height);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    matrix[x, y] = input.Lines[y][x];
                }
            }

            return matrix;
        }
    }
}
