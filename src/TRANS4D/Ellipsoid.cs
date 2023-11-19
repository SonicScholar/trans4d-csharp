using System;
using System.Collections.Generic;
using System.Text;

namespace TRANS4D
{
    /// <summary>
    /// todo
    /// </summary>
    public class Ellipsoid
    {
        public static readonly Ellipsoid GRS80 = new Ellipsoid(6378137.0, 1.0 / 298.257222101);
        public static readonly Ellipsoid WGS84 = new Ellipsoid(6378137.0, 1.0 / 298.257223563);

        /// <summary>
        /// Semi-major axis
        /// </summary>
        public double A { get; }
        /// <summary>
        /// Flattening
        /// </summary>
        public double F { get; }
        /// <summary>
        /// Semi-minor axis
        /// </summary>
        public double B => A * (1 - F);
        /// <summary>
        /// First Eccentricity ? todo
        /// </summary>
        public double E2 => (A*A - B * B) / (A * A);
        /// <summary>
        /// ??? todo
        /// </summary>
        public double AF => A / (1.0 - F);
        /// <summary>
        /// ??? todo
        /// </summary>
        public double EPS => F * (2.0 - F) / (Math.Pow((1.0 - F), 2));

        /// <summary>
        /// Create an ellipsoid with the given semi-major axis and flattening.
        /// </summary>
        /// <param name="a">Semi-major axis length</param>
        /// <param name="f">Flattening</param>
        public Ellipsoid(double a, double f)
        {
            A = a;
            F = f;
        }

        /// <summary>
        /// todo
        /// </summary>
        /// <param name="latitude">Latitude in Radians</param>
        /// <param name="longitude">Longitude in Radians</param>
        /// <param name="ellipsoidHeight">Ellipsoidal Height in meters</param>
        /// <returns></returns>
        public (double x, double y, double z)
            LatLongHeightToXYZ(double latitude, double longitude, double ellipsoidHeight)
        {
            double sinLat = Math.Sin(latitude);
            double cosLat = Math.Cos(latitude);
            double w = Math.Sqrt(1.0e0 - E2 * sinLat * sinLat);
            double en = A / w;

            double x = (en + ellipsoidHeight) * cosLat * Math.Cos(longitude);
            double y = (en + ellipsoidHeight) * cosLat * Math.Sin(longitude);
            double z = (en * (1.0e0 - E2) + ellipsoidHeight) * sinLat;

            return (x, y, z);
        }
        
    }
}
