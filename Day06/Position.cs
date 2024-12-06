namespace AdventOfCode2024.Day06
{
    internal record Position(int X, int Y);

    internal record DirectedPosition(int X, int Y, Direction Direction)
    {
        public Position WithoutDirection()
        {
            return new Position(X, Y);
        }
    }
}
