namespace AdventOfCode2024.Day08;

internal sealed class Solution : ISolution
{
    public static object RunPart1(Input input)
    {
        var map = input.ToSquareMatrix();

        var antennas = map
            .Where(tuple => tuple.Value != '.')
            .Select(tuple => new Antenna(tuple.Value, new Position(tuple.X, tuple.Y)))
            .ToList();

        return antennas
            .GroupBy(a => a.Character)
            .SelectMany(g =>
            {
                var positions = new HashSet<Position>();

                foreach (var antenna in g)
                {
                    foreach (var otherAntenna in g.Where(a => a != antenna))
                    {
                        var xDifference = otherAntenna.Position.X - antenna.Position.X;
                        var yDifference = otherAntenna.Position.Y - antenna.Position.Y;

                        var antennaAntinodePosition = new Position(antenna.Position.X - xDifference, antenna.Position.Y - yDifference);
                        var otherAntennaAntinodePosition = new Position(otherAntenna.Position.X + xDifference, otherAntenna.Position.Y + yDifference);

                        positions.Add(antennaAntinodePosition);
                        positions.Add(otherAntennaAntinodePosition);
                    }
                }

                return positions;
            })
            .Where(p => p.X >= 0 && p.X < map.Width && p.Y >= 0 && p.Y < map.Height)
            .Distinct()
            .Count();
    }

    public static object RunPart2(Input input)
    {
        var map = input.ToSquareMatrix();

        var antennas = map
            .Where(tuple => tuple.Value != '.')
            .Select(tuple => new Antenna(tuple.Value, new Position(tuple.X, tuple.Y)))
            .ToList();

        return antennas
            .GroupBy(a => a.Character)
            .SelectMany(g =>
            {
                var positions = new HashSet<Position>();

                foreach (var antenna in g)
                {
                    positions.Add(new Position(antenna.Position.X, antenna.Position.Y));

                    foreach (var otherAntenna in g.Where(a => a != antenna))
                    {
                        positions.Add(new Position(otherAntenna.Position.X, otherAntenna.Position.Y));

                        var xDifference = otherAntenna.Position.X - antenna.Position.X;
                        var yDifference = otherAntenna.Position.Y - antenna.Position.Y;

                        var antennaAntinodePosition = new Position(antenna.Position.X - xDifference, antenna.Position.Y - yDifference);
                        var otherAntennaAntinodePosition = new Position(otherAntenna.Position.X + xDifference, otherAntenna.Position.Y + yDifference);

                        var antennaAntinodeOutOfBounds = false;
                        var otherAntennaAntinodeOutOfBounds = false;

                        while (!antennaAntinodeOutOfBounds || !otherAntennaAntinodeOutOfBounds)
                        {
                            if (antennaAntinodePosition.X < 0
                                || antennaAntinodePosition.X >= map.Width
                                || antennaAntinodePosition.Y < 0
                                || antennaAntinodePosition.Y >= map.Height)
                            {
                                antennaAntinodeOutOfBounds = true;
                            }
                            else
                            {
                                positions.Add(antennaAntinodePosition);
                            }

                            if (otherAntennaAntinodePosition.X < 0
                                || otherAntennaAntinodePosition.X >= map.Width
                                || otherAntennaAntinodePosition.Y < 0
                                || otherAntennaAntinodePosition.Y >= map.Height)
                            {
                                otherAntennaAntinodeOutOfBounds = true;
                            }
                            else
                            {
                                positions.Add(otherAntennaAntinodePosition);
                            }

                            antennaAntinodePosition = new Position(antennaAntinodePosition.X - xDifference, antennaAntinodePosition.Y - yDifference);
                            otherAntennaAntinodePosition = new Position(otherAntennaAntinodePosition.X + xDifference, otherAntennaAntinodePosition.Y + yDifference);
                        }
                    }
                }

                return positions;
            })
            .Distinct()
            .Count();
    }

    private record Position(int X, int Y);
    private record Antenna(char Character, Position Position);
}
