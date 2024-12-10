namespace AdventOfCode2024.Day10;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        var topographicMap = input.ToRectangularIntegerMatrix();

        return topographicMap
            .Where(tuple => tuple.Value == 0)
            .Select(tuple => GetTrailheadPeaks(0, tuple.X, tuple.Y, topographicMap).Count)
            .Sum();
    }

    private static HashSet<Position> GetTrailheadPeaks(int height, int x, int y, Matrix<int> topographicMap)
    {
        if (height == 9) { return [new Position(x, y)]; }

        var peaksWhenGoingUp = y - 1 >= 0 && topographicMap[x, y - 1] == height + 1
            ? GetTrailheadPeaks(height + 1, x, y - 1, topographicMap)
            : [];
        var peaksWhenGoingDown = y + 1 < topographicMap.Height && topographicMap[x, y + 1] == height + 1
            ? GetTrailheadPeaks(height + 1, x, y + 1, topographicMap)
            : [];
        var peaksWhenGoingLeft = x - 1 >= 0 && topographicMap[x - 1, y] == height + 1
            ? GetTrailheadPeaks(height + 1, x - 1, y, topographicMap)
            : [];
        var peaksWhenGoingRight = x + 1 < topographicMap.Width && topographicMap[x + 1, y] == height + 1
            ? GetTrailheadPeaks(height + 1, x + 1, y, topographicMap)
            : [];

        var peaksReached = peaksWhenGoingUp;
        peaksReached.UnionWith(peaksWhenGoingDown);
        peaksReached.UnionWith(peaksWhenGoingLeft);
        peaksReached.UnionWith(peaksWhenGoingRight);

        return peaksReached;
    }

    public static object RunPart2(Input input)
    {
        var topographicMap = input.ToRectangularIntegerMatrix();

        return topographicMap
            .Where(tuple => tuple.Value == 0)
            .Select(tuple => GetTrails(0, tuple.X, tuple.Y, [], topographicMap).Count)
            .Sum();
    }

    private static List<List<Position>> GetTrails(int height, int x, int y, List<Position> currentTrail, Matrix<int> topographicMap)
    {
        List<Position> newTrail = [.. currentTrail, new Position(x, y)];

        if (height == 9) { return [newTrail]; }

        var trailsWhenGoingUp = y - 1 >= 0 && topographicMap[x, y - 1] == height + 1
            ? GetTrails(height + 1, x, y - 1, newTrail, topographicMap)
            : [];
        var trailsWhenGoingDown = y + 1 < topographicMap.Height && topographicMap[x, y + 1] == height + 1
            ? GetTrails(height + 1, x, y + 1, newTrail, topographicMap)
            : [];
        var trailsWhenGoingLeft = x - 1 >= 0 && topographicMap[x - 1, y] == height + 1
            ? GetTrails(height + 1, x - 1, y, newTrail, topographicMap)
            : [];
        var trailsWhenGoingRight = x + 1 < topographicMap.Width && topographicMap[x + 1, y] == height + 1
            ? GetTrails(height + 1, x + 1, y, newTrail, topographicMap)
            : [];

        var trails = trailsWhenGoingUp;
        trails.AddRange(trailsWhenGoingDown);
        trails.AddRange(trailsWhenGoingLeft);
        trails.AddRange(trailsWhenGoingRight);

        return trails;
    }

    private record Position(int X, int Y);
}
