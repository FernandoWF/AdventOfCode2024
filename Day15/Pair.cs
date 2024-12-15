namespace AdventOfCode2024.Day15
{
    internal record Pair(int X, int Y)
    {
        public static Pair operator +(Pair left, Pair right)
        {
            return new Pair(left.X + right.X, left.Y + right.Y);
        }
    }
}
