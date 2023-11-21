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


        public (double latitude, double longitude, double height, bool succeeded)
            XYZToLatLongHeight(double x, double y, double z)
        {
            /*
               bool frmxyz;
               // *** convert x,y,z into geodetic lat, lon, and ellip. ht
               // *** ref: eq a.4b, p. 132, appendix a, osu #370
               // *** ref: geom geod notes gs 658, rapp
            */

            double w;
            double en;
            const int maxIterations = 10;
            const double tolerance = 1.0e-13;
            double ae2 = A * E2;

            // compute initial estimate of reduced latitude  (eht=0)
            // if
            double p = Math.Sqrt(x * x + y * y);

            // this can happen if sqrt(x) or sqrt(y) is greater than sqrt(double.MaxValue)
            // but that would mean we're trying to find geodetic coordinates for a point that
            // is about 10^127 times larger than the width of the observable universe. So....
            if (double.IsInfinity(p)) //
            {
                return (double.NaN, double.NaN, double.NaN, false);
            }
            double tgla = z / p / (1.0 - E2);

            // iterate to convergence, or to max # iterations
            int icount = 0;
            bool converged = false;
            do
            {
                double tglax = tgla;
                tgla = z / (p - (ae2 / Math.Sqrt(1.0 + (1.0 - E2) * tgla * tgla)));
                icount++;
                converged |= (Math.Abs(tgla - tglax) <= tolerance);
            } while (!converged && icount <= maxIterations);

            // 11/20/2023 - CT - C# porting note
            // This always seems to converge, even with huge and small numbers for x,y,z
            // so I'm not sure if we need to check for convergence
            // source: 
            // Clynch, James R. "Geodetic coordinate conversions." Naval Postgraduate School (2002)
            // https://www.oc.nps.edu/oc2902w/coord/coordcvt.pdf
            // "This converges in a few iterations (4 at most) to a few centimeters. This is for positions
            // even at earth satellite altitudes.
            //if (!converged)
            //{
            //    return (double.NaN, double.NaN, double.NaN, false);
            //}

            // we are using the degree symbol [] to denote radians
            double glat = Math.Atan(tgla);
            double slat = Math.Sin(glat);
            double clat = Math.Cos(glat);
            double glon = Math.Atan2(y, x);
            w = Math.Sqrt(1.0 - E2 * slat * slat);
            en = A / w;
            double eht = 0.0;

            //Calculating the ellipsoidal height based on the latitude
            //If the latitude is less than 45°, use the formula p/cos(lat) - en
            //Else use the formula z/sin(lat) - en + E2 * en
            if (Math.Abs(glat) <= Math.PI / 4) //45°
            {
                eht = p / clat - en;
            }
            else
            {
                eht = z / slat - en + E2 * en;
            }

            return (glat, glon, eht, true);

        }
    }
}
