using System.Collections;

namespace AdventOfCode2024
{
    internal class Matrix<T>(int width, int height) : IEnumerable<(T Value, int X, int Y)>
    {
        private readonly T[,] values = new T[width, height];

        public int Width { get; } = width;
        public int Height { get; } = height;

        public T this[int x, int y]
        {
            get => values[x, y];
            set => values[x, y] = value;
        }

        public IEnumerator<(T Value, int X, int Y)> GetEnumerator()
        {
            for (var y = 0; y < Height; y++)
            {
                for (var x = 0; x < Width; x++)
                {
                    yield return (values[x, y], x, y);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
