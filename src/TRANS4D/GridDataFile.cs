using System.IO;

namespace TRANS4D
{
    public class GridDataFile
    {
        public string FilePath { get; set; }
        public double[] Velocities { get; set; }
        public double[] VelocityErrors { get; set; }
        public GridDataFile(string filePath)
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

        // todo: confirm order of velocity vectors
        public (double VN, double VE, double VU)[][] GRDVEC(int jregn, int i, int j)
        {
            var VEL = new (double VN, double VE, double VU)[2][];
            for (int ii = 0; ii < 2; ii++)
            {
                VEL[ii] = new (double VN, double VE, double VU)[2];
            }

            for (int II = 0; II <= 1; II++)
            {
                for (int IJ = 0; IJ <= 1; IJ++)
                {
                    double vn = Velocities[IUNGRD(jregn, i + II, j + IJ, 1)];
                    double ve = Velocities[IUNGRD(jregn, i + II, j + IJ, 2)];
                    double vu = Velocities[IUNGRD(jregn, i + II, j + IJ, 3)];
                    VEL[II][IJ] = (vn, ve, vu);
                }
            }
            return VEL;
        }

    }
}