namespace AdventOfCode2024.Day12
{
    internal class GardenPlotNeighbors(GardenPlot? up, GardenPlot? down, GardenPlot? left, GardenPlot? right)
    {
        public GardenPlot? Up { get; } = up;
        public GardenPlot? Down { get; } = down;
        public GardenPlot? Left { get; } = left;
        public GardenPlot? Right { get; } = right;
    }
}
