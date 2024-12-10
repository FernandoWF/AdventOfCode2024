using System.Diagnostics;

namespace AdventOfCode2024.Day06;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        var lab = input.ToRectangularMatrix();
        var guardStartingPosition = GetGuardStartingPosition(lab);

        return GetVisitedPositions(guardStartingPosition, lab, out _)
            .Select(v => v.WithoutDirection())
            .Distinct()
            .Count();
    }

    private static DirectedPosition GetGuardStartingPosition(Matrix<char> lab)
    {
        var guardPosition = new DirectedPosition(-1, -1, Direction.Up);

        foreach ((var character, var x, var y) in lab)
        {
            if (character == '^')
            {
                guardPosition = guardPosition with { X = x, Y = y };
                break;
            }
        }

        if (guardPosition.X == -1 || guardPosition.Y == -1)
        {
            throw new UnreachableException("Guard initial position should have been found.");
        }

        return guardPosition;
    }

    private static HashSet<DirectedPosition> GetVisitedPositions(DirectedPosition guardStartingPosition, Matrix<char> lab, out bool isInLoop)
    {
        var guardPosition = guardStartingPosition;
        var visitedPositions = new HashSet<DirectedPosition> { guardPosition };

        while (!(guardPosition.Direction == Direction.Up && guardPosition.Y == 0)
            && !(guardPosition.Direction == Direction.Right && guardPosition.X == lab.Width - 1)
            && !(guardPosition.Direction == Direction.Down && guardPosition.Y == lab.Height - 1)
            && !(guardPosition.Direction == Direction.Left && guardPosition.X == 0))
        {
            var nextPosition = guardPosition.Direction switch
            {
                Direction.Up => guardPosition with { Y = guardPosition.Y - 1 },
                Direction.Right => guardPosition with { X = guardPosition.X + 1 },
                Direction.Down => guardPosition with { Y = guardPosition.Y + 1 },
                Direction.Left => guardPosition with { X = guardPosition.X - 1 },
                _ => throw new UnreachableException("All possible values should have been handled."),
            };

            if (lab[nextPosition.X, nextPosition.Y] == '#')
            {
                guardPosition = guardPosition with { Direction = guardPosition.Direction.TurnClockwise() };
            }
            else
            {
                guardPosition = nextPosition;

                if (visitedPositions.Contains(guardPosition))
                {
                    isInLoop = true;
                    return visitedPositions;
                }

                visitedPositions.Add(guardPosition);
            }
        }

        isInLoop = false;
        return visitedPositions;
    }

    public static object RunPart2(Input input)
    {
        var lab = input.ToRectangularMatrix();
        var guardStartingPosition = GetGuardStartingPosition(lab);
        var visitedPositions = GetVisitedPositions(guardStartingPosition, lab, out _);
        var newObstructionPositions = visitedPositions
            .Where(v => v != guardStartingPosition)
            .Select(v => v.WithoutDirection())
            .ToHashSet();

        return newObstructionPositions
            .Where(v =>
            {
                lab[v.X, v.Y] = '#';
                GetVisitedPositions(guardStartingPosition, lab, out var isInLoop);
                lab[v.X, v.Y] = '.';

                return isInLoop;
            })
            .Count();
    }
}
