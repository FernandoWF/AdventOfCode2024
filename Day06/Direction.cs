namespace AdventOfCode2024.Day06
{
    internal enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    internal static class DirectionExtensions
    {
        public static Direction TurnClockwise(this Direction direction)
        {
            const int DirectionCount = 4;
            var nextDirectionNumber = ((int)direction) + 1;

            return (Direction)(nextDirectionNumber % DirectionCount);
        }
    }
}
