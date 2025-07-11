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
        public double E2 => (A * A - B * B) / (A * A);
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
        /// Computes the radius of curvature in the meridian and in the parallel at a given latitude (radians).
        /// </summary>
        /// <param name="latitude">Latitude in radians.</param>
        /// <param name="radiusMeridian">Output: radius of curvature in the meridian (meters).</param>
        /// <param name="radiusParallel">Output: radius of curvature in the parallel (meters).</param>
        public void GetRadii(double latitude, out double radiusMeridian, out double radiusParallel)
        {
            double cosLat = Math.Cos(latitude);
            double denom = Math.Sqrt(1.0 + EPS * cosLat * cosLat);
            radiusMeridian = AF / Math.Pow(denom, 3);
            radiusParallel = AF * cosLat / denom;
        }
    }
}
