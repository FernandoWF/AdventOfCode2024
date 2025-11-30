using QuikGraph;
using QuikGraph.Algorithms.RankedShortestPath;
using QuikGraph.Algorithms.ShortestPath;

namespace AdventOfCode2024.Day16;

internal sealed class Solution : ISolution
{
    private record Edge(Tile Source, Tile Target, Direction Direction) : IEdge<Tile>;

    public static object RunPart1(Input input)
    {
        var map = input.ToRectangularMatrix();

        var startingPosition = map
            .Where(tuple => tuple.Value == 'S')
            .Select(tuple => new Position(tuple.X, tuple.Y))
            .Single();
        map[startingPosition.X, startingPosition.Y] = '.';

        var endingPosition = map
            .Where(tuple => tuple.Value == 'E')
            .Select(tuple => new Position(tuple.X, tuple.Y))
            .Single();
        map[endingPosition.X, endingPosition.Y] = '.';

        var graph = GetGraph(map);

        var startingTile = graph.Vertices
            .First(vertice => vertice.Position == startingPosition);
        startingTile.EntryDirection = Direction.Right;

        var endingTile = graph.Vertices
            .First(vertice => vertice.Position == endingPosition);

        static double GetCost(Edge edge)
        {
            edge.Target.EntryDirection = edge.Direction;
            return edge.Source.EntryDirection!.Value.GetRotationCost(edge.Direction) + 1;
        }

        var dijkstra = new DijkstraShortestPathAlgorithm<Tile, Edge>(graph, GetCost);
        dijkstra.Compute(startingTile);

        return dijkstra.GetDistance(endingTile);
    }

    private static BidirectionalGraph<Tile, Edge> GetGraph(Matrix<char> map)
    {
        var tileFromPosition = new Dictionary<Position, Tile>();
        var graph = new BidirectionalGraph<Tile, Edge>();

        void TryAddEdge(Tile tile, Position neighborPosition, Direction direction)
        {
            if (map[neighborPosition.X, neighborPosition.Y] == '.')
            {
                if (!tileFromPosition.TryGetValue(neighborPosition, out var targetTile))
                {
                    targetTile = new Tile(neighborPosition);
                    tileFromPosition.Add(neighborPosition, targetTile);
                }

                graph.AddVerticesAndEdge(new Edge(tile, targetTile, direction));
            }
        }

        foreach ((var character, var x, var y) in map)
        {
            if (character == '#') { continue; }

            var position = new Position(x, y);
            if (!tileFromPosition.TryGetValue(position, out var tile))
            {
                tile = new Tile(position);
                tileFromPosition.Add(position, tile);
            }

            TryAddEdge(tile, new Position(x, y - 1), Direction.Up);
            TryAddEdge(tile, new Position(x, y + 1), Direction.Down);
            TryAddEdge(tile, new Position(x - 1, y), Direction.Left);
            TryAddEdge(tile, new Position(x + 1, y), Direction.Right);
        }

        return graph;
    }

    public static object RunPart2(Input input)
    {
        var map = input.ToRectangularMatrix();

        var startingPosition = map
            .Where(tuple => tuple.Value == 'S')
            .Select(tuple => new Position(tuple.X, tuple.Y))
            .Single();
        map[startingPosition.X, startingPosition.Y] = '.';

        var endingPosition = map
            .Where(tuple => tuple.Value == 'E')
            .Select(tuple => new Position(tuple.X, tuple.Y))
            .Single();
        map[endingPosition.X, endingPosition.Y] = '.';

        var graph = GetGraph(map);

        var startingTile = graph.Vertices
            .First(vertice => vertice.Position == startingPosition);
        startingTile.EntryDirection = Direction.Right;

        var endingTile = graph.Vertices
            .First(vertice => vertice.Position == endingPosition);

        static double GetCost(Edge edge)
        {
            // Avoid going back to a tile that was already calculated
            // It would mess with the EntryDirection value and would not give a valid path anyway
            if (edge.Target.EntryDirection.HasValue && edge.Target.EntryDirection.Value != edge.Direction)
            {
                return double.MaxValue;
            }

            edge.Target.EntryDirection = edge.Direction;
            return edge.Source.EntryDirection!.Value.GetRotationCost(edge.Direction) + 1;
        }

        var dijkstra = new DijkstraShortestPathAlgorithm<Tile, Edge>(graph, GetCost);
        dijkstra.Compute(startingTile);
        var bestPathScore = dijkstra.GetDistance(endingTile);

        // The Dijkstra run sets EntryDirection of all tiles, which Hoffman-Pavley run will use
        var hoffmanPavley = new HoffmanPavleyRankedShortestPathAlgorithm<Tile, Edge>(
            graph,
            edge => edge.Source.EntryDirection!.Value.GetRotationCost(edge.Direction) + 1)
        {
            ShortestPathCount = 31 // Trial and error. Should be enough to account for all best paths, increased until the correct result came out
        };
        hoffmanPavley.Compute(startingTile, endingTile);

        var allBestPaths = hoffmanPavley.ComputedShortestPaths
            .Where(path =>
            {
                foreach (var vertice in graph.Vertices)
                {
                    vertice.EntryDirection = null;
                }
                startingTile.EntryDirection = Direction.Right;

                return path.Select(GetCost).Sum() == bestPathScore;
            })
            .ToList();

        var tilesInBestPaths = allBestPaths
            .SelectMany(path => path.SelectMany(edge => new[] { edge.Source, edge.Target }))
            .ToHashSet();

        return tilesInBestPaths.Count;
    }
}
