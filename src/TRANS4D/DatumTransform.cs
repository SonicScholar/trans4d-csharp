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


    public class DatumTransform
    {
        public (double x, double y, double z) TransForm(double xIn, double yIn, double zIn, double epochYear)
            => Transform(xIn, yIn, zIn, epochYear, false);


        public (double x, double y, double z) TransformInverse(double xIn, double yIn, double zIn, double epochYear)
            => Transform(xIn, yIn, zIn, epochYear, true);

        /// <summary>
        /// Performs a 14-parameter transformation on cartesian coordinates using the parameters in this object.
        /// </summary>
        /// <param name="xIn">input x coordinate</param>
        /// <param name="yIn">input y coordinate</param>
        /// <param name="zIn">input z coordinate</param>
        /// <param name="epochYear">decimal epoch in years of input coordinates</param>
        /// <param name="inverse">If true, applies the inverse direction</param>
        /// <returns>x,y,z tuple of the transformed coordinates</returns>
        internal (double x, double y, double z) Transform(double xIn, double yIn, double zIn, double epochYear, bool inverse)
        {
            double yearTimeDiff = epochYear - RefEpoch;
            double xTranslation = Tx + Dtx * yearTimeDiff;
            double yTranslation = Ty + Dty * yearTimeDiff;
            double zTranslation = Tz + Dtz * yearTimeDiff;
            double xRotation = Rx + Drx * yearTimeDiff;
            double yRotation = Ry + Dry * yearTimeDiff;
            double zRotation = Rz + Drz * yearTimeDiff;
            double scaleDelta = Scale + DScale * yearTimeDiff;
            if (inverse)
            {
                xTranslation = -xTranslation;
                yTranslation = -yTranslation;
                zTranslation = -zTranslation;
                xRotation = -xRotation;
                yRotation = -yRotation;
                zRotation = -zRotation;
                scaleDelta = -scaleDelta;
            }

            double ds = 1.0 + scaleDelta;

            double x2 = xTranslation + ds * xIn + zRotation * yIn - yRotation * zIn;
            double y2 = yTranslation - zRotation * xIn + ds * yIn + xRotation * zIn;
            double z2 = zTranslation + yRotation * xIn - xRotation * yIn + ds * zIn;

            return (x2, y2, z2);
        }
        /// <summary>
        /// Translation in X (meters)
        /// </summary>
        public double Tx { get; set; }

        /// <summary>
        /// Translation in Y (meters)
        /// </summary>
        public double Ty { get; set; }

        /// <summary>
        /// Translation in Z (meters)
        /// </summary>
        public double Tz { get; set; }

        /// <summary>
        /// Translation in X over time (meters/year)
        /// </summary>
        public double Dtx { get; set; }

        /// <summary>
        /// Translation in Y over time (meters/year)
        /// </summary>
        public double Dty { get; set; }

        /// <summary>
        /// Translation in Z over time (meters/year)
        /// </summary>
        public double Dtz { get; set; }

        /// <summary>
        /// Rotation about X (radians)
        /// </summary>
        public double Rx { get; set; }

        /// <summary>
        /// Rotation about Y (radians)
        /// </summary>
        public double Ry { get; set; }

        /// <summary>
        /// Rotation about Z (radians)
        /// </summary>
        public double Rz { get; set; }

        /// <summary>
        /// Rotation about X over time (radians/year)
        /// </summary>
        public double Drx { get; set; }

        /// <summary>
        /// Rotation about Y over time (radians/year)
        /// </summary>
        public double Dry { get; set; }

        /// <summary>
        /// Rotation about Z over time (radians/year)
        /// </summary>
        public double Drz { get; set; }

        /// <summary>
        /// Scale
        /// </summary>
        public double Scale { get; set; }

        /// <summary>
        /// Change in scale over time (per year)
        /// </summary>
        public double DScale { get; set; }

        /// <summary>
        /// Reference epoch
        /// </summary>
        public double RefEpoch { get; set; }


        //todo: see if this is needed
        //public static DatumTransform ForDatum(Datum datum) =>
        //    SupportedTransformations[(int)datum];


    }

    //todo
    //public static class TransformationParametersHelper
    //{
    //    public static DatumTransform GetTransformationParameters(this Datum datum) =>
    //        DatumTransform.ForDatum(datum);

    //}
}
