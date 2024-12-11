namespace AdventOfCode2024.Day11;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        var stones = input.Text.Split(' ').ToList();

        const int TotalIterations = 25;
        for (var i = 0; i < TotalIterations; i++)
        {
            Blink(stones);
        }

        return stones.Count;
    }

    private static void Blink(List<string> stones)
    {
        for (var i = 0; i < stones.Count; i++)
        {
            var newStones = ChangeStone(stones[i]);

            stones[i] = newStones[0];

            if (newStones.Count == 2)
            {
                stones.Insert(++i, newStones[1]);
            }
        }
    }

    private static List<string> ChangeStone(string stone)
    {
        if (stone == "0")
        {
            return ["1"];
        }

        if (stone.Length % 2 == 0)
        {
            var leftStone = stone[..(stone.Length / 2)];
            var rightStone = stone[(stone.Length / 2)..];

            return [leftStone, rightStone.TrimStart('0').PadLeft(1, '0')];
        }

        return [(long.Parse(stone) * 2024).ToString()];
    }

    public static object RunPart2(Input input)
    {
        var stones = input.Text.Split(' ').ToList();
        var changedStonesCountByStoneAndIteration = new Dictionary<(string Stone, int Iteration), long>();

        const int TotalIterations = 75;
        return BlinkRecursively(stones, TotalIterations, 1, changedStonesCountByStoneAndIteration);
    }

    private static long BlinkRecursively(
        List<string> stones,
        int totalIterations,
        int currentIteration,
        Dictionary<(string Stone, int Iteration), long> changedStonesCountByStoneAndIteration)
    {
        if (currentIteration > totalIterations)
        {
            return stones.Count;
        }

        return stones
            .Select(s =>
            {
                if (!changedStonesCountByStoneAndIteration.TryGetValue((s, currentIteration), out var count))
                {
                    var newStones = ChangeStone(s);
                    count = BlinkRecursively(newStones, totalIterations, currentIteration + 1, changedStonesCountByStoneAndIteration);
                    changedStonesCountByStoneAndIteration.Add((s, currentIteration), count);
                }

                return count;
            })
            .Sum();
    }
}
