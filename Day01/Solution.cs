namespace AdventOfCode2024.Day01;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        (var leftIds, var rightIds) = ParseIds(input);
        leftIds.Sort();
        rightIds.Sort();

        return leftIds
            .Zip(rightIds)
            .Select(tuple => Math.Abs(tuple.First - tuple.Second))
            .Sum();
    }

    public static object RunPart2(Input input)
    {
        (var leftIds, var rightIds) = ParseIds(input);

        return leftIds
            .Select(leftId => leftId * rightIds.Count(rightId => rightId == leftId))
            .Sum();
    }

    private static (List<int> leftIds, List<int> rightIds) ParseIds(Input input)
    {
        var leftIds = new List<int>();
        var rightIds = new List<int>();

        const int IdLength = 5;
        const int SeparationLength = 3;

        foreach (var line in input.Lines)
        {
            var leftId = int.Parse(line[..IdLength]);
            leftIds.Add(leftId);
            var rightId = int.Parse(line[(IdLength + SeparationLength)..]);
            rightIds.Add(rightId);
        }

        return (leftIds, rightIds);
    }
}
