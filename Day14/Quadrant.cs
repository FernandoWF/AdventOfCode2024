namespace AdventOfCode2024.Day14;

internal record Quadrant()
{
    public required int StartingX { get; init; }
    public required int EndingX { get; init; }
    public required int StartingY { get; init; }
    public required int EndingY { get; init; }
}
