namespace AdventOfCode2024.Day04
{
    internal static class XmasSearcher
    {
        private static readonly Dictionary<Direction, (int X, int Y)> AdjacentCoordinatesFromDirection = new()
        {
            { Direction.Up, (0, -1) },
            { Direction.UpRight, (1, -1) },
            { Direction.Right, (1, 0) },
            { Direction.RightDown, (1, 1) },
            { Direction.Down, (0, 1) },
            { Direction.DownLeft, (-1, 1) },
            { Direction.Left, (-1, 0) },
            { Direction.LeftUp, (-1, -1) },
        };

        public static IEnumerable<Xmas> Search(Matrix<char> matrix, int x, int y)
        {
            foreach (var pair in AdjacentCoordinatesFromDirection)
            {
                var direction = pair.Key;
                (var adjacentX, var adjacentY) = pair.Value;

                for (var i = 0; i < Xmas.Pattern.Length; i++)
                {
                    var characterX = x + adjacentX * i;
                    var characterY = y + adjacentY * i;

                    if (characterX < 0 || characterX >= matrix.Width || characterY < 0 || characterY >= matrix.Height)
                    {
                        break;
                    }

                    if (matrix[characterX, characterY] != Xmas.Pattern[i])
                    {
                        break;
                    }

                    if (i == Xmas.Pattern.Length - 1)
                    {
                        yield return new Xmas(x, y, direction);
                        break;
                    }
                }
            }
        }
    }
}
