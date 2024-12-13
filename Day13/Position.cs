namespace AdventOfCode2024.Day13
{
    internal record Position(long X, long Y)
    {
        public static Position operator +(Position left, Position right)
        {
            return new Position(left.X + right.X, left.Y + right.Y);
        }

        public static Position operator *(Position left, long factor)
        {
            return new Position(left.X * factor, left.Y * factor);
        }
    }
}
