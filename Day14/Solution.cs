namespace AdventOfCode2024.Day14;

using System.Text.RegularExpressions;

internal sealed class Solution : ISolution
{
    private const int SpaceWidth = 101;
    private const int QuadrantWidth = SpaceWidth / 2;
    private const int SpaceHeight = 103;
    private const int QuadrantHeight = SpaceHeight / 2;

    public static object RunPart1(Input input)
    {
        var robots = ParseRobots(input);

        const int Seconds = 100;
        MoveRobots(robots, Seconds);

        var firstQuadrant = new Quadrant
        {
            StartingX = SpaceWidth - QuadrantWidth,
            StartingY = 0,
            EndingX = SpaceWidth - 1,
            EndingY = QuadrantHeight - 1
        };

        var secondQuadrant = new Quadrant
        {
            StartingX = 0,
            StartingY = 0,
            EndingX = QuadrantWidth - 1,
            EndingY = QuadrantHeight - 1
        };

        var thirdQuadrant = new Quadrant
        {
            StartingX = 0,
            StartingY = SpaceHeight - QuadrantHeight,
            EndingX = QuadrantWidth - 1,
            EndingY = SpaceHeight - 1
        };

        var fourthQuadrant = new Quadrant
        {
            StartingX = SpaceWidth - QuadrantWidth,
            StartingY = SpaceHeight - QuadrantHeight,
            EndingX = SpaceWidth - 1,
            EndingY = SpaceHeight - 1
        };

        var firstQuadrantRobotCount = robots
            .Where(r => r.IsInQuadrant(firstQuadrant))
            .Count();
        var secondQuadrantRobotCount = robots
            .Where(r => r.IsInQuadrant(secondQuadrant))
            .Count();
        var thirdQuadrantRobotCount = robots
            .Where(r => r.IsInQuadrant(thirdQuadrant))
            .Count();
        var fourthQuadrantRobotCount = robots
            .Where(r => r.IsInQuadrant(fourthQuadrant))
            .Count();

        return firstQuadrantRobotCount * secondQuadrantRobotCount * thirdQuadrantRobotCount * fourthQuadrantRobotCount;
    }

    private static List<Robot> ParseRobots(Input input)
    {
        return input.Lines
            .Select(l =>
            {
                var matches = Regex.Matches(l, "-?\\d+");
                return new Robot
                {
                    Position = new(int.Parse(matches[0].Value), int.Parse(matches[1].Value)),
                    Velocity = new(int.Parse(matches[2].Value), int.Parse(matches[3].Value))
                };
            })
            .ToList();
    }

    private static void MoveRobots(List<Robot> robots, int seconds)
    {
        for (var i = 0; i < seconds; i++)
        {
            foreach (var robot in robots)
            {
                var newX = (robot.Position.X + robot.Velocity.X + SpaceWidth) % SpaceWidth;
                var newY = (robot.Position.Y + robot.Velocity.Y + SpaceHeight) % SpaceHeight;
                robot.Position = new Pair(newX, newY);
            }
        }
    }

    public static object RunPart2(Input input)
    {
        var robots = ParseRobots(input);
        var seconds = 0;

        PrintSpace(robots);

        while (string.IsNullOrEmpty(Console.ReadLine()))
        {
            MoveRobots(robots, 1);
            seconds++;

            PrintSpace(robots);
        }

        return seconds;
    }

    private static void PrintSpace(List<Robot> robots)
    {
        for (var y = 0; y < SpaceHeight; y++)
        {
            for (var x = 0; x < SpaceWidth; x++)
            {
                if (robots.Any(r => r.Position.X == x && r.Position.Y == y))
                {
                    Console.Write('█');
                }
                else
                {
                    Console.Write(' ');
                }
            }

            Console.WriteLine();
        }

        Console.WriteLine("=====================================================================================================");
    }
}
