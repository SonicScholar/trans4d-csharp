using System;
using System.Collections.Generic;
using System.Text;

namespace TRANS4D
{
    internal static class Constants
    {
        /*
           Original Fortran values
           A = 6.378137e6;
           F = 1.0 /298.25722101;
           E2 = 0.6694380022903146e-2;
           AF = A / (1.0 - F);
           EPS = F*(2.0 - F) / (pow((1.0 -F),2));
           PI = 4.0 * DATAN(1.0);
           RHOSEC = (180.0 * 3600.0) / PI;
           TWOPI = PI + PI;
         */

        /// <summary>
        /// GRS80 semi-major axis (m)
        /// </summary>
        public const double A = 6.378137e6;

        /// <summary>
        /// GRS80 flattening
        /// </summary>
        public const double F = 1.0 / 298.25722101;

        /// <summary>
        /// GRS80 first eccentricity squared
        /// </summary>
        public const double E2 = 0.6694380022903146e-2;

        /// <summary>
        /// GRS80 semi-minor axis (m)
        /// </summary>
        public const double AF = A / (1.0 - F);

        /// <summary>
        /// GRS80 second eccentricity squared
        /// </summary>
        public static readonly double EPS = F * (2.0 - F) / (Math.Pow((1.0 - F), 2));

        /// <summary>
        /// PI
        /// </summary>
        public const double PI = Math.PI; //4.0 * Math.Atan(1.0);

        public const double DEGREES_PER_RADIAN = 180.0 / PI;

        /// <summary>
        ///  The number of arcseconds in 1.0 radians
        /// </summary>
        public const double RHOSEC = (180.0 * 3600.0) / PI;

        /// <summary>
        /// Two * PI, of course.
        /// </summary>
        public const double TWOPI = PI + PI;
    }
}
