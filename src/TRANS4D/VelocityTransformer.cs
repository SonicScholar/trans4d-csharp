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
    }
}
