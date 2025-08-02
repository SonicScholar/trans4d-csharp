using System;
using System.Collections.Generic;
using System.Text;

namespace TRANS4D
{
    internal static class Constants
    {
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
