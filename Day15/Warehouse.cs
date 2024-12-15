namespace AdventOfCode2024.Day15
{
    internal class Warehouse(Matrix<char> map)
    {
        public Pair RobotPosition { get; set; } = map
            .Where(tuple => tuple.Value == '@')
            .Select(tuple => new Pair(tuple.X, tuple.Y))
            .Single();

        public HashSet<Pair> BoxPositions { get; } = map
            .Where(tuple => tuple.Value == 'O')
            .Select(tuple => new Pair(tuple.X, tuple.Y))
            .ToHashSet();

        public IReadOnlySet<Pair> WallPositions { get; } = map
            .Where(tuple => tuple.Value == '#')
            .Select(tuple => new Pair(tuple.X, tuple.Y))
            .ToHashSet();
    }
}
