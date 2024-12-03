using System.Text.RegularExpressions;

namespace AdventOfCode2024.Day03;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        return Regex.Matches(input.Text, @"mul\(([0-9]{1,3})\,([0-9]{1,3})\)")
            .Select(m =>
            {
                var firstNumber = int.Parse(m.Groups[1].Value);
                var secondNumber = int.Parse(m.Groups[2].Value);

                return firstNumber * secondNumber;
            })
            .Sum();
    }

    public static object RunPart2(Input input)
    {
        var multiplicationProcessor = new MultiplicationProcessor();
        var doProcessor = new DoProcessor();
        var dontProcessor = new DontProcessor();
        var multiply = true;
        var total = 0;

        foreach (var character in input.Text)
        {
            multiplicationProcessor.Process(character);
            doProcessor.Process(character);
            dontProcessor.Process(character);

            if (multiply && multiplicationProcessor.Result is int number)
            {
                total += number;
            }

            if (doProcessor.Finished)
            {
                multiply = true;
            }

            if (dontProcessor.Finished)
            {
                multiply = false;
            }
        }

        return total;
    }
}
