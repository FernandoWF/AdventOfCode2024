namespace AdventOfCode2024.Day14;

internal record Robot()
{
    public required Pair Position { get; set; }
    public required Pair Velocity { get; init; }

    public bool IsInQuadrant(Quadrant quadrant)
    {
        return Position.X >= quadrant.StartingX
            && Position.X <= quadrant.EndingX
            && Position.Y >= quadrant.StartingY
            && Position.Y <= quadrant.EndingY;
    }
}
