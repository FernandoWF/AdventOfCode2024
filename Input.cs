namespace AdventOfCode2024
{
    internal class Input(string text)
    {
        public string Text { get; } = text;
        public string[] Lines { get; } = text.Split('\n');
    }

    internal static class InputTransformationExtensions
    {
        public static Matrix<char> ToRectangularMatrix(this Input input)
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

        public static Matrix<int> ToRectangularIntegerMatrix(this Input input)
        {
            var height = input.Lines.Length;
            var width = input.Lines[0].Length;
            var matrix = new Matrix<int>(width, height);

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    matrix[x, y] = (int)char.GetNumericValue(input.Lines[y][x]);
                }
            }

            return matrix;
        }
    }
}
