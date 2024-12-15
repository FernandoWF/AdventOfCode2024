using System.Diagnostics;

namespace AdventOfCode2024.Day15
{
    internal enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    internal static class DirectionExtensions
    {
        private static readonly Pair UpMovementOffset = new(0, -1);
        private static readonly Pair DownMovementOffset = new(0, 1);
        private static readonly Pair LeftMovementOffset = new(-1, 0);
        private static readonly Pair RightMovementOffset = new(1, 0);

        public static Pair GetMovementOffset(this Direction direction)
        {
            return direction switch
            {
                Direction.Up => UpMovementOffset,
                Direction.Down => DownMovementOffset,
                Direction.Left => LeftMovementOffset,
                Direction.Right => RightMovementOffset,
                _ => throw new UnreachableException("All possible directions should have been handled.")
            };
        }
    }
}
