using System;
using System.Collections.Generic;
using System.Text;
using TRANS4D.Compatibility;

namespace TRANS4D
{
    public class BuiltInTransforms
    {
        //// *** From ITRF2014 to NAD 83[2011] or NAD 83[CORS96]
        //tx[1] = 1.00530e0;
        //ty[1] = -1.9021e0;
        //tz[1] = -.54157e0;
        //dtx[1] = 0.00079e0;
        //dty[1] = -.00060e0;
        //dtz[1] = -.00144e0;
        //rx[1] = 0.02678138 / RHOSEC;
        //ry[1] = -0.00042027 / RHOSEC;
        //rz[1] = 0.01093206 / RHOSEC;
        //drx[1] = 0.00006667 / RHOSEC;
        //dry[1] = -.00075744 / RHOSEC;
        //drz[1] = -.00005133 / RHOSEC;
        //scale[1] = 0.36891e-9;
        //dscale[1] = -0.07201e-9;
        //refepc[1] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_NAD_1983_2011 = new TransformationParameters()
        {
            Tx = 1.00530e0,
            Ty = -1.9021e0,
            Tz = -.54157e0,
            Dtx = 0.00079e0,
            Dty = -.00060e0,
            Dtz = -.00144e0,
            Rx = 0.02678138 / Constants.RHOSEC,
            Ry = -0.00042027 / Constants.RHOSEC,
            Rz = 0.01093206 / Constants.RHOSEC,
            Drx = 0.00006667 / Constants.RHOSEC,
            Dry = -.00075744 / Constants.RHOSEC,
            Drz = -.00005133 / Constants.RHOSEC,
            Scale = 0.36891e-9,
            DScale = -0.07201e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF88
        //tx[2] = 0.0254e0;
        //ty[2] = -.0005e0;
        //tz[2] = -.1548e0;
        //dtx[2] = 0.0001e0;    
        //dty[2] = -.0005e0;
        //dtz[2] = -.0033e0;
        //rx[2] = -.0001e0 / RHOSEC;
        //ry[2] = 0.e0;
        //rz[2] = -0.00026e0 / RHOSEC;
        //drx[2] = 0.0e0;
        //dry[2] = 0.0e0;
        //drz[2] = -.00002e0 / RHOSEC;
        //scale[2] = 11.29e-9;
        //dscale[2] = 0.12e-9;
        //refepc[2] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_1988 = new TransformationParameters()
        {
            Tx = 0.0254e0,
            Ty = -.0005e0,
            Tz = -.1548e0,
            Dtx = 0.0001e0,
            Dty = -.0005e0,
            Dtz = -.0033e0,
            Rx = -.0001e0 / Constants.RHOSEC,
            Ry = 0.0e0,
            Rz = -0.00026e0 / Constants.RHOSEC,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = -.00002e0 / Constants.RHOSEC,
            Scale = 11.29e-9,
            DScale = 0.12e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF89
        //  tx[3] = 0.0304e0;
        //ty[3] = 0.0355e0;
        //tz[3] = -.1308e0;
        //dtx[3] = 0.0001e0;    
        //dty[3] = -.0005e0;
        //dtz[3] = -.0033e0;
        //rx[3] = 0.0e0;
        //ry[3] = 0.0e0;                
        //rz[3] = -.00026e0 / RHOSEC;
        //drx[3] = 0.0e0;
        //dry[3] = 0.0e0;
        //drz[3] = -.00002e0 / RHOSEC;
        //scale[3] = 8.19e-9;
        //dscale[3] = 0.12e-9;
        //refepc[3] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_1989 = new TransformationParameters()
        {
            Tx = 0.0304e0,
            Ty = 0.0355e0,
            Tz = -.1308e0,
            Dtx = 0.0001e0,
            Dty = -.0005e0,
            Dtz = -.0033e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = -.00026e0 / Constants.RHOSEC,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = -.00002e0 / Constants.RHOSEC,
            Scale = 8.19e-9,
            DScale = 0.12e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF90
        //tx[4] = 0.0254e0;
        //ty[4] = 0.0115e0;
        //tz[4] = -.0928e0;
        //dtx[4] = 0.0001e0;    
        //dty[4] = -.0005e0;
        //dtz[4] = -.0033e0;
        //rx[4] = 0.0e0;
        //ry[4] = 0.0e0;                 
        //rz[4] = -.00026e0 / RHOSEC; 
        //drx[4] = 0.0e0;                  
        //dry[4] = 0.0e0;              
        //drz[4] = -.00002e0 / RHOSEC;
        //scale[4] = 4.79e-9;
        //dscale[4] = 0.12e-9;
        //refepc[4] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_1990 = new TransformationParameters()
        {
            Tx = 0.0254e0,
            Ty = 0.0115e0,
            Tz = -.0928e0,
            Dtx = 0.0001e0,
            Dty = -.0005e0,
            Dtz = -.0033e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = -.00026e0 / Constants.RHOSEC,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = -.00002e0 / Constants.RHOSEC,
            Scale = 4.79e-9,
            DScale = 0.12e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF91
        //tx[5] = 0.0274e0;
        //ty[5] = 0.0155e0;
        //tz[5] = -.0768e0;
        //dtx[5] = 0.0001e0;    
        //dty[5] = -.0005e0;
        //dtz[5] = -.0033e0;
        //rx[5] = 0.0e0;
        //ry[5] = 0.0e0;                
        //rz[5] = -.000026e0 /RHOSEC; 
        //drx[5] = 0.0e0;                  
        //dry[5] = 0.0e0;              
        //drz[5] = -.00002e0 / RHOSEC;
        //scale[5] = 4.49e-9;
        //dscale[5] = 0.12e-9;
        //refepc[5] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_1991 = new TransformationParameters()
        {
            Tx = 0.0274e0,
            Ty = 0.0155e0,
            Tz = -.0768e0,
            Dtx = 0.0001e0,
            Dty = -.0005e0,
            Dtz = -.0033e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = -.000026e0 / Constants.RHOSEC,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = -.00002e0 / Constants.RHOSEC,
            Scale = 4.49e-9,
            DScale = 0.12e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF92
        //tx[6] = 0.0154e0;
        //ty[6] = 0.0015e0;
        //tz[6] = -.0708e0;
        //dtx[6] = 0.0001e0;    
        //dty[6] = -.0005e0;
        //dtz[6] = -.0033e0;
        //rx[6] = 0.0e0;
        //ry[6] = 0.0e0;                 
        //rz[6] = -.00026e0 / RHOSEC;
        //drx[6] = 0.0e0;                  
        //dry[6] = 0.0e0;              
        //drz[6] = -.00002e0 / RHOSEC;
        //scale[6] = 3.09e-9;
        //dscale[6] = 0.12e-9;
        //refepc[6] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_1992 = new TransformationParameters()
        {
            Tx = 0.0154e0,
            Ty = 0.0015e0,
            Tz = -.0708e0,
            Dtx = 0.0001e0,
            Dty = -.0005e0,
            Dtz = -.0033e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = -.00026e0 / Constants.RHOSEC,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = -.00002e0 / Constants.RHOSEC,
            Scale = 3.09e-9,
            DScale = 0.12e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF93
        //tx[7] = -.0504e0;
        //ty[7] = 0.0033e0;
        //tz[7] = -.0602e0;
        //dtx[7] = -.0028e0;
        //dty[7] = -.0001e0;
        //dtz[7] = -.0025e0;
        //rx[7] = 0.00281e0 / RHOSEC;
        //ry[7] = 0.00338e0 / RHOSEC;
        //rz[7] = -.00040e0 / RHOSEC;
        //drx[7] = .00011e0 / RHOSEC;
        //dry[7] = .00019e0 / RHOSEC;
        //drz[7] =-.00007e0 / RHOSEC;
        //scale[7] = 4.29e-9;
        //dscale[7] = 0.12e-9;
        //refepc[7] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_1993 = new TransformationParameters()
        {
            Tx = -.0504e0,
            Ty = 0.0033e0,
            Tz = -.0602e0,
            Dtx = -.0028e0,
            Dty = -.0001e0,
            Dtz = -.0025e0,
            Rx = 0.00281e0 / Constants.RHOSEC,
            Ry = 0.00338e0 / Constants.RHOSEC,
            Rz = -.00040e0 / Constants.RHOSEC,
            Drx = .00011e0 / Constants.RHOSEC,
            Dry = .00019e0 / Constants.RHOSEC,
            Drz = -.00007e0 / Constants.RHOSEC,
            Scale = 4.29e-9,
            DScale = 0.12e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF94 and ITRF96
        //tx[8] = 0.0074e0;
        //ty[8] = -.0005e0;
        //tz[8] = -.0628e0;
        //dtx[8] = 0.0001e0;
        //dty[8] = -.0005e0;
        //dtz[8] = -.0033e0;
        //rx[8] = 0.e0;
        //ry[8] = 0.e0;
        //rz[8] = -.00026e0 / RHOSEC;
        //drx[8] = 0.0e0;
        //dry[8] = 0.e0;
        //drz[8] = -.00002e0 / RHOSEC;
        //scale[8] = 3.80e-9;
        //dscale[8] = 0.12e-9;
        //refepc[8] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_1994_OR_1996 = new TransformationParameters()
        {
            Tx = 0.0074e0,
            Ty = -.0005e0,
            Tz = -.0628e0,
            Dtx = 0.0001e0,
            Dty = -.0005e0,
            Dtz = -.0033e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = -.00026e0 / Constants.RHOSEC,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = -.00002e0 / Constants.RHOSEC,
            Scale = 3.80e-9,
            DScale = 0.12e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF97 
        //tx[9] = 0.0074e0;
        //ty[9] = -.0005e0;
        //tz[9] = -.0628e0;
        //dtx[9] = 0.0001e0;
        //dty[9] = -.0005e0;
        //dtz[9] = -.0033e0;
        //rx[9] = 0.0e0; 
        //ry[9] = 0.0e0;
        //rz[9] = -.00026e0 / RHOSEC;
        //drx[9] = 0.0e0; 
        //dry[9] = 0.0e0; 
        //drz[9] = -0.00002e0 / RHOSEC;
        //scale[9] = 3.80e-9;
        //dscale[9] = 0.12e-9;
        //refepc[9] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_1997 = new TransformationParameters()
        {
            Tx = 0.0074e0,
            Ty = -.0005e0,
            Tz = -.0628e0,
            Dtx = 0.0001e0,
            Dty = -.0005e0,
            Dtz = -.0033e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = -.00026e0 / Constants.RHOSEC,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = -0.00002e0 / Constants.RHOSEC,
            Scale = 3.80e-9,
            DScale = 0.12e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF2014-PMM for North America
        //tx[10] = 0.0e0;
        //ty[10] = 0.0e0;
        //tz[10] = 0.0e0;
        //dtx[10] = 0.0e0;
        //dty[10] = 0.0e0;
        //dtz[10] = 0.0e0;
        //rx[10] = 0.0e0; 
        //ry[10] = 0.0e0;
        //rz[10] = 0.0e0;
        //drx[10] = +0.000024e0 / RHOSEC;
        //dry[10] = -0.000694e0 / RHOSEC;
        //drz[10] = -0.000063e0 / RHOSEC;
        //scale[10] = 0.0e-9;
        //dscale[10] = 0.0e-9;
        //refepc[10] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_2014_PMM_NORTH_AMERICA = new TransformationParameters()
        {
            Tx = 0.0e0,
            Ty = 0.0e0,
            Tz = 0.0e0,
            Dtx = 0.0e0,
            Dty = 0.0e0,
            Dtz = 0.0e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = 0.0e0,
            Drx = +0.000024e0 / Constants.RHOSEC,
            Dry = -0.000694e0 / Constants.RHOSEC,
            Drz = -0.000063e0 / Constants.RHOSEC,
            Scale = 0.0e-9,
            DScale = 0.0e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to ITRF2000
        //tx[11] = 0.0007e0;
        //ty[11] = 0.0012e0;
        //tz[11] = -.0261e0;
        //dtx[11] = 0.0001e0;
        //dty[11] = 0.0001e0;
        //dtz[11] = -0.0019e0;
        //rx[11] = 0.0e0; 
        //ry[11] = 0.0e0; 
        //rz[11] = 0.0e0; 
        //drx[11] = 0.0e0; 
        //dry[11] = 0.0e0;
        //drz[11] = 0.0e0; 
        //scale[11] = 2.12e-9;
        //dscale[11] = 0.11e-9;
        //refepc[11] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_2000 = new TransformationParameters()
        {
            Tx = 0.0007e0,
            Ty = 0.0012e0,
            Tz = -.0261e0,
            Dtx = 0.0001e0,
            Dty = 0.0001e0,
            Dtz = -0.0019e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = 0.0e0,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = 0.0e0,
            Scale = 2.12e-9,
            DScale = 0.11e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to PACP00 or PA11
        // *** Based on the rotation rate of the Pacific plate
        // ***   estimated by Beavan [2002]
        //tx[12] = 0.9109e0;
        //ty[12] = -2.0129e0;
        //tz[12] = -0.5863e0;
        //dtx[12] = 0.0001e0;
        //dty[12] = 0.0001e0;
        //dtz[12] = -.0019e0;
        //rx[12] = 0.022749e0 / RHOSEC;
        //ry[12] = 0.026560e0 / RHOSEC;
        //rz[12] = -.025706e0 / RHOSEC;
        //drx[12] = -.000344e0 / RHOSEC;
        //dry[12] = 0.001007e0 / RHOSEC;
        //drz[12] = -.002186e0 / RHOSEC;
        //scale[12] = 2.12e-9;
        //dscale[12] = 0.11e-9;
        //refepc[12] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_PACP_2000_OR_PA_2011 = new TransformationParameters()
        {
            Tx = 0.9109e0,
            Ty = -2.0129e0,
            Tz = -0.5863e0,
            Dtx = 0.0001e0,
            Dty = 0.0001e0,
            Dtz = -.0019e0,
            Rx = 0.022749e0 / Constants.RHOSEC,
            Ry = 0.026560e0 / Constants.RHOSEC,
            Rz = -.025706e0 / Constants.RHOSEC,
            Drx = -.000344e0 / Constants.RHOSEC,
            Dry = 0.001007e0 / Constants.RHOSEC,
            Drz = -.002186e0 / Constants.RHOSEC,
            Scale = 2.12e-9,
            DScale = 0.11e-9,
            RefEpoch = 2010.0e0
        };

        // *** From ITRF2014 to MARP00 or MA11
        // *** Based on the velocity of GUAM
        //tx[13] = 0.9109e0;
        //ty[13] = -2.0129e0;
        //tz[13] = -0.5863e0;
        //dtx[13] = 0.0001e0;
        //dty[13] = 0.0001e0;
        //dtz[13] = -.0019e0;
        //rx[13] = 0.028711e0 / RHOSEC;
        //ry[13] = 0.011785e0 / RHOSEC;
        //rz[13] = 0.004417e0 / RHOSEC;
        //drx[13] = -.000020e0 / RHOSEC;
        //dry[13] = 0.000105e0 / RHOSEC;
        //drz[13] = -.000347e0 / RHOSEC;
        //scale[13] = 2.12e-9;
        //dscale[13] = 0.11e-9;
        //refepc[13] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_MARP_2000_OR_MA_2011 = new TransformationParameters()
        {
            Tx = 0.9109e0,
            Ty = -2.0129e0,
            Tz = -0.5863e0,
            Dtx = 0.0001e0,
            Dty = 0.0001e0,
            Dtz = -.0019e0,
            Rx = 0.028711e0 / Constants.RHOSEC,
            Ry = 0.011785e0 / Constants.RHOSEC,
            Rz = 0.004417e0 / Constants.RHOSEC,
            Drx = -.000020e0 / Constants.RHOSEC,
            Dry = 0.000105e0 / Constants.RHOSEC,
            Drz = -.000347e0 / Constants.RHOSEC,
            Scale = 2.12e-9,
            DScale = 0.11e-9,
            RefEpoch = 2010.0e0
        };

        //*** From ITRF2014 to ITRF2005
        //tx[14] = 0.0026e0;
        //ty[14] = 0.0010e0;
        //tz[14] = -.0023e0;
        //dtx[14] = 0.0003e0;
        //dty[14] = 0.0000e0;
        //dtz[14] = -.0001e0;
        //rx[14] = 0.0e0; 
        //ry[14] = 0.0e0;
        //rz[14] = 0.0e0; 
        //drx[14] = 0.0e0;
        //dry[14] = 0.0e0;
        //drz[14] = 0.0e0;
        //scale[14] = 0.92e-9;
        //dscale[14] = 0.03e-9;
        //refepc[14] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_2005 = new TransformationParameters()
        {
            Tx = 0.0026e0,
            Ty = 0.0010e0,
            Tz = -.0023e0,
            Dtx = 0.0003e0,
            Dty = 0.0000e0,
            Dtz = -.0001e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = 0.0e0,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = 0.0e0,
            Scale = 0.92e-9,
            DScale = 0.03e-9,
            RefEpoch = 2010.0e0
        };

        //*** From ITRF2014 to ITRF2008 [also IGS08 and IGB08]
        //tx[15] = 0.0016e0;
        //ty[15] = 0.0019e0;
        //tz[15] = 0.0024e0;
        //dtx[15] = 0.0e0;
        //dty[15] = 0.0e0;
        //dtz[15] = -.0001e0;
        //rx[15] = 0.0e0; 
        //ry[15] = 0.0e0; 
        //rz[15] = 0.0e0;
        //drx[15] = 0.0e0; 
        //dry[15] = 0.0e0; 
        //drz[15] = 0.0e0; 
        //scale[15] = -0.02e-9;
        //dscale[15] = 0.03e-9;
        //refepc[15] = 2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_2008_OR_IGS08_OR_IGB08 = new TransformationParameters()
        {
            Tx = 0.0016e0,
            Ty = 0.0019e0,
            Tz = 0.0024e0,
            Dtx = 0.0e0,
            Dty = 0.0e0,
            Dtz = -.0001e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = 0.0e0,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = 0.0e0,
            Scale = -0.02e-9,
            DScale = 0.03e-9,
            RefEpoch = 2010.0e0
        };

        //*** From ITRF2014 to ITRF2014
        //tx[16]     = 0.0e0;
        //ty[16]     = 0.0e0;
        //tz[16]     = 0.0e0;
        //dtx[16]    = 0.0e0;                 
        //dty[16]    = 0.0e0;                 
        //dtz[16]    = 0.0e0;                 
        //rx[16]     = 0.0e0; 
        //ry[16]     = 0.0e0;
        //rz[16]     = 0.0e0; 
        //drx[16]    = 0.0e0;     
        //dry[16]    = 0.0e0;     
        //drz[16]    = 0.0e0;      
        //scale[16]  = 0.0e0;
        //dscale[16] =  0.0e0;               
        //refepc[16] =  2010.0e0;
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_2014 = new TransformationParameters()
        {
            Tx = 0.0e0,
            Ty = 0.0e0,
            Tz = 0.0e0,
            Dtx = 0.0e0,
            Dty = 0.0e0,
            Dtz = 0.0e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = 0.0e0,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = 0.0e0,
            Scale = 0.0e0,
            DScale = 0.0e0,
            RefEpoch = 2010.0e0
        };

        //*** From ITRF2014 to Plate Motion Model for Caribbean
        //tx(17) = 0.00d0
        //ty(17) = 0.00d0
        //tz(17) = 0.00d0
        //dtx(17) = 0.000d0
        //dty(17) = 0.000d0
        //dtz(17) = 0.000d0
        //rx(17) = 0.000d0
        //ry(17) =  0.000d0
        //rz(17) =  0.000d0
        //drx(17) = -0.000000000188d0 
        //dry(17) = -0.000000004730d0 
        //drz(17) = +0.000000002963d0 
        //scale(17) = 0.000d0
        //dscale(17) = 0.000d0
        //refepc(17) = 2010.0d0
        public static readonly TransformationParameters ITRF_2014_TO_CATRF_2022 = new TransformationParameters()
        {
            Tx = 0.00e0,
            Ty = 0.00e0,
            Tz = 0.00e0,
            Dtx = 0.000e0,
            Dty = 0.000e0,
            Dtz = 0.000e0,
            Rx = 0.000e0 / Constants.RHOSEC,
            Ry = 0.000e0 / Constants.RHOSEC,
            Rz = 0.000e0 / Constants.RHOSEC,
            Drx = -0.000000000188e0,
            Dry = -0.000000004730e0,
            Drz = +0.000000002963e0,
            Scale = 0.000e0,
            DScale = 0.000e0,
            RefEpoch = 2010.0e0
        };

        //*** From ITRF2014 to Plate Motion Model for Pacific

        //    tx(18) = 0.00d0
        //ty(18) = 0.00d0
        //tz(18) = 0.00d0
        //dtx(18) = 0.00d0
        //dty(18) = 0.00d0
        //rx(18) = 0.00d0
        //ry(18) = 0.00d0
        //rz(18) = 0.00d0
        //drx(18) = -0.000000001983d0
        //dry(18) = +0.000000005076d0 
        //drz(18) = -0.000000010516d0
        //scale(18) = 0.00d0
        //dscale(18) = 0.000d0
        //refepc(18) = 2010.0d0
        public static readonly TransformationParameters ITRF_2014_TO_PMM_PACIFIC = new TransformationParameters()
        {
            Tx = 0.00e0,
            Ty = 0.00e0,
            Tz = 0.00e0,
            Dtx = 0.00e0,
            Dty = 0.00e0,
            Dtz = 0.00e0,
            Rx = 0.00e0,
            Ry = 0.00e0,
            Rz = 0.00e0,
            Drx = -0.000000001983e0,
            Dry = +0.000000005076e0,
            Drz = -0.000000010516e0,
            Scale = 0.00e0,
            DScale = 0.000e0,
            RefEpoch = 2010.0e0
        };

        //    *** From ITRF2014 to Plate Motion Model for Mariana

        //    tx(19) = 0.00d0
        //ty(19) = 0.00d0
        //tz(19) = 0.00d0
        //dtx(19) = 0.00d0
        //dty(19) = 0.00d0
        //rx(19) = 0.00d0
        //ry(19) = 0.00d0
        //rz(19) = 0.00d0
        //drx(19) = -0.000000039217d0
        //dry(19) = +0.000000028783d0 
        //drz(19) = +0.000000010467d0
        //scale(19) = 0.00d0
        //dscale(19) = 0.000d0
        //refepc(19) = 2010.0d0
        public static readonly TransformationParameters ITRF_2014_TO_PMM_MARIANA = new TransformationParameters()
        {
            Tx = 0.00e0,
            Ty = 0.00e0,
            Tz = 0.00e0,
            Dtx = 0.00e0,
            Dty = 0.00e0,
            Dtz = 0.00e0,
            Rx = 0.00e0,
            Ry = 0.00e0,
            Rz = 0.00e0,
            Drx = -0.000000039217e0,
            Dry = +0.000000028783e0,
            Drz = +0.000000010467e0,
            Scale = 0.00e0,
            DScale = 0.000e0,
            RefEpoch = 2010.0e0
        };

        //    *** From ITRF2014 to Plate Motion Model for Bering
        //    tx(20) = 0.00d0
        //ty(20) = 0.00d0
        //tz(20) = 0.00d0
        //dtx(20) = 0.00d0
        //dty(20) = 0.00d0
        //rx(20) = 0.00d0
        //ry(20) = 0.00d0
        //rz(20) = 0.00d0
        //drx(20) = +0.000000001556d0
        //dry(20) = -0.000000003550d0 
        //drz(20) = -0.000000001981d0
        //scale(20) = 0.00d0
        //dscale(20) = 0.000d0
        //refepc(20) = 2010.0d0
        public static readonly TransformationParameters ITRF_2014_TO_PMM_BERING = new TransformationParameters()
        {
            Tx = 0.00e0,
            Ty = 0.00e0,
            Tz = 0.00e0,
            Dtx = 0.00e0,
            Dty = 0.00e0,
            Dtz = 0.00e0,
            Rx = 0.00e0,
            Ry = 0.00e0,
            Rz = 0.00e0,
            Drx = +0.000000001556e0,
            Dry = -0.000000003550e0,
            Drz = -0.000000001981e0,
            Scale = 0.00e0,
            DScale = 0.000e0,
            RefEpoch = 2010.0e0
        };

        //    *** From ITRF2014 to ITRF2020

        //tx(21) = 0.0014d0
        //ty(21) = 0.0014d0
        //tz(21) = -0.0024d0
        //dtx(21) = 0.0d0
        //dty(21) = 0.0001d0
        //dtz(21) = -0.0002d0
        //rx(21) = 0.0d0
        //ry(21) = 0.0d0
        //rz(21) = 0.0d0
        //drx(21) = 0.0d0
        //dry(21) = 0.0d0
        //drx(21) = 0.0d0
        //scale(21) = 0.00000000042d0
        //dscale(21) = 0.0d0
        //refepc(21) = 2010.00d0
        public static readonly TransformationParameters ITRF_2014_TO_ITRF_2020 = new TransformationParameters()
        {
            Tx = 0.0014e0,
            Ty = 0.0014e0,
            Tz = -0.0024e0,
            Dtx = 0.0e0,
            Dty = 0.0001e0,
            Dtz = -0.0002e0,
            Rx = 0.0e0,
            Ry = 0.0e0,
            Rz = 0.0e0,
            Drx = 0.0e0,
            Dry = 0.0e0,
            Drz = 0.0e0,
            Scale = 0.00000000042e0,
            DScale = 0.0e0,
            RefEpoch = 2010.00e0
        };


        public static readonly FortranArray<TransformationParameters> SupportedTransformations =
            new List<TransformationParameters>
            {
                ITRF_2014_TO_NAD_1983_2011,
                ITRF_2014_TO_ITRF_1988,
                ITRF_2014_TO_ITRF_1989,
                ITRF_2014_TO_ITRF_1990,
                ITRF_2014_TO_ITRF_1991,
                ITRF_2014_TO_ITRF_1992,
                ITRF_2014_TO_ITRF_1993,
                ITRF_2014_TO_ITRF_1994_OR_1996,
                ITRF_2014_TO_ITRF_1997,
                ITRF_2014_TO_ITRF_2014_PMM_NORTH_AMERICA,
                ITRF_2014_TO_ITRF_2000,
                ITRF_2014_TO_PACP_2000_OR_PA_2011,
                ITRF_2014_TO_MARP_2000_OR_MA_2011,
                ITRF_2014_TO_ITRF_2005,
                ITRF_2014_TO_ITRF_2008_OR_IGS08_OR_IGB08,
                ITRF_2014_TO_ITRF_2014,
                ITRF_2014_TO_CATRF_2022,
                ITRF_2014_TO_PMM_PACIFIC,
                ITRF_2014_TO_PMM_MARIANA,
                ITRF_2014_TO_PMM_BERING,
                ITRF_2014_TO_ITRF_2020
            }.ToFortranArray();

    }
}
