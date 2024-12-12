namespace AdventOfCode2024.Day12;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        var farm = Farm.Parse(input);

        return farm.Regions
            .Select(region =>
            {
                var area = region.Count;

                const int SidesInAGardenPlot = 4;
                var perimeter = region
                    .Select(gardenPlot => SidesInAGardenPlot - farm.SameTypeNeighborsByGardenPlot[gardenPlot].Count)
                    .Sum();

                return area * perimeter;
            })
            .Sum();
    }

    public static object RunPart2(Input input)
    {
        var farm = Farm.Parse(input);

        return farm.Regions
            .Select(region =>
            {
                var area = region.Count;
                var sides = region
                    .Select(gardenPlot =>
                    {
                        var neighbors = gardenPlot.GetNeighbors(farm.SameTypeNeighborsByGardenPlot);

                        var hasUpNeighbor = neighbors.Up is not null;
                        var upNeighborNeighbors = neighbors.Up?.GetNeighbors(farm.SameTypeNeighborsByGardenPlot);
                        var upNeighborHasLeftNeighbor = upNeighborNeighbors?.Left is not null;
                        var upNeighborHasRightNeighbor = upNeighborNeighbors?.Right is not null;

                        var hasDownNeighbor = neighbors.Down is not null;

                        var hasLeftNeighbor = neighbors.Left is not null;
                        var leftNeighborNeighbors = neighbors.Left?.GetNeighbors(farm.SameTypeNeighborsByGardenPlot);
                        var leftNeighborHasUpNeighbor = leftNeighborNeighbors?.Up is not null;
                        var leftNeighborHasDownNeighbor = leftNeighborNeighbors?.Down is not null;

                        var hasRightNeighbor = neighbors.Right is not null;

                        var considerUpSide = !hasUpNeighbor && (!hasLeftNeighbor || leftNeighborHasUpNeighbor);
                        var considerDownSide = !hasDownNeighbor && (!hasLeftNeighbor || leftNeighborHasDownNeighbor);
                        var considerLeftSide = !hasLeftNeighbor && (!hasUpNeighbor || upNeighborHasLeftNeighbor);
                        var considerRightSide = !hasRightNeighbor && (!hasUpNeighbor || upNeighborHasRightNeighbor);

                        return (considerUpSide ? 1 : 0)
                            + (considerDownSide ? 1 : 0)
                            + (considerLeftSide ? 1 : 0)
                            + (considerRightSide ? 1 : 0);
                    })
                    .Sum();

                return area * sides;
            })
            .Sum();
    }
}
