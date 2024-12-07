namespace AdventOfCode2024.Day07;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        var equations = input.Lines
            .Select(l => new Equation(l))
            .ToList();

        static bool IsEquationSolvable(IReadOnlyList<int> numbers, long currentResult, long targetResult, int index)
        {
            if (index == numbers.Count) { return currentResult == targetResult; }
            if (currentResult > targetResult) { return false; }

            if (IsEquationSolvable(numbers, currentResult + numbers[index], targetResult, index + 1)) { return true; }
            if (IsEquationSolvable(numbers, currentResult * numbers[index], targetResult, index + 1)) { return true; }

            return false;
        }

        return equations
            .Where(e => IsEquationSolvable(e.Numbers, e.Numbers[0], e.Result, 1))
            .Select(e => e.Result)
            .Sum();
    }

    public static object RunPart2(Input input)
    {
        var equations = input.Lines
            .Select(l => new Equation(l))
            .ToList();

        static bool IsEquationSolvableWithConcatenation(IReadOnlyList<int> numbers, long currentResult, long targetResult, int index)
        {
            if (index == numbers.Count) { return currentResult == targetResult; }
            if (currentResult > targetResult) { return false; }

            if (IsEquationSolvableWithConcatenation(numbers, currentResult + numbers[index], targetResult, index + 1)) { return true; }
            if (IsEquationSolvableWithConcatenation(numbers, currentResult * numbers[index], targetResult, index + 1)) { return true; }

            var concatenatedValue = long.Parse(currentResult.ToString() + numbers[index].ToString());
            if (IsEquationSolvableWithConcatenation(numbers, concatenatedValue, targetResult, index + 1)) { return true; }

            return false;
        }

        return equations
            .Where(e => IsEquationSolvableWithConcatenation(e.Numbers, e.Numbers[0], e.Result, 1))
            .Select(e => e.Result)
            .Sum();
    }
}
