namespace AdventOfCode2024.Day04
{
    internal static class X_MasSearcher
    {
        public static bool Search(Matrix<char> matrix, int x, int y)
        {
            var leftX = x - 1;
            var rightX = x + 1;
            var topY = y - 1;
            var bottomY = y + 1;

            if (leftX < 0 || rightX >= matrix.Width || topY < 0 || bottomY >= matrix.Height)
            {
                return false;
            }

            if (matrix[x, y] != 'A')
            {
                return false;
            }

            var topLeftCharacter = matrix[leftX, topY];
            var topRightCharacter = matrix[rightX, topY];
            var bottomLeftCharacter = matrix[leftX, bottomY];
            var bottomRightCharacter = matrix[rightX, bottomY];

            var firstDiagonalContainsWord = (topLeftCharacter == 'M' && bottomRightCharacter == 'S')
                || (topLeftCharacter == 'S' && bottomRightCharacter == 'M');
            var secondDiagonalContainsWord = (topRightCharacter == 'M' && bottomLeftCharacter == 'S')
                || (topRightCharacter == 'S' && bottomLeftCharacter == 'M');

            return firstDiagonalContainsWord && secondDiagonalContainsWord;
        }
    }
}
