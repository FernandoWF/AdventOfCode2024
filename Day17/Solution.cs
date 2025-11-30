using System.Diagnostics;

namespace AdventOfCode2024.Day17;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        var lines = input.Lines;
        var registerA = int.Parse(lines[0][(lines[0].LastIndexOf(' ') + 1)..]);
        var registerB = int.Parse(lines[1][(lines[1].LastIndexOf(' ') + 1)..]);
        var registerC = int.Parse(lines[2][(lines[2].LastIndexOf(' ') + 1)..]);
        var instructions = lines[4][(lines[4].LastIndexOf(' ') + 1)..].Split(',');
        var instructionPointer = 0;
        List<int> outputValues = [];

        int GetComboOperandValue(int value)
        {
            return value switch
            {
                0 or 1 or 2 or 3 => value,
                4 => registerA,
                5 => registerB,
                6 => registerC,
                7 => throw new UnreachableException("This value is reserved and should not have appeared."),
                _ => throw new UnreachableException($"Invalid value: {value}")
            };
        }

        while (instructionPointer < instructions.Length - 1)
        {
            var operandValue = int.Parse(instructions[instructionPointer + 1]);

            switch (instructions[instructionPointer])
            {
                case "0":
                    var denominator = Math.Pow(2, GetComboOperandValue(operandValue));
                    registerA = (int)(registerA / denominator);
                    instructionPointer += 2;
                    break;

                case "1":
                    registerB ^= operandValue;
                    instructionPointer += 2;
                    break;

                case "2":
                    registerB = GetComboOperandValue(operandValue) % 8;
                    instructionPointer += 2;
                    break;

                case "3":
                    instructionPointer = registerA == 0
                        ? instructionPointer + 2
                        : operandValue;
                    break;

                case "4":
                    registerB ^= registerC;
                    instructionPointer += 2;
                    break;

                case "5":
                    outputValues.Add(GetComboOperandValue(operandValue) % 8);
                    instructionPointer += 2;
                    break;

                case "6":
                    denominator = Math.Pow(2, GetComboOperandValue(operandValue));
                    registerB = (int)(registerA / denominator);
                    instructionPointer += 2;
                    break;

                case "7":
                    denominator = Math.Pow(2, GetComboOperandValue(operandValue));
                    registerC = (int)(registerA / denominator);
                    instructionPointer += 2;
                    break;
            }
        }

        return string.Join(',', outputValues);
    }

    public static object RunPart2(Input input)
    {
        var lines = input.Lines;
        var registerA = long.Parse(lines[0][(lines[0].LastIndexOf(' ') + 1)..]);
        var registerB = long.Parse(lines[1][(lines[1].LastIndexOf(' ') + 1)..]);
        var registerC = long.Parse(lines[2][(lines[2].LastIndexOf(' ') + 1)..]);
        var instructions = lines[4][(lines[4].LastIndexOf(' ') + 1)..]
            .Split(',')
            .Select(long.Parse)
            .ToList();
        var instructionPointer = 0;
        List<long> outputValues = [];

        long GetComboOperandValue(long value)
        {
            return value switch
            {
                0 or 1 or 2 or 3 => value,
                4 => registerA,
                5 => registerB,
                6 => registerC,
                7 => throw new UnreachableException("This value is reserved and should not have appeared."),
                _ => throw new UnreachableException($"Invalid value: {value}")
            };
        }

        var initializationValue = 0;
        while (!instructions.SequenceEqual(outputValues))
        {
            registerA = initializationValue++;
            outputValues = [];
            instructionPointer = 0;

            while (instructionPointer < instructions.Count - 1)
            {
                var operandValue = instructions[instructionPointer + 1];

                switch (instructions[instructionPointer])
                {
                    case 0:
                        var denominator = Math.Pow(2, GetComboOperandValue(operandValue));
                        registerA = (long)(registerA / denominator);
                        instructionPointer += 2;
                        break;

                    case 1:
                        registerB ^= operandValue;
                        instructionPointer += 2;
                        break;

                    case 2:
                        registerB = GetComboOperandValue(operandValue) % 8L;
                        instructionPointer += 2;
                        break;

                    case 3:
                        instructionPointer = registerA == 0L
                            ? instructionPointer + 2
                            : (int)operandValue;
                        break;

                    case 4:
                        registerB ^= registerC;
                        instructionPointer += 2;
                        break;

                    case 5:
                        outputValues.Add(GetComboOperandValue(operandValue) % 8L);
                        instructionPointer += 2;
                        break;

                    case 6:
                        denominator = Math.Pow(2, GetComboOperandValue(operandValue));
                        registerB = (long)(registerA / denominator);
                        instructionPointer += 2;
                        break;

                    case 7:
                        denominator = Math.Pow(2, GetComboOperandValue(operandValue));
                        registerC = (long)(registerA / denominator);
                        instructionPointer += 2;
                        break;
                }
            }
        }

        return registerA;
    }
}
