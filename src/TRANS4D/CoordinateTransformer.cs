using System;
using static TRANS4D.Ellipsoid;

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
        public static XYZ GeodeticToCartesian(
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

            return new XYZ(x, y, z);
        }

        public static (GeodeticCoordinates, bool succeeded)
            XYZToLatLongHeight(this Ellipsoid ellipsoid, XYZ xyz)
        {
            var E2 = ellipsoid.E2;
            var A = ellipsoid.A;
            /*
               bool frmxyz;
               // *** convert x,y,z into geodetic lat, lon, and ellip. ht
               // *** ref: eq a.4b, p. 132, appendix a, osu #370
               // *** ref: geom geod notes gs 658, rapp
            */
            var x = xyz.X;
            var y = xyz.Y;
            var z = xyz.Z;

            double w;
            double en;
            const int maxIterations = 10;
            const double tolerance = 1.0e-13;
            double ae2 = A * E2;

            // note: SonicScholar (Collin) 11.3.24
            //
            // If coordinate is on or very close (1cm pythagorean distance x/y to z axis)
            // consider returning +/- 90 degrees for latitude and 0 for longitude. x or y are not zero
            // then maybe get the longitude using the method below.
            // ex: lat = +/- pi/2, height = abs(z) - B.
            // this would only make sense to do if the result coordinate is more accurate than what the
            // method below would provide. I don't know for what domain of values of Z this would be
            // applicable for. But for this application it seems like anything within a few hundred km
            // of the ellipsoid surface would be sufficient.
            // A future iteration of this idea could do something like, check the z value, and depending
            // how extreme it is, find an appropriate pythagorean distance in the x/y plane to use as a
            // threshold. If the distance is less than that threshold, return the +/- 90 degrees for latitude

            // Another idea to check out for "mostly spherical" (whatever that means) ellipsoids like GRS80
            // and WGS84 is to assume a perfect sphere and use the formula for converting XYZ to lat/long
            // when Z values are sufficiently large. All these ideas need some mathematical rigor that I
            // don't have at the moment. But the big idea is to find some domain of values where we can
            // return a result that is "better" than the iterative method below.


            // compute initial estimate of reduced latitude  (eht=0)
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
            // This always seemed to converge, even with huge and small numbers for x,y,z
            // I wasn't sure if we needed to check for convergence, but then ran across a
            // case when this didn't converge when using 0,0,0 as the input xyz.
            // source: 
            // Clynch, James R. "Geodetic coordinate conversions." Naval Postgraduate School (2002)
            // https://www.oc.nps.edu/oc2902w/coord/coordcvt.pdf
            // "This converges in a few iterations (4 at most) to a few centimeters. This is for positions
            // even at earth satellite altitudes.
            if (!converged)
            {
                return (GeodeticCoordinates.Invalid,  false);
            }

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


        /// <summary>
        /// Converts X,Y,Z in specified datum to latitude and
        /// longitude (in radians) and height (meters) in ITRF2014
        /// datum with longitude positive west.
        /// </summary>
        /// <param name="xyz"></param>
        /// <param name="date"></param>
        /// <param name="ioptDatum"></param>
        /// <returns>
        /// A <see cref="GeodeticCoordinates"/> object containing the latitude, longitude, and height.
        /// </returns>
        /// <remarks>
        /// See subroutine <c>XTOITRF2014</c> from the original Trans4D source.
        /// </remarks>
        public static GeodeticCoordinates TransformToItrf2014(XYZ xyz, DateTime date, Datum ioptDatum)
        {
            XYZ itrf2014CoordinatesXyz;

            // Convert to cartesian coordinates in ITRF2014
            if (ioptDatum == Datum.ITRF2014)
            {
                itrf2014CoordinatesXyz = xyz;
            }
            else
            {
                //to_itrf2014(x, y, z, X1, Y1, Z1, DATE, IOPT);
                var transformationParameters = ioptDatum.GetTransformationParametersForItrf2014();
                itrf2014CoordinatesXyz = transformationParameters.TransformInverse(xyz, date.ToEpoch());
            }

            // Convert to geodetic coordinates

            //if (!FRMXYZ(X1, Y1, Z1, RLAT, ELON, EHT14))
            var (itrf2014CoordinatesGeodetic, success) = GRS80.XYZToLatLongHeight(itrf2014CoordinatesXyz);
            if (!success)
            {
                //todo: consider adding to a logging service instead of throwing an exception
                throw new InvalidOperationException("Failed to convert XYZ to geodetic coordinates.");
            }

            return itrf2014CoordinatesGeodetic;

            // todo: make sure whatever uses this knows it's getting it in pos. East. Maybe make conversion property
            //WLON = -ELON;
            //while (WLON < 0)
            //{
            //    WLON = WLON + TWOPI;
            //}
            throw new NotImplementedException();
        }
    }
}
