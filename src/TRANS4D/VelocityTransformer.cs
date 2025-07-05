using System;

namespace TRANS4D
{
    public class VelocityTransformer
    {
        /*
         *      SUBROUTINE VTRANF(X,Y,Z,VX,VY,VZ, IOPT1, IOPT2)
         *      ... (see above for full Fortran logic)
         */
        public static XYZ Transform(XYZ inputCoordinate, XYZ inputVelocity, Datum inDatum, Datum outDatum)
        {
            if (inDatum == outDatum) return new XYZ(inputVelocity.X, inputVelocity.Y, inputVelocity.Z);

            // Step 1: From inDatum to ITRF2014 (inverse=true)
            var vItrf2014 = TransformInternal(inputCoordinate, inputVelocity, inDatum, true);
            // Step 2: From ITRF2014 to outDatum (inverse=false)
            var vOut = TransformInternal(inputCoordinate, vItrf2014, outDatum, false);
            return vOut;
        }

        public static XYZ TransformInternal(XYZ inXyz, XYZ inVelocity, Datum datum, bool inverse)
        {
            // This method applies the velocity transformation for a single datum (to or from ITRF2014)
            var p = datum.GetTransformationParametersForItrf2014();
            if (inverse)
            {
                p = p.GetInverse();
            }
            double x = inXyz.X;
            double y = inXyz.Y;
            double z = inXyz.Z;
            double vx = inVelocity.X / 1000.0;
            double vy = inVelocity.Y / 1000.0;
            double vz = inVelocity.Z / 1000.0;
            double wx = p.Drx;
            double wy = p.Dry;
            double wz = p.Drz;
            double ds = p.DScale;
            vx = vx + p.Dtx + ds * x + wz * y - wy * z;
            vy = vy + p.Dty - wz * x + ds * y + wx * z;
            vz = vz + p.Dtz + wy * x - wx * y + ds * z;
            vx *= 1000.0;
            vy *= 1000.0;
            vz *= 1000.0;
            return new XYZ(vx, vy, vz);
        }

        /// <summary>
        /// Converts velocity from ECEF (vx, vy, vz) to local tangent plane (vn, ve, vu) at the given geodetic coordinates.
        /// </summary>
        /// <param name="geo">Geodetic coordinates (lat, lon in radians).</param>
        /// <param name="ecefVelocity">Velocity in ECEF (vx, vy, vz).</param>
        /// <returns>Velocity in local tangent plane (vn, ve, vu) as XYZ (N, E, U).</returns>
        public static XYZ ToNeu(GeodeticCoordinates geo, XYZ ecefVelocity)
        {
            double lat = geo.Latitude;
            double lon = geo.Longitude;
            double vx = ecefVelocity.X;
            double vy = ecefVelocity.Y;
            double vz = ecefVelocity.Z;
            double slat = Math.Sin(lat);
            double clat = Math.Cos(lat);
            double slon = Math.Sin(lon);
            double clon = Math.Cos(lon);
            double vn = -slat * clon * vx - slat * slon * vy + clat * vz;
            double ve = -slon * vx + clon * vy;
            double vu = clat * clon * vx + clat * slon * vy + slat * vz;
            return new XYZ(vn, ve, vu);
        }

        /// <summary>
        /// Converts velocity from local tangent plane (vn, ve, vu) to ECEF (vx, vy, vz) at the given geodetic coordinates.
        /// </summary>
        /// <param name="geo">Geodetic coordinates (lat, lon in radians).</param>
        /// <param name="neuVelocity">Velocity in local tangent plane (vn, ve, vu).</param>
        /// <returns>Velocity in ECEF (vx, vy, vz) as XYZ.</returns>
        public static XYZ ToXyz(GeodeticCoordinates geo, XYZ neuVelocity)
        {
            double lat = geo.Latitude;
            double lon = geo.Longitude;
            double vn = neuVelocity.X;
            double ve = neuVelocity.Y;
            double vu = neuVelocity.Z;
            double slat = Math.Sin(lat);
            double clat = Math.Cos(lat);
            double slon = Math.Sin(lon);
            double clon = Math.Cos(lon);
            double vx = -slat * clon * vn - slon * ve + clat * clon * vu;
            double vy = -slat * slon * vn + clon * ve + clat * slon * vu;
            double vz = clat * vn + slat * vu;
            return new XYZ(vx, vy, vz);
        }
    }
}
