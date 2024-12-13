using MathNet.Numerics.LinearAlgebra;
using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day13;

internal sealed class Solution : ISolution
{
    const int ButtonATokens = 3;
    const int ButtonBTokens = 1;

    public static object RunPart1(Input input)
    {
        var machineRawTexts = input.Text.Split("\n\n");
        var machines = machineRawTexts.Select(ParseMachine);

        return machines
            .Select(m =>
            {
                List<(int ButtonAPresses, int ButtonBPresses)> validPressCombinations = [];

                for (var buttonAPresses = 0; buttonAPresses < 100; buttonAPresses++)
                {
                    for (var buttonBPresses = 0; buttonBPresses < 100; buttonBPresses++)
                    {
                        var buttonAMovement = m.ButtonAIncrease * buttonAPresses;
                        var buttonBMovement = m.ButtonBIncrease * buttonBPresses;

                        if (buttonAMovement + buttonBMovement == m.PrizePosition)
                        {
                            validPressCombinations.Add((buttonAPresses, buttonBPresses));
                        }
                    }
                }

                if (validPressCombinations.Count == 0)
                {
                    return 0;
                }

                return validPressCombinations
                    .Select(a => a.ButtonAPresses * ButtonATokens + a.ButtonBPresses * ButtonBTokens)
                    .Min();
            })
            .Sum();
    }

    private static Machine ParseMachine(string text)
    {
        static Position ParsePosition(string line)
        {
            var matches = Regex.Matches(line, "\\d+");
            return new Position(int.Parse(matches[0].Value), int.Parse(matches[1].Value));
        }

        var lines = text.Split("\n");
        var buttonAMatches = ParsePosition(lines[0]);
        var buttonBMatches = ParsePosition(lines[1]);
        var prizePosition = ParsePosition(lines[2]);

        return new Machine(buttonAMatches, buttonBMatches, prizePosition);
    }

    public static object RunPart2(Input input)
    {
        var machineTexts = input.Text.Split("\n\n");
        var machines = machineTexts.Select(ParseMachine);

        return machines
            .Select(m =>
            {
                var buttonMovementMatrix = CreateMatrix.DenseOfArray(new double[,]
                {
                    { m.ButtonAIncrease.X, m.ButtonBIncrease.X },
                    { m.ButtonAIncrease.Y, m.ButtonBIncrease.Y }
                });

                const long PositionIncrease = 10000000000000L;
                var increasedPrizePosition = m.PrizePosition + new Position(PositionIncrease, PositionIncrease);

                var prizePositionMatrix = CreateMatrix.DenseOfArray(new double[,]
                {
                    { increasedPrizePosition.X },
                    { increasedPrizePosition.Y }
                });

                var solution = buttonMovementMatrix.Solve(prizePositionMatrix);

                var buttonAPresses = (long)Math.Round(solution[0, 0]);
                var buttonBPresses = (long)Math.Round(solution[1, 0]);

                var buttonAMovement = m.ButtonAIncrease * buttonAPresses;
                var buttonBMovement = m.ButtonBIncrease * buttonBPresses;

                return buttonAMovement + buttonBMovement == increasedPrizePosition
                    ? ButtonATokens * buttonAPresses + ButtonBTokens * buttonBPresses
                    : 0;
            })
            .Sum();
    }
}
