namespace AdventOfCode2024.Day07
{
    internal class Equation
    {
        public long Result { get; }
        public IReadOnlyList<int> Numbers { get; }

        public Equation(string line)
        {
            var parts = line.Split(": ");
            Result = long.Parse(parts[0]);
            Numbers = parts[1].Split(' ')
                .Select(int.Parse)
                .ToList();
        }
    }
}
