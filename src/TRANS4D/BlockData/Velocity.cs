using System;
using System.Collections.Generic;
using System.Text;
using TRANS4D.Compatibility;

namespace TRANS4D.BlockData
{
    public class VelocityInfo
    {
        public double VN { get; set; }
        public double VE { get; set; }
        public double VU { get; set; }
        public double SN { get; set; }
        public double SE { get; set; }
        public double SU { get; set; }
    }

    internal class Velocity
    {
        public const int NUMGRD = 8;

        private static bool Initialized = false;

        private static readonly FortranArray<double> _GRDLX = new FortranArray<double>(NUMGRD);
        public static FortranArray<double> GRDLX
        {
            get
            {
                Init();
                return _GRDLX;
            }
        }

        private static readonly FortranArray<double> _GRDUX = new FortranArray<double>(NUMGRD);
        public static FortranArray<double> GRDUX
        {
            get
            {
                Init();
                return _GRDUX;
            }
        }

        private static readonly FortranArray<double> _GRDLY = new FortranArray<double>(NUMGRD);
        public static FortranArray<double> GRDLY
        {
            get
            {
                Init();
                return _GRDLY;
            }
        }

        private static readonly FortranArray<double> _GRDUY = new FortranArray<double>(NUMGRD);
        public static FortranArray<double> GRDUY
        {
            get
            {
                Init();
                return _GRDUY;
            }
        }

        private static readonly FortranArray<int> _ICNTX = new FortranArray<int>(NUMGRD);
        public static FortranArray<int> ICNTX
        {
            get
            {
                Init();
                return _ICNTX;
            }
        }

        private static readonly FortranArray<int> _ICNTY = new FortranArray<int>(NUMGRD);
        public static FortranArray<int> ICNTY
        {
            get
            {
                Init();
                return _ICNTY;
            }
        }

        private static readonly FortranArray<int> _NBASE = new FortranArray<int>(NUMGRD);
        public static FortranArray<int> NBASE
        {
            get
            {
                Init();
                return _NBASE;
            }
        }

        private static void Init()
        {
            if (Initialized)
                return;

            _GRDLX[1] = 238.20000000000;
            _GRDUX[1] = 239.49000000000;
            _GRDLY[1] = 35.80000000000;
            _GRDUY[1] = 36.79000000000;
            _ICNTX[1] = 129;
            _ICNTY[1] = 99;
            _NBASE[1] = 0;
            _GRDLX[2] = 235.00000000000;
            _GRDUX[2] = 253.00000000000;
            _GRDLY[2] = 31.00000000000;
            _GRDUY[2] = 49.00000000000;
            _ICNTX[2] = 288;
            _ICNTY[2] = 288;
            _NBASE[2] = 39000;
            _GRDLX[3] = 253.00000000000;
            _GRDUX[3] = 286.50000000000;
            _GRDLY[3] = 24.00000000000;
            _GRDUY[3] = 40.00000000000;
            _ICNTX[3] = 536;
            _ICNTY[3] = 256;
            _NBASE[3] = 289563;
            _GRDLX[4] = 253.00000000000;
            _GRDUX[4] = 294.00000000000;
            _GRDLY[4] = 24.00000000000;
            _GRDUY[4] = 50.00000000000;
            _ICNTX[4] = 82;
            _ICNTY[4] = 52;
            _NBASE[4] = 703590;
            _GRDLX[5] = 190.00000000000;
            _GRDUX[5] = 230.00000000000;
            _GRDLY[5] = 53.00000000000;
            _GRDUY[5] = 73.00000000000;
            _ICNTX[5] = 160;
            _ICNTY[5] = 80;
            _NBASE[5] = 716787;
            _GRDLX[6] = 231.00000000000;
            _GRDUX[6] = 240.00000000000;
            _GRDLY[6] = 49.00000000000;
            _GRDUY[6] = 51.00000000000;
            _ICNTX[6] = 36;
            _ICNTY[6] = 8;
            _NBASE[6] = 755910;
            _GRDLX[7] = 230.00000000000;
            _GRDUX[7] = 308.00000000000;
            _GRDLY[7] = 42.00000000000;
            _GRDUY[7] = 78.00000000000;
            _ICNTX[7] = 52;
            _ICNTY[7] = 36;
            _NBASE[7] = 756909;
            _GRDLX[8] = 265.00000000000;
            _GRDUX[8] = 303.00000000000;
            _GRDLY[8] = 6.00000000000;
            _GRDUY[8] = 24.00000000000;
            _ICNTX[8] = 608;
            _ICNTY[8] = 288;
            _NBASE[8] = 0;

            Initialized = true;
        }
    }
}
