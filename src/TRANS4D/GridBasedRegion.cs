using TRANS4D.Geometry;

namespace TRANS4D
{
    public class GridBasedRegion : RegionBase
    {
        public GridBasedRegion(Polygon boundary, int regionId): base(boundary)
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


            throw new System.NotImplementedException();
        }

        public double[][] GetGridWeights(GeodeticCoordinates coordinates)
        {
            // coordinates are passed in as positive east for consistency, but the grids are defined using positive
            // west, so we're hiding that implementation detail here and getting degrees in positive west
            var xDegrees = coordinates.Longitude.NormalizeRadians().ToDegrees();
            var yDegrees = coordinates.LatitudeDegrees;


            // C*** Obtain indices for the lower-left corner of the cell
            // C*** containing the point
            // I = IDINT((POSX - GRDLX[JREGN]) / STEPX) + 1;
            // J = IDINT((POSY - GRDLY[JREGN]) / STEPY) + 1;
            int xCellIndex = (int)((xDegrees - XMin) / XInterval) + 1;
            int yCellIndex = (int)((yDegrees - YMin) / YInterval) + 1;

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
