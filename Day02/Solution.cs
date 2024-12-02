namespace AdventOfCode2024.Day02;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        return input.Lines
            .Select(l =>
            {
                var levels = l.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => int.Parse(n))
                    .ToList();
                return new Report(levels);
            })
            .Where(r => r.IsSafe)
            .Count();
    }

    public static object RunPart2(Input input)
    {
        return input.Lines
            .Select(l =>
            {
                var levels = l.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(n => int.Parse(n))
                    .ToList();
                return new Report(levels);
            })
            .Where(r => r.IsSafeWhenDampened)
            .Count();
    }
}
