namespace AdventOfCode2024.Day02
{
    internal class Report
    {
        public bool IsSafe { get; }
        public bool IsSafeWhenDampened { get; }

        public Report(List<int> levels)
        {
            IsSafeWhenDampened = IsSafe = CheckSafe(levels);

            if (IsSafeWhenDampened)
            {
                return;
            }

            for (var i = 0; i < levels.Count; i++)
            {
                var dampenedLevels = levels.ToList();
                dampenedLevels.RemoveAt(i);

                if (CheckSafe(dampenedLevels))
                {
                    IsSafeWhenDampened = true;
                    return;
                }
            }
        }

        private static bool CheckSafe(List<int> levels)
        {
            var increasing = true;
            var decreasing = true;

            const int MinimumDifference = 1;
            const int MaximumDifference = 3;
            var validDifference = true;

            for (var i = 1; i < levels.Count; i++)
            {
                var previousLevel = levels[i - 1];
                var currentLevel = levels[i];

                if (currentLevel >= previousLevel)
                {
                    decreasing = false;
                }

                if (currentLevel <= previousLevel)
                {
                    increasing = false;
                }

                var difference = Math.Abs(currentLevel - previousLevel);
                if (difference < MinimumDifference || difference > MaximumDifference)
                {
                    validDifference = false;
                }

                if ((!increasing && !decreasing) || !validDifference)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
