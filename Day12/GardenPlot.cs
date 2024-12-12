namespace AdventOfCode2024.Day12
{
    internal record GardenPlot(char PlantType, Position Position)
    {
        public GardenPlotNeighbors GetNeighbors(IReadOnlyDictionary<GardenPlot, HashSet<GardenPlot>> sameTypeNeighborsByGardenPlot)
        {
            var neighbors = sameTypeNeighborsByGardenPlot[this];

            var possibleUpNeighbor = new GardenPlot(PlantType, Position with { Y = Position.Y - 1 });
            neighbors.TryGetValue(possibleUpNeighbor, out var upNeighbor);

            var possibleDownNeighbor = new GardenPlot(PlantType, Position with { Y = Position.Y + 1 });
            neighbors.TryGetValue(possibleDownNeighbor, out var downNeighbor);

            var possibleLeftNeighbor = new GardenPlot(PlantType, Position with { X = Position.X - 1 });
            neighbors.TryGetValue(possibleLeftNeighbor, out var leftNeighbor);

            var possibleRightNeighbor = new GardenPlot(PlantType, Position with { X = Position.X + 1 });
            neighbors.TryGetValue(possibleRightNeighbor, out var rightNeighbor);

            return new GardenPlotNeighbors(upNeighbor, downNeighbor, leftNeighbor, rightNeighbor);
        }
    }
}
