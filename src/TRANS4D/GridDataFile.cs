using System.IO;
using System.Linq;

namespace TRANS4D
{
    public class GridDataFile
    {
        public string ResourcePath { get; set; }
        public double[] Velocities { get; set; } 
        public double[] VelocityErrors { get; set; }
        public GridDataFile(string resourcePath)
        {
            ResourcePath = resourcePath;
        }

        private void Reallocate()
        {
            // Use LINQ to compute the maximum index needed for all regions (1-7)
            int maxIndex = Enumerable.Range(1, 7)
                .Select(iregn => BlockData.Velocity.NBASE[iregn] + 3 * ((BlockData.Velocity.ICNTX[iregn] + 1) * (BlockData.Velocity.ICNTY[iregn] + 1)) + 2)
                .Max();
            Velocities = new double[maxIndex + 1];
            VelocityErrors = new double[maxIndex + 1];
        }

        public void LoadFile(Stream stream)
        {
            Reallocate();
            GridRecord g = new GridRecord();
            stream.Seek(0, SeekOrigin.Begin);

            for (int iregn = 1; iregn <= 7; iregn++)
            {
                for (int i = 1; i <= BlockData.Velocity.ICNTX[iregn] + 1; i++)
                {
                    for (int j = 1; j <= BlockData.Velocity.ICNTY[iregn] + 1; j++)
                    {
                        g.ReadRecordFromFile(stream);
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

        //todo: understand this function, and rename it for C#. possibly Ungrid()?
        static int IUNGRD(int IREGN, int I, int J, int IVEC)
        {
            int IUNGRD = BlockData.Velocity.NBASE[IREGN] +
                         3 * ((J - 1) * (BlockData.Velocity.ICNTX[IREGN] + 1) + (I - 1)) + IVEC;

            return IUNGRD;
        }

        // todo: confirm order of velocity vectors
        /// <summary>
        /// Return the 2x2 array of VelocityInfo for the grid cell at (i,j) in region jregn.
        /// </summary>
        /// <param name="jregn"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <returns></returns>
        public VelocityInfo[][] GetGridCellVelocities(int jregn, int i, int j)
        {
            // initialize 2x2 array of VelocityInfo
            var result = new VelocityInfo[2][];
            for (int ii = 0; ii < 2; ii++)
            {
                result[ii] = new VelocityInfo[2];
            }

            for (int II = 0; II <= 1; II++)
            {
                for (int IJ = 0; IJ <= 1; IJ++)
                {
                    double vn = Velocities[IUNGRD(jregn, i + II, j + IJ, 1)];
                    double ve = Velocities[IUNGRD(jregn, i + II, j + IJ, 2)];
                    double vu = Velocities[IUNGRD(jregn, i + II, j + IJ, 3)];
                    double sn = VelocityErrors[IUNGRD(jregn, i + II, j + IJ, 1)];
                    double se = VelocityErrors[IUNGRD(jregn, i + II, j + IJ, 2)];
                    double su = VelocityErrors[IUNGRD(jregn, i + II, j + IJ, 3)];
                    result[II][IJ] = new VelocityInfo(vn, ve, vu, sn, se, su);
                }
            }
            return result;
        }

    }
}