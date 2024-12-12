namespace AdventOfCode2024.Day12
{
    internal class Farm
    {
        public IReadOnlyList<HashSet<GardenPlot>> Regions { get; }
        public IReadOnlyDictionary<GardenPlot, HashSet<GardenPlot>> SameTypeNeighborsByGardenPlot { get; }

        private Farm(
            IReadOnlyList<HashSet<GardenPlot>> regions,
            IReadOnlyDictionary<GardenPlot, HashSet<GardenPlot>> sameTypeNeighborsByGardenPlot)
        {
            Regions = regions;
            SameTypeNeighborsByGardenPlot = sameTypeNeighborsByGardenPlot;
        }

        public static Farm Parse(Input input)
        {
            var gardenPlotMatrix = input.ToRectangularMatrix();
            var gardenPlotByPosition = gardenPlotMatrix.ToDictionary(
                tuple => new Position(tuple.X, tuple.Y),
                tuple => new GardenPlot(tuple.Value, new Position(tuple.X, tuple.Y)));
            var gardenPlots = gardenPlotByPosition.Values;

            Dictionary<GardenPlot, HashSet<GardenPlot>> regionByGardenPlot = [];
            Dictionary<GardenPlot, HashSet<GardenPlot>> sameTypeNeighborsByGardenPlot = [];

            foreach (var gardenPlot in gardenPlots)
            {
                HashSet<GardenPlot> sameTypeNeighbors = [];

                if (gardenPlot.Position.Y > 0)
                {
                    var upNeighborPlot = gardenPlotByPosition[gardenPlot.Position with { Y = gardenPlot.Position.Y - 1 }];
                    AddNeighborIfSameType(gardenPlot, upNeighborPlot, sameTypeNeighbors, regionByGardenPlot);
                }

                if (gardenPlot.Position.X > 0)
                {
                    var leftNeighborPlot = gardenPlotByPosition[gardenPlot.Position with { X = gardenPlot.Position.X - 1 }];
                    AddNeighborIfSameType(gardenPlot, leftNeighborPlot, sameTypeNeighbors, regionByGardenPlot);
                }

                if (gardenPlot.Position.X < gardenPlotMatrix.Width - 1)
                {
                    var rightNeighborPlot = gardenPlotByPosition[gardenPlot.Position with { X = gardenPlot.Position.X + 1 }];
                    AddNeighborIfSameType(gardenPlot, rightNeighborPlot, sameTypeNeighbors, regionByGardenPlot);
                }

                if (gardenPlot.Position.Y < gardenPlotMatrix.Height - 1)
                {
                    var downNeighborPlot = gardenPlotByPosition[gardenPlot.Position with { Y = gardenPlot.Position.Y + 1 }];
                    AddNeighborIfSameType(gardenPlot, downNeighborPlot, sameTypeNeighbors, regionByGardenPlot);
                }

                if (!regionByGardenPlot.TryGetValue(gardenPlot, out var region))
                {
                    region = [gardenPlot];
                    regionByGardenPlot.Add(gardenPlot, region);
                }

                region.UnionWith(sameTypeNeighbors);

                foreach (var neighborPlot in sameTypeNeighbors)
                {
                    regionByGardenPlot.TryAdd(neighborPlot, region);
                }

                sameTypeNeighborsByGardenPlot.Add(gardenPlot, sameTypeNeighbors);
            }

            return new Farm(regionByGardenPlot.Values.Distinct().ToList(), sameTypeNeighborsByGardenPlot);
        }

        private static void AddNeighborIfSameType(
            GardenPlot gardenPlot,
            GardenPlot neighborPlot,
            HashSet<GardenPlot> sameTypeNeighbors,
            Dictionary<GardenPlot, HashSet<GardenPlot>> regionByGardenPlot)
        {
            if (neighborPlot.PlantType != gardenPlot.PlantType)
            {
                return;
            }

            sameTypeNeighbors.Add(neighborPlot);
            var plotHasRegion = regionByGardenPlot.TryGetValue(gardenPlot, out var plotRegion);
            var neighborPlotHasRegion = regionByGardenPlot.TryGetValue(neighborPlot, out var neighborPlotRegion);

            if (plotHasRegion)
            {
                if (neighborPlotHasRegion && plotRegion != neighborPlotRegion)
                {
                    MergeRegions(plotRegion!, neighborPlotRegion!, regionByGardenPlot);
                }

                return;
            }

            if (neighborPlotHasRegion)
            {
                neighborPlotRegion!.Add(gardenPlot);
                regionByGardenPlot.Add(gardenPlot, neighborPlotRegion);
            }
        }

        private static void MergeRegions(
            HashSet<GardenPlot> regionToBeIncorporated,
            HashSet<GardenPlot> regionThatWillIncorporate,
            Dictionary<GardenPlot, HashSet<GardenPlot>> regionByGardenPlot)
        {
            regionThatWillIncorporate.UnionWith(regionToBeIncorporated);
            foreach (var plot in regionToBeIncorporated)
            {
                regionByGardenPlot[plot] = regionThatWillIncorporate;
            }
        }
    }
}
