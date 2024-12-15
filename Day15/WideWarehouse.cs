namespace AdventOfCode2024.Day15
{
    internal class WideWarehouse
    {
        public Pair RobotPosition { get; set; }
        public Dictionary<Pair, Pair> BoxLeftPositionFromPosition { get; } = [];
        public IReadOnlySet<Pair> WallPositions { get; }

        public WideWarehouse(Matrix<char> map)
        {
            RobotPosition = map
                .Where(tuple => tuple.Value == '@')
                .Select(tuple => new Pair(tuple.X * 2, tuple.Y))
                .Single();

            HashSet<Pair> wallPositions = [];

            foreach ((var character, var x, var y) in map)
            {
                var leftPosition = new Pair(x * 2, y);
                var rightPosition = new Pair(x * 2 + 1, y);

                if (character == 'O')
                {
                    BoxLeftPositionFromPosition.Add(leftPosition, leftPosition);
                    BoxLeftPositionFromPosition.Add(rightPosition, leftPosition);
                }
                else if (character == '#')
                {
                    wallPositions.Add(leftPosition);
                    wallPositions.Add(rightPosition);
                }
            }

            WallPositions = wallPositions;
        }
    }
}
