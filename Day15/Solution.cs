using System.Diagnostics;

namespace AdventOfCode2024.Day15;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        var inputs = input.Text.Split("\n\n");

        var warehouseMap = new Input(inputs[0]).ToRectangularMatrix();
        var warehouse = new Warehouse(warehouseMap);

        foreach (var direction in ParseMovementDirections(inputs[1]))
        {
            TryMoveRobot(direction, warehouse);
        }

        return warehouse.BoxPositions
            .Select(position => position.X + position.Y * 100)
            .Sum();
    }

    private static List<Direction> ParseMovementDirections(string input)
    {
        return input
            .Split('\n')
            .SelectMany(line => line.Select(character => character switch
            {
                '^' => Direction.Up,
                'v' => Direction.Down,
                '<' => Direction.Left,
                '>' => Direction.Right,
                _ => throw new UnreachableException("Invalid movement character.")
            }))
            .ToList();
    }

    private static void TryMoveRobot(Direction direction, Warehouse warehouse)
    {
        var movementOffset = direction.GetMovementOffset();
        var newRobotPosition = warehouse.RobotPosition + movementOffset;

        if (warehouse.WallPositions.Contains(newRobotPosition))
        {
            return;
        }

        if (!warehouse.BoxPositions.TryGetValue(newRobotPosition, out var boxPosition)
            || TryMoveBox(boxPosition, movementOffset, warehouse))
        {
            warehouse.RobotPosition = newRobotPosition;
        }
    }

    private static bool TryMoveBox(Pair boxPositon, Pair movementOffset, Warehouse warehouse)
    {
        var newBoxPosition = boxPositon + movementOffset;

        if (warehouse.WallPositions.Contains(newBoxPosition))
        {
            return false;
        }

        if (!warehouse.BoxPositions.TryGetValue(newBoxPosition, out var otherBoxPosition)
            || TryMoveBox(otherBoxPosition, movementOffset, warehouse))
        {
            warehouse.BoxPositions.Remove(boxPositon);
            warehouse.BoxPositions.Add(newBoxPosition);

            return true;
        }

        return false;
    }

    public static object RunPart2(Input input)
    {
        var inputs = input.Text.Split("\n\n");

        var warehouseMap = new Input(inputs[0]).ToRectangularMatrix();
        var warehouse = new WideWarehouse(warehouseMap);

        foreach (var direction in ParseMovementDirections(inputs[1]))
        {
            TryMoveRobot(direction, warehouse);
        }

        return warehouse.BoxLeftPositionFromPosition.Values
            .Distinct()
            .Select(position => position.X + position.Y * 100)
            .Sum();
    }

    private static void TryMoveRobot(Direction direction, WideWarehouse warehouse)
    {
        var movementOffset = direction.GetMovementOffset();
        var newRobotPosition = warehouse.RobotPosition + movementOffset;

        if (warehouse.WallPositions.Contains(newRobotPosition))
        {
            return;
        }

        if (warehouse.BoxLeftPositionFromPosition.TryGetValue(newRobotPosition, out var boxLeftPosition))
        {
            if (CanMoveBox(boxLeftPosition, movementOffset, warehouse))
            {
                MoveBox(boxLeftPosition, movementOffset, warehouse);
            }
            else
            {
                return;
            }
        }

        warehouse.RobotPosition = newRobotPosition;
    }

    private static bool CanMoveBox(Pair leftPosition, Pair movementOffset, WideWarehouse warehouse)
    {
        var newLeftPosition = leftPosition + movementOffset;
        var newRightPosition = newLeftPosition + new Pair(1, 0);

        if (warehouse.WallPositions.Contains(newLeftPosition)
            || warehouse.WallPositions.Contains(newRightPosition))
        {
            return false;
        }

        var canMoveLeftPosition = !warehouse.BoxLeftPositionFromPosition.TryGetValue(newLeftPosition, out var otherBoxLeftPosition)
            || otherBoxLeftPosition == leftPosition
            || CanMoveBox(otherBoxLeftPosition, movementOffset, warehouse);
        var canMoveRightPosition = !warehouse.BoxLeftPositionFromPosition.TryGetValue(newRightPosition, out otherBoxLeftPosition)
            || otherBoxLeftPosition == leftPosition
            || CanMoveBox(otherBoxLeftPosition, movementOffset, warehouse);

        return canMoveLeftPosition && canMoveRightPosition;
    }

    private static void MoveBox(Pair leftPosition, Pair movementOffset, WideWarehouse warehouse)
    {
        var rightPosition = leftPosition + new Pair(1, 0);
        var newLeftPosition = leftPosition + movementOffset;
        var newRightPosition = newLeftPosition + new Pair(1, 0);

        if (warehouse.BoxLeftPositionFromPosition.TryGetValue(newLeftPosition, out var otherBoxLeftPosition)
            && otherBoxLeftPosition != leftPosition)
        {
            MoveBox(otherBoxLeftPosition, movementOffset, warehouse);
        }

        if (warehouse.BoxLeftPositionFromPosition.TryGetValue(newRightPosition, out otherBoxLeftPosition)
            && otherBoxLeftPosition != leftPosition)
        {
            MoveBox(otherBoxLeftPosition, movementOffset, warehouse);
        }

        warehouse.BoxLeftPositionFromPosition.Remove(leftPosition);
        warehouse.BoxLeftPositionFromPosition.Remove(rightPosition);
        warehouse.BoxLeftPositionFromPosition.Add(newLeftPosition, newLeftPosition);
        warehouse.BoxLeftPositionFromPosition.Add(newRightPosition, newLeftPosition);
    }
}
