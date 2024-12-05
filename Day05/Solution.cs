namespace AdventOfCode2024.Day05;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        (var pageOrderingRules, var updates) = ParseInput(input);

        var correctlyOrderedUpdates = updates
            .Where(u => IsUpdateInCorrectOrder(u, pageOrderingRules))
            .ToList();

        var middlePageNumbers = correctlyOrderedUpdates
            .Select(u => u[u.Count / 2]);

        return middlePageNumbers.Sum();
    }

    private static (List<PageOrderingRule> PageOrderingRules, List<List<int>> Updates) ParseInput(Input input)
    {
        var sections = input.Text.Split("\n\n");
        var pageOrderingRules = sections[0]
            .Split('\n')
            .Select(l =>
            {
                var predecessorPage = int.Parse(l[..2]);
                var successorPage = int.Parse(l[3..]);

                return new PageOrderingRule(predecessorPage, successorPage);
            })
            .ToList();
        var updates = sections[1]
            .Split('\n')
            .Select(l => l.Split(',').Select(int.Parse).ToList())
            .ToList();

        return (pageOrderingRules, updates);
    }

    private static bool IsUpdateInCorrectOrder(List<int> updatePages, List<PageOrderingRule> pageOrderingRules)
    {
        for (var pageIndex = 0; pageIndex < updatePages.Count; pageIndex++)
        {
            var page = updatePages[pageIndex];
            for (var nextPageIndex = pageIndex + 1; nextPageIndex < updatePages.Count; nextPageIndex++)
            {
                var nextPage = updatePages[nextPageIndex];

                if (!pageOrderingRules.Contains(new PageOrderingRule(page, nextPage)))
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static object RunPart2(Input input)
    {
        (var pageOrderingRules, var updates) = ParseInput(input);

        var incorrectlyOrderedUpdates = updates
            .Where(u => !IsUpdateInCorrectOrder(u, pageOrderingRules))
            .ToList();

        foreach (var update in incorrectlyOrderedUpdates)
        {
            OrderUpdate(update, pageOrderingRules);
        }

        var middlePageNumbers = incorrectlyOrderedUpdates
            .Select(u => u[u.Count / 2]);

        return middlePageNumbers.Sum();
    }

    private static void OrderUpdate(List<int> updatePages, List<PageOrderingRule> pageOrderingRules)
    {
        static bool IsPageInCorrectOrder(int pageIndex, List<int> updatePages, List<PageOrderingRule> pageOrderingRules)
        {
            for (var i = pageIndex + 1; i < updatePages.Count; i++)
            {
                var page = updatePages[pageIndex];
                var nextPage = updatePages[i];

                if (!pageOrderingRules.Contains(new PageOrderingRule(page, nextPage)))
                {
                    return false;
                }
            }

            return true;
        }

        for (var i = 0; i < updatePages.Count - 1; i++)
        {
            var isPageInCorrectOrder = IsPageInCorrectOrder(i, updatePages, pageOrderingRules);

            while (!isPageInCorrectOrder)
            {
                var page = updatePages[i];
                updatePages.RemoveAt(i);
                updatePages.Add(page);

                isPageInCorrectOrder = IsPageInCorrectOrder(i, updatePages, pageOrderingRules);
            }
        }
    }

    private record PageOrderingRule(int PredecessorPage, int SuccessorPage);
}
