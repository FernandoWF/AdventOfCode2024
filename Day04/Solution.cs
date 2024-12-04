namespace AdventOfCode2024.Day04;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        var matrix = input.ToSquareMatrix();

        return matrix
            .SelectMany(tuple => XmasSearcher.Search(matrix, tuple.X, tuple.Y))
            .Count();
    }

    public static object RunPart2(Input input)
    {
        var matrix = input.ToSquareMatrix();

        return matrix.Count(tuple => X_MasSearcher.Search(matrix, tuple.X, tuple.Y));
    }
}
