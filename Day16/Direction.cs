namespace AdventOfCode2024.Day16
{
    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    };

    internal static class DirectionExtensions
    {
        public static int GetRotationCost(this Direction direction, Direction destinationDirection)
        {
            const int RotationCost = 1000;

            if (direction == destinationDirection) { return 0; }
            if (direction == Direction.Up && destinationDirection == Direction.Down
                || direction == Direction.Down && destinationDirection == Direction.Up
                || direction == Direction.Left && destinationDirection == Direction.Right
                || direction == Direction.Right && destinationDirection == Direction.Left)
            {
                return RotationCost * 2;
            }

            return RotationCost;
        }
    }
}
