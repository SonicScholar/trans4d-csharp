using System;
using System.Collections.Generic;
using System.Text;
using TRANS4D.Compatibility;

namespace TRANS4D
{
    //todo: idea. create a Attribute class that can be used to decorate the built
    //in transformatino parameters. It would have a From and To property that would
    //take a Datum enum value. This would then register that tranformation with some
    //kind of factory that would be used to look up the transformation parameters.
    //All of the transforms work by using ITRF2014 as an intermediate datum.
    //i.e. if you wanted to transform from NAD83 to ITRF2008, you would first
    //transform from NAD83 to ITRF2014, then from ITRF2014 to ITRF2008.
    //I'm imagining a function that would take a from and to datum and return
    //a tuple of the from and to transformation parameters.


    public class TransformationParameters
    {
        public TransformationParameters(
            double tx, double ty, double tz,
            double dtx, double dty, double dtz,
            double rx, double ry, double rz,
            double drx, double dry, double drz,
            double scale, double dscale, double refEpoch)
        {
            Tx = tx;
            Ty = ty;
            Tz = tz;
            Dtx = dtx;
            Dty = dty;
            Dtz = dtz;
            Rx = rx;
            Ry = ry;
            Rz = rz;
            Drx = drx;
            Dry = dry;
            Drz = drz;
            Scale = scale;
            DScale = dscale;
            RefEpoch = refEpoch;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="coordinates"></param>
        /// <param name="epochYear"></param>
        /// <returns></returns>
        public XYZ Transform(XYZ coordinates, double epochYear)
            => Transform(coordinates, epochYear, false);


        public XYZ TransformInverse(XYZ coordinates, double epochYear)
            => Transform(coordinates, epochYear, true);

        public TransformationParameters GetInverse()
        {
            return new TransformationParameters(
                               -Tx, -Ty, -Tz,
                              -Dtx, -Dty, -Dtz,
                              -Rx, -Ry, -Rz,
                              -Drx, -Dry, -Drz,
                              -Scale, -DScale, RefEpoch);
        }

        /// <summary>
        /// Performs a 14-parameter transformation on cartesian coordinates using the parameters in this object.
        /// </summary>
        /// <param name="coordinates">input xyz coordinate</param>
        /// <param name="epochYear">decimal epoch in years of input coordinates</param>
        /// <param name="inverse">If true, applies the inverse direction</param>
        /// <returns>x,y,z tuple of the transformed coordinates</returns>
        internal XYZ Transform(XYZ coordinates, double epochYear, bool inverse)
        {
            double xIn = coordinates.X;
            double yIn = coordinates.Y;
            double zIn = coordinates.Z;

            var xform = inverse ? GetInverse() : this;

            double yearTimeDiff = epochYear - RefEpoch;
            double xTranslation = xform.Tx + xform.Dtx * yearTimeDiff;
            double yTranslation = xform.Ty + xform.Dty * yearTimeDiff;
            double zTranslation = xform.Tz + xform.Dtz * yearTimeDiff;
            double xRotation = xform.Rx + xform.Drx * yearTimeDiff;
            double yRotation = xform.Ry + xform.Dry * yearTimeDiff;
            double zRotation = xform.Rz + xform.Drz * yearTimeDiff;
            double scaleDelta = xform.Scale + xform.DScale * yearTimeDiff;

            double ds = 1.0 + scaleDelta;

            double x2 = xTranslation + ds * xIn + zRotation * yIn - yRotation * zIn;
            double y2 = yTranslation - zRotation * xIn + ds * yIn + xRotation * zIn;
            double z2 = zTranslation + yRotation * xIn - xRotation * yIn + ds * zIn;

            return new XYZ(x2, y2, z2);
        }
        /// <summary>
        /// Translation in X (meters)
        /// </summary>
        public double Tx { get; }

        /// <summary>
        /// Translation in Y (meters)
        /// </summary>
        public double Ty { get; }

        /// <summary>
        /// Translation in Z (meters)
        /// </summary>
        public double Tz { get; }

        /// <summary>
        /// Translation in X over time (meters/year)
        /// </summary>
        public double Dtx { get; }

        /// <summary>
        /// Translation in Y over time (meters/year)
        /// </summary>
        public double Dty { get; }

        /// <summary>
        /// Translation in Z over time (meters/year)
        /// </summary>
        public double Dtz { get; }

        /// <summary>
        /// Rotation about X (radians)
        /// </summary>
        public double Rx { get; }

        /// <summary>
        /// Rotation about Y (radians)
        /// </summary>
        public double Ry { get; }

        /// <summary>
        /// Rotation about Z (radians)
        /// </summary>
        public double Rz { get; }

        /// <summary>
        /// Rotation about X over time (radians/year)
        /// </summary>
        public double Drx { get; }

        /// <summary>
        /// Rotation about Y over time (radians/year)
        /// </summary>
        public double Dry { get; }

        /// <summary>
        /// Rotation about Z over time (radians/year)
        /// </summary>
        public double Drz { get; }

        /// <summary>
        /// Scale
        /// </summary>
        public double Scale { get; }

        /// <summary>
        /// Change in scale over time (per year)
        /// </summary>
        public double DScale { get; }

        /// <summary>
        /// Reference epoch
        /// </summary>
        public double RefEpoch { get; }


        public static TransformationParameters ForDatum(Datum datum) =>
            BuiltInTransforms.SupportedTransformations[(int)datum];


    }

    public static class TransformationParametersHelper
    {
        /// <summary>
        /// This is used internally as an intermediate datum for all transformations.
        /// i.e. input datum -> ITRF2014 -> output datum
        /// This reduces the total # of transform permutations this library
        /// has to keep track of.
        /// </summary>
        /// <param name="datum">Specifies the datum to transform ITRF2014 coordinates into.</param>
        /// <returns>Transformation parameters to go from ITRF2014 to <c>datum</c></returns>
        /// <remarks>To go from <c>datum</c> to ITRF2014, call <see cref="TransformationParameters.TransformInverse"/></remarks>
        public static TransformationParameters GetTransformationParametersForItrf2014(this Datum datum) =>
            TransformationParameters.ForDatum(datum);

    }
}
