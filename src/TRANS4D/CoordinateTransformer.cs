using System;

namespace TRANS4D
{
    public static class CoordinateTransformer
    {
        /// <summary>
        /// Convert geodetic coordinates (latitude, longitude, height) with respect to the given ellipsoid
        /// to cartesian (x, y, z) coordinates.
        /// </summary>
        /// <param name="ellipsoid"></param>
        /// <param name="geodeticCoordinates"></param>
        /// <returns></returns>
        public static CartesianCoordinates GeodeticToCartesian(
            this Ellipsoid ellipsoid, GeodeticCoordinates geodeticCoordinates)
        {
            var latitude = geodeticCoordinates.Latitude;
            var longitude = geodeticCoordinates.Longitude;
            var ellipsoidHeight = geodeticCoordinates.Height;
            var E2 = ellipsoid.E2;
            var A = ellipsoid.A;

            double sinLat = Math.Sin(latitude);
            double cosLat = Math.Cos(latitude);
            double w = Math.Sqrt(1.0e0 - E2 * sinLat * sinLat);
            double en = A / w;

            double x = (en + ellipsoidHeight) * cosLat * Math.Cos(longitude);
            double y = (en + ellipsoidHeight) * cosLat * Math.Sin(longitude);
            double z = (en * (1.0e0 - E2) + ellipsoidHeight) * sinLat;

            return new CartesianCoordinates(x, y, z);
        }

        public static (GeodeticCoordinates, bool succeeded)
            XYZToLatLongHeight(this Ellipsoid ellipsoid, CartesianCoordinates cartesianCoordinates)
        {
            var E2 = ellipsoid.E2;
            var A = ellipsoid.A;
            /*
               bool frmxyz;
               // *** convert x,y,z into geodetic lat, lon, and ellip. ht
               // *** ref: eq a.4b, p. 132, appendix a, osu #370
               // *** ref: geom geod notes gs 658, rapp
            */
            var x = cartesianCoordinates.X;
            var y = cartesianCoordinates.Y;
            var z = cartesianCoordinates.Z;

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
                return (new GeodeticCoordinates(double.NaN, double.NaN, double.NaN),
                    false);
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

            return (new GeodeticCoordinates(glat, glon, eht), true);

        }
    }
}
