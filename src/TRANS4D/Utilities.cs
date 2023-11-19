using System;
using System.Collections.Generic;
using System.Text;

namespace TRANS4D
{
    public static class Utilities
    {
        //todo: determine if these are needed
        //public static double DABS(double x) { return Math.Abs(x);}
        ///* arctan */
        //public static double DATAN(double x) { return Math.Atan(x); }
        ///* arctan 2*/
        //public static double DATAN2(double y, double x) { return Math.Atan2(y, x); }
        ///* convert to double */
        //public static  double DBLE(int x) { return (double)x; }
        ///* cos */
        //public static double DCOS(double x) { return Math.Cos(x); }
        ///* Exponential */
        //public static  double DEXP(double x) { return Math.Exp(x); }
        ///* Truncation of Real */
        //public static double DINT(double x) { return (double)((int)x); }
        ///* Natural Log */
        //public static double DLOG(double x) { return Math.Log(x); }
        ///* sin */
        //public static double DSIN(double x) { return Math.Sin(x); }
        ///* square root */
        //public static double DSQRT(double x) { return Math.Sqrt(x); }
        ///* convert to int */
        //public static int IDINT(double x) { return (int)x; }

        ///* IF_ARITHMETIC */
        //public static int IF_ARITHMETIC(double x)
        //{
        //    if (x < 0) return -1;
        //    if (x == 0) return 0;
        //    return 1;
        //}

        ///* Utility functions ported from HTDP and TRANS4D */
        //public static int IYMDMJ(int IYR, int IMON, int IDAY)
        //{
        //    int MJD;
        //    // C NAME:       IYMDMJ
        //    // C VERSION:    Sep. 17, 2010
        //    // C WRITTEN BY: R. SNAY (after M. SCHENEWERK)
        //    // C PURPOSE:    CONVERT DATE TO MODIFIED JULIAN DATE 
        //    // C
        //    // C INPUT PARAMETERS FROM THE ARGUEMENT LIST:
        //    // C -----------------------------------------
        //    // C IDAY              DAY
        //    // C IMON              MONTH
        //    // C IYR               YEAR
        //    // C
        //    // C OUTPUT PARAMETERS FROM ARGUEMENT LIST:
        //    // C --------------------------------------
        //    // C MJD               MODIFIED JULIAN DATE 
        //    // C
        //    // C
        //    // C LOCAL VARIABLES AND CONSTANTS:
        //    // C ------------------------------
        //    // C A                 TEMPORARY STORAGE
        //    // C B                 TEMPORARY STORAGE
        //    // C C                 TEMPORARY STORAGE
        //    // C D                 TEMPORARY STORAGE
        //    // C IMOP              TEMPORARY STORAGE
        //    // C IYRP              TEMPORARY STORAGE
        //    // C
        //    // C GLOBAL VARIABLES AND CONSTANTS:
        //    // C ------------------------------
        //    // C
        //    // C
        //    // C       THIS MODULE CALLED BY: GENERAL USE
        //    // C
        //    // C       THIS MODULE CALLS:     DINT
        //    // C
        //    // C       INCLUDE FILES USED:
        //    // C
        //    // C       COMMON BLOCKS USED:       
        //    // C
        //    // C       REFERENCES:            DUFFETT-SMITH, PETER  1982, 'PRACTICAL
        //    // C                              ASTRONOMY WITH YOUR CALCULATOR', 2ND
        //    // C                              EDITION, CAMBRIDGE UNIVERSITY PRESS,
        //    // C                              NEW YORK, P.9
        //    // C
        //    // C       COMMENTS:              THIS SUBROUTINE REQUIRES THE FULL YEAR,
        //    // C                              I.E. 1992 RATHER THAN 92.  
        //    // C
        //    // C********1*********2*********3*********4*********5*********6*********7**
        //    // C::LAST MODIFICATION
        //    // C::8909.06, MSS, DOC STANDARD IMPLIMENTED
        //    // C::9004.17, MSS, CHANGE ORDER YY MM DD
        //    // C********1*********2*********3*********4*********5*********6*********7**
        //    int A, B, C, D;
        //    int IYRP = IYR;

        //    int IMOP;//
        //    if (IMON < 3)
        //    {
        //        IYRP = IYRP - 1;
        //        IMOP = IMON + 12;
        //    }
        //    else
        //    {
        //        IMOP = IMON;
        //    }
        //    // C
        //    // C........  1.0  CALCULATION
        //    // C
        //    A = (int)(IYRP * 0.01);
        //    B = (int)(2 - A + DINT(A * 0.25));
        //    C = (int)(365.25 * IYRP);
        //    D = (int)(30.6001 * (IMOP + 1));
        //    MJD = (B + C + D + IDAY - 679006);
        //    // C      
        //    return MJD;
        //}

        public static double ToRadians(this double degrees) => degrees * Math.PI / 180.0;

        public static double ToDegrees(this double radians) => radians * 180.0 / Math.PI;
    }
}
