using System;
using TRANS4D.Compatibility;
using TRANS4D.Geometry;

namespace TRANS4D
{
    public class GridBasedRegion : RegionBase
    {
        // based on the region, a particular file is needed which
        // contains the grid points. In a future version, these
        // files are likely going to contain one grid each. For
        // now this is an implementation detail hidden away in
        // this class only.
        private static readonly FortranArray<int> NeededGrid = new[]
        {
            1, 1, 1, 1, 1, 1, 1,
            2
        }.ToFortranArray();

        private string GridFileResourcePathForRegion
        {
            get
            {
                int neededGrid = NeededGrid[RegionId];
                string fileName = null;
                if (neededGrid == 1) fileName = "Data4.2.5A.txt";
                else if (neededGrid == 2) fileName = "Data4.2.5B.txt";
                else throw new ArgumentException($"No grid file defined for region {RegionId}");
                // Prepend the namespace and Data folder for embedded resource
                return $"TRANS4D.Data.{fileName}";
            }
        }

        private GridDataFile GridDataFile
        {
            get
            {
                var resourcePath = GridFileResourcePathForRegion;
                if (!Ioc.NamedIocContainer.Instance.IsRegistered(resourcePath))
                {
                    var gridDataFile = new GridDataFile(resourcePath);
                    // Load from embedded resource stream
                    var assembly = typeof(GridBasedRegion).Assembly;
                    using (var stream = assembly.GetManifestResourceStream(resourcePath))
                    {
                        if (stream == null)
                            throw new InvalidOperationException($"Resource not found: {resourcePath}");
                        gridDataFile.LoadFile(stream);
                    }
                    Ioc.NamedIocContainer.Instance.Register(resourcePath, gridDataFile);
                }
                return Ioc.NamedIocContainer.Instance.Get<GridDataFile>(resourcePath);
            }
        }

        public GridBasedRegion(Polygon boundary, int regionId) : base(boundary)
        {
            RegionId = regionId;
        }

        public int RegionId { get; }

        public double XMin => BlockData.Velocity.GRDLX[RegionId];
        public double XMax => BlockData.Velocity.GRDUX[RegionId];
        public double NumX => BlockData.Velocity.ICNTX[RegionId];
        public double XInterval => (XMax - XMin) / NumX;

        public double YMin => BlockData.Velocity.GRDLY[RegionId];
        public double YMax => BlockData.Velocity.GRDUY[RegionId];
        public double NumY => BlockData.Velocity.ICNTY[RegionId];
        public double YInterval => (YMax - YMin) / NumY;



        /// <summary>
        /// Computes the velocity at a given latitude and longitude. (Positive East)
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public override VelocityInfo GetVelocity(GeodeticCoordinates coordinates)
        {

            var gridWeights = GetGridWeights(coordinates, out int xCellIndex, out int yCellIndex);
            var cellVelocities = GridDataFile.GetGridCellVelocities(RegionId, xCellIndex, yCellIndex);

            VelocityInfo result = new VelocityInfo()
            {
                NorthVelocity = gridWeights[0][0] * cellVelocities[0][0].NorthVelocity +
                                gridWeights[0][1] * cellVelocities[0][1].NorthVelocity +
                                gridWeights[1][0] * cellVelocities[1][0].NorthVelocity +
                                gridWeights[1][1] * cellVelocities[1][1].NorthVelocity,

                EastVelocity = gridWeights[0][0] * cellVelocities[0][0].EastVelocity +
                               gridWeights[0][1] * cellVelocities[0][1].EastVelocity +
                               gridWeights[1][0] * cellVelocities[1][0].EastVelocity +
                               gridWeights[1][1] * cellVelocities[1][1].EastVelocity,

                UpwardVelocity = gridWeights[0][0] * cellVelocities[0][0].UpwardVelocity +
                                 gridWeights[0][1] * cellVelocities[0][1].UpwardVelocity +
                                 gridWeights[1][0] * cellVelocities[1][0].UpwardVelocity +
                                 gridWeights[1][1] * cellVelocities[1][1].UpwardVelocity,

                SigmaNorthVelocity = gridWeights[0][0] * cellVelocities[0][0].SigmaNorthVelocity +
                                     gridWeights[0][1] * cellVelocities[0][1].SigmaNorthVelocity +
                                     gridWeights[1][0] * cellVelocities[1][0].SigmaNorthVelocity +
                                     gridWeights[1][1] * cellVelocities[1][1].SigmaNorthVelocity,

                SigmaEastVelocity = gridWeights[0][0] * cellVelocities[0][0].SigmaEastVelocity +
                                    gridWeights[0][1] * cellVelocities[0][1].SigmaEastVelocity +
                                    gridWeights[1][0] * cellVelocities[1][0].SigmaEastVelocity +
                                    gridWeights[1][1] * cellVelocities[1][1].SigmaEastVelocity,

                SigmaUpwardVelocity = gridWeights[0][0] * cellVelocities[0][0].SigmaUpwardVelocity +
                                      gridWeights[0][1] * cellVelocities[0][1].SigmaUpwardVelocity +
                                      gridWeights[1][0] * cellVelocities[1][0].SigmaUpwardVelocity +
                                      gridWeights[1][1] * cellVelocities[1][1].SigmaUpwardVelocity
            };

            return result;
        }

        public double[][] GetGridWeights(GeodeticCoordinates coordinates, out int xCellIndex, out int yCellIndex)
        {
            // coordinates are passed in as positive east for consistency, but the grids are defined using positive
            // west, so we're hiding that implementation detail here and getting degrees in positive west
            var xDegrees = coordinates.Longitude.NormalizeRadians().ToDegrees();
            var yDegrees = coordinates.LatitudeDegrees;


            // C*** Obtain indices for the lower-left corner of the cell
            // C*** containing the point
            // I = IDINT((POSX - GRDLX[JREGN]) / STEPX) + 1;
            // J = IDINT((POSY - GRDLY[JREGN]) / STEPY) + 1;
            xCellIndex = (int)((xDegrees - XMin) / XInterval) + 1;
            yCellIndex = (int)((yDegrees - YMin) / YInterval) + 1;

            // C*** Compute the limits of the grid cell
            double cellXMin = XMin + (xCellIndex - 1) * XInterval;
            double cellXMax = cellXMin + XInterval;
            double cellYMin = YMin + (yCellIndex - 1) * YInterval;
            double cellYMax = cellYMin + YInterval;

            double[][] gridWeights = new double[2][];
            gridWeights[0] = new double[2];
            gridWeights[1] = new double[2];

            // C*** Compute the weights for the interpolation
            double denominator = (cellXMax - cellXMin) * (cellYMax - cellYMin);

            gridWeights[0][0] = (cellXMax - xDegrees) * (cellYMax - yDegrees) / denominator;
            gridWeights[1][0] = (xDegrees - cellXMin) * (cellYMax - yDegrees) / denominator;
            gridWeights[0][1] = (cellXMax - xDegrees) * (yDegrees - cellYMin) / denominator;
            gridWeights[1][1] = (xDegrees - cellXMin) * (yDegrees - cellYMin) / denominator;

            return gridWeights;
        }
    }
}
