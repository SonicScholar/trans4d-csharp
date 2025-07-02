using System;
using System.IO;
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
        private static readonly FortranArray<int> NeededGrid = new []
        {
            1, 1, 1, 1, 1, 1, 1,
            2
        }.ToFortranArray();

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

        public class GridFile
        {
            public string FilePath { get; set; }
            public double[] Velocities { get; set; }
            public double[] VelocityErrors { get; set; }
            public GridFile(string filePath)
            {
                FilePath = filePath;
            }

            public void LoadFile()
            {
                GridRecord g = new GridRecord();
                long seek = 0;
                using (var filestream = new FileStream(FilePath, FileMode.Open))
                {
                    filestream.Seek(0, SeekOrigin.Begin);

                    for (int iregn = 1; iregn <= 7; iregn++)
                    {
                        for (int i = 1; i <= BlockData.Velocity.ICNTX[iregn] + 1; i++)
                        {
                            for (int j = 1; j <= BlockData.Velocity.ICNTY[iregn] + 1; j++)
                            {
                                g.ReadRecordFromFile(filestream);
                                int index = IUNGRD(iregn, i, j, 1);
                                int index1 = index + 1;
                                int index2 = index + 2;
                                Velocities[index] = g.VN;
                                VelocityErrors[index] = g.SN;
                                Velocities[index1] = g.VE;
                                VelocityErrors[index1] = g.SE;
                                Velocities[index2] = g.VU;
                                VelocityErrors[index2] = g.SU;
                            }
                        }
                    }
                }
            }

            //todo: understand this function, and rename it for C#. possibly Ungrid()?
            static int IUNGRD(int IREGN, int I, int J, int IVEC)
            {
                int IUNGRD = BlockData.Velocity.NBASE[IREGN] +
                             3 * ((J - 1) * (BlockData.Velocity.ICNTX[IREGN] + 1) + (I - 1)) + IVEC;

                return IUNGRD;
            }

    }

        public struct GridRecord
        {
            public int padding1;
            public double VN;
            public double SN;
            public double VE;
            public double SE;
            public double VU;
            public double SU;
            public int padding2;

            // Reads a GridRecord from a FileStream (FORTRAN unformatted sequential binary)
            // Returns the total number of bytes read
            public uint ReadRecordFromFile(FileStream file)
            {
                int bytesRead = 0;
                using (var reader = new BinaryReader(file, System.Text.Encoding.Default, leaveOpen: true))
                {
                    padding1 = reader.ReadInt32(); bytesRead += sizeof(int);
                    VN = reader.ReadDouble(); bytesRead += sizeof(double);
                    SN = reader.ReadDouble(); bytesRead += sizeof(double);
                    VE = reader.ReadDouble(); bytesRead += sizeof(double);
                    SE = reader.ReadDouble(); bytesRead += sizeof(double);
                    VU = reader.ReadDouble(); bytesRead += sizeof(double);
                    SU = reader.ReadDouble(); bytesRead += sizeof(double);
                    padding2 = reader.ReadInt32(); bytesRead += sizeof(int);
                }
                return (uint)bytesRead;
            }
        }
    }
}
