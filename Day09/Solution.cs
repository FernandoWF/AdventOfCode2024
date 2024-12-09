namespace AdventOfCode2024.Day09;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        (var fileBlockPositionsFromId, var freeSpacePositions) = ParseDiskInformation(input);

        var idOfFileToMove = fileBlockPositionsFromId.Keys.Last();
        var lastBlockPositionContainingFile = fileBlockPositionsFromId[idOfFileToMove].Last();
        while (freeSpacePositions[0] < lastBlockPositionContainingFile)
        {
            var blockPositionsOfFileToMove = fileBlockPositionsFromId[idOfFileToMove];

            for (var i = blockPositionsOfFileToMove.Count - 1; i >= 0 && freeSpacePositions[0] < lastBlockPositionContainingFile; i--)
            {
                var firstFreeSpacePosition = freeSpacePositions[0];
                freeSpacePositions.RemoveAt(0);
                blockPositionsOfFileToMove[i] = firstFreeSpacePosition;

                lastBlockPositionContainingFile = i == 0
                    ? fileBlockPositionsFromId[idOfFileToMove - 1].Last()
                    : blockPositionsOfFileToMove[i - 1];
            }

            idOfFileToMove--;
        }

        return fileBlockPositionsFromId
            .SelectMany(pair => pair.Value.Select(p => (BlockPosition: p, FileId: pair.Key)))
            .Sum(tuple => (long)tuple.BlockPosition * tuple.FileId);
    }

    private static (Dictionary<int, List<int>> FileBlockPositionsFromId, List<int> FreeSpacePositions) ParseDiskInformation(Input input)
    {
        var fileBlockPositionsFromId = new Dictionary<int, List<int>>();
        var freeSpacePositions = new List<int>();

        var blockPosition = 0;
        var fileId = 0;
        var indicatingFile = true;

        foreach (var digit in input.Text.Select(c => (int)char.GetNumericValue(c)))
        {
            if (indicatingFile)
            {
                var id = fileId++;
                fileBlockPositionsFromId.Add(id, []);
                for (var i = 0; i < digit; i++)
                {
                    fileBlockPositionsFromId[id].Add(blockPosition++);
                }
            }
            else
            {
                for (var i = 0; i < digit; i++)
                {
                    freeSpacePositions.Add(blockPosition++);
                }
            }

            indicatingFile = !indicatingFile;
        }

        return (fileBlockPositionsFromId, freeSpacePositions);
    }

    public static object RunPart2(Input input)
    {
        (var fileBlockPositionsFromId, var freeSpacePositions) = ParseDiskInformation(input);

        for (var idOfFileToMove = fileBlockPositionsFromId.Keys.Last(); idOfFileToMove >= 1; idOfFileToMove--)
        {
            var blockPositionsOfFileToMove = fileBlockPositionsFromId[idOfFileToMove];
            var canMove = TryGetLeftmostFreeSpaceSpan(blockPositionsOfFileToMove.Count, freeSpacePositions, out var newBlockPosition);

            if (canMove && newBlockPosition < blockPositionsOfFileToMove[0])
            {
                for (var i = 0; i < blockPositionsOfFileToMove.Count; i++)
                {
                    freeSpacePositions.Remove(newBlockPosition);
                    freeSpacePositions.Add(blockPositionsOfFileToMove[i]);

                    blockPositionsOfFileToMove[i] = newBlockPosition;
                    newBlockPosition++;
                }

                freeSpacePositions.Sort();
            }
        }

        return fileBlockPositionsFromId
            .SelectMany(pair => pair.Value.Select(p => (BlockPosition: p, FileId: pair.Key)))
            .Sum(tuple => (long)tuple.BlockPosition * tuple.FileId);
    }

    private static bool TryGetLeftmostFreeSpaceSpan(int length, List<int> freeSpacePositions, out int startingBlockPosition)
    {
        var i = 0;
        var foundSpanLength = 1;
        startingBlockPosition = freeSpacePositions[0];

        while (foundSpanLength < length && i < freeSpacePositions.Count - 1)
        {
            if (freeSpacePositions[i + 1] == freeSpacePositions[i] + 1)
            {
                foundSpanLength++;
            }
            else
            {
                startingBlockPosition = freeSpacePositions[i + 1];
                foundSpanLength = 1;
            }

            i++;
        };

        if (foundSpanLength == length)
        {
            return true;
        }

        startingBlockPosition = -1;
        return false;
    }
}
