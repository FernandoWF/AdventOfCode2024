using System.Diagnostics;
using System.Reflection;

namespace AdventOfCode2024;

internal static class SolutionRunner
{
    public static void Run<TDay>() where TDay : ISolution
    {
        var input = InputFetcher.Fetch<TDay>().GetAwaiter().GetResult();

        var stopwatch = new Stopwatch();
        Console.WriteLine("========== Part 1 ==========");

        stopwatch.Start();
        var solution = TDay.RunPart1(input);
        stopwatch.Stop();

        Console.WriteLine(solution?.ToString());
        Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");

        Console.WriteLine();
        Console.WriteLine("========== Part 2 ==========");

        stopwatch.Restart();
        solution = TDay.RunPart2(input);
        stopwatch.Stop();

        Console.WriteLine(solution?.ToString());
        Console.WriteLine($"Elapsed time: {stopwatch.Elapsed}");
    }

    public static void RunLast()
    {
        var iSolutionType = typeof(ISolution);
        var lastSolutionType = Assembly.GetExecutingAssembly().GetTypes()
            .Where(t => t.IsAssignableTo(iSolutionType) && t != iSolutionType)
            .OrderByDescending(s => s.Namespace)
            .First();

        typeof(SolutionRunner)
            .GetMethod(nameof(Run))!
            .MakeGenericMethod(lastSolutionType)
            .Invoke(null, null);
    }
}
