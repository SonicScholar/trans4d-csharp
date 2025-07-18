using System;
using System.Runtime.CompilerServices;
using TRANS4D.BlockData;
using TRANS4D.Compatibility;
using TRANS4D.Geometry;
using static TRANS4D.Ellipsoid;

[assembly: InternalsVisibleTo("TRANS4D.Tests")]

namespace TRANS4D
{
    public class Trans4d
    {
        const int REFCON_SIZE = 30;
        //todo what is this for?
        static readonly FortranArray<int> IRFCON = new FortranArray<int>(REFCON_SIZE);
        //todo what is this for?
        static readonly FortranArray<int> JRFCON = new FortranArray<int>(REFCON_SIZE);

        /// <summary>
        /// Reference epoch for ITRF calculations (1/1/2010)
        /// </summary>
        public static readonly DateTime ReferenceEpoch = new DateTime(2010, 1, 1);

        /// <summary>
        /// Compute new coordinates based on velocity displacement over time.
        /// This is the velocity movement portion of the legacy COMPSN function.
        /// </summary>
        /// <param name="coordinates">Input geodetic coordinates in radians</param>
        /// <param name="fromDate">Source date/epoch</param>
        /// <param name="toDate">Target date/epoch</param>
        /// <param name="velocity">Velocity information in mm/yr (North, East, Up)</param>
        /// <returns>New geodetic coordinates after applying velocity displacement</returns>
        public static GeodeticCoordinates ComputeNewCoordinatesFromVelocity(
            GeodeticCoordinates coordinates, DateTime fromDate, DateTime toDate, VelocityInfo velocity)
        {
            if (fromDate == toDate)
            {
                return GeodeticCoordinates.FromRadians(coordinates.Latitude, coordinates.Longitude, coordinates.Height);
            }

            double rlat = coordinates.Latitude;
            double rlon = coordinates.Longitude;
            double eht = coordinates.Height;

            double vn = velocity.VelocityNorth;
            double ve = velocity.VelocityEast;
            double vu = velocity.VelocityUp;

            // Convert velocities from mm/yr to radians/yr for horizontal components
            GRS80.ConvertHorizontalVelocityToRadians(rlat, vn, ve, out double vnr, out double ver);

            // Convert velocity from mm/yr to m/yr for vertical component
            double vur = vu / 1000.0;

            int fromDateMjdMinutes = fromDate.ToModifiedJulianDateMinutes();
            int toDateMjdMinutes = toDate.ToModifiedJulianDateMinutes();
            int itrefMjdMinutes = ReferenceEpoch.ToModifiedJulianDateMinutes();

            // Compute time difference in years 
            // Legacy used (date_minutes - itref_minutes) / (365.25 * 24 * 60)
            double yearDiffFrom = (fromDateMjdMinutes - itrefMjdMinutes) / (365.25 * 24.0 * 60.0);
            double yearDiffTo = (toDateMjdMinutes - itrefMjdMinutes) / (365.25 * 24.0 * 60.0);
            double deltaYears = yearDiffTo - yearDiffFrom;

            // Compute displacements
            double dn = vnr * deltaYears;  // North displacement in radians
            double de = ver * deltaYears;  // East displacement in radians  
            double du = vur * deltaYears;  // Up displacement in meters

            // Apply displacements to get new coordinates
            double rlat1 = rlat + dn;
            double rlon1 = rlon + de;
            double eht1 = eht + du;

            return GeodeticCoordinates.FromRadians(rlat1, rlon1, eht1);
        }

        public static GeodeticCoordinates TransformPosition(
            GeodeticCoordinates coordinates, DatumEpoch sourceDatumEpoch, DatumEpoch targetDatumEpoch)
        {
            GeodeticCoordinates sourceDatumTargetEpochCoordinates;
            if (sourceDatumEpoch.Epoch == targetDatumEpoch.Epoch)
            {
                //todo: don't return these
                // these coordinates are source datum and target epoch 
                sourceDatumTargetEpochCoordinates = coordinates;
            }
            else
            {
                // Get velocity information at the source coordinates and datum
                var velocityInfo = PredictVelocity(coordinates, sourceDatumEpoch.Epoch, sourceDatumEpoch.Datum);
                if (velocityInfo == VelocityInfo.Zero) return coordinates;

                // Apply velocity displacement (first part of COMPSN)
                sourceDatumTargetEpochCoordinates = ComputeNewCoordinatesFromVelocity(
                coordinates, sourceDatumEpoch.Epoch, targetDatumEpoch.Epoch, velocityInfo);
            }

            // TODO: Apply earthquake adjustments (second part of COMPSN)
            // TODO: Apply postseismic adjustments (third part of COMPSN)

            // Transform between reference frames if needed
            if (sourceDatumEpoch.Datum == targetDatumEpoch.Datum)
            {
                // Same datum, just return the epoch-adjusted coordinates
                return sourceDatumTargetEpochCoordinates;
            }

            // Convert to cartesian coordinates
            var cartesianCoords = GRS80.GeodeticToCartesian(sourceDatumTargetEpochCoordinates);

            // todo: bottle up step 1 and 2 into its own method which would take
            //   - coordinates
            //   - epoch of coordinates
            //   - source datum
            //   - target datum
            //   and return the coordinates in the target datum

            // Transform through ITRF2014
            // Step 1: Transform from source datum to ITRF201
            var itrf2014CartesianCoords = TransformToItrf2014Cartesian(cartesianCoords, targetDatumEpoch.Epoch, sourceDatumEpoch.Datum);

            // Step 2: Transform from ITRF2014 to target datum
            var targetCartesianCoords = TransformFromItrf2014Cartesian(itrf2014CartesianCoords, targetDatumEpoch.Epoch, targetDatumEpoch.Datum);

            // Convert back to geodetic coordinates
            var targetGeodeticResult = GRS80.XYZToLatLongHeight(targetCartesianCoords);
            if (!targetGeodeticResult.Item2)
            {
                throw new InvalidOperationException("Failed to convert target cartesian coordinates back to geodetic coordinates.");
            }

            return targetGeodeticResult.Item1;
        }

        /// <summary>
        /// Transform cartesian coordinates from the specified datum to ITRF2014.
        /// </summary>
        /// <param name="coordinates">Cartesian coordinates in the source datum</param>
        /// <param name="epoch">Epoch date</param>
        /// <param name="sourceDatum">Source datum</param>
        /// <returns>Cartesian coordinates in ITRF2014</returns>
        private static XYZ TransformToItrf2014Cartesian(XYZ coordinates, DateTime epoch, Datum sourceDatum)
        {
            if (sourceDatum == Datum.ITRF2014)
            {
                return coordinates;
            }

            var transformationParameters = sourceDatum.GetTransformationParametersForItrf2014();
            return transformationParameters.TransformInverse(coordinates, epoch.ToEpoch());
        }

        /// <summary>
        /// Transform cartesian coordinates from ITRF2014 to the specified datum.
        /// </summary>
        /// <param name="coordinates">Cartesian coordinates in ITRF2014</param>
        /// <param name="epoch">Epoch date</param>
        /// <param name="targetDatum">Target datum</param>
        /// <returns>Cartesian coordinates in the target datum</returns>
        private static XYZ TransformFromItrf2014Cartesian(XYZ coordinates, DateTime epoch, Datum targetDatum)
        {
            if (targetDatum == Datum.ITRF2014)
            {
                return coordinates;
            }

            var transformationParameters = targetDatum.GetTransformationParametersForItrf2014();
            return transformationParameters.Transform(coordinates, epoch.ToEpoch());
        }

        /// <summary>
        /// Compute the velocity at a point in mm/yr in the specified output datum.
        /// </summary>
        /// <param name="coordinates">
        /// Geodetic coordinates in radians. (Positive east)
        /// </param>
        /// <param name="date">The reference epoch of the input coordinates</param>
        /// <param name="outputDatum">The datum to output the velocity in.</param>
        /// <returns>
        /// A <see cref="VelocityInfo"/> object containing the velocity in the specified datum.
        /// </returns>
        public static VelocityInfo PredictVelocity(GeodeticCoordinates coordinates, DateTime date, Datum outputDatum)
        {
            // Predict velocity in output datum reference frame

            //TOXYZ(ylat, elon, eht, ref x, ref y, ref z);
            var cartesianCoords = GRS80.GeodeticToCartesian(coordinates);

            GeodeticCoordinates itrf2014GeodeticCoords;

            // Check reference frame option
            if (outputDatum == Datum.ITRF2014)
            {
                itrf2014GeodeticCoords = GeodeticCoordinates.FromRadians(coordinates.Latitude, coordinates.Longitude, 0);
            }
            else
            {
                //XTOITRF2014(x, y, z, ref rlat, ref rlon, ref eht2014, date, iopt);
                itrf2014GeodeticCoords = CoordinateTransformer.TransformToItrf2014(
                    cartesianCoords, date, outputDatum);
            }

            // Get deformation region
            //GETREG(rlat, rlon, out int jregnTemp);
            var region = RegionManager.GetBoundary(itrf2014GeodeticCoords);

            if (region == null)
            {
                return VelocityInfo.Zero;
            }

            //COMVEL(rlat, rlon, jregnTemp, out vn, out ve, out vu, out sn, out se, out su);
            var velocityInfo = region.GetVelocity(itrf2014GeodeticCoords);

            // Convert velocity to reference frame if iopt != ITRF2014
            if (outputDatum == Datum.ITRF2014)
            {
                return velocityInfo;
            }

            //TOVXYZ(ylat, elon, vn, ve, vu, ref vx, ref vy, ref vz);
            var vxyz = VelocityTransformer.ToXyz(coordinates, velocityInfo);

            //VTRANF(x, y, z, ref vx, ref vy, ref vz, 16, iopt);
            vxyz = VelocityTransformer.Transform(cartesianCoords, vxyz, Datum.ITRF2014, outputDatum);

            //TOVNEU(ylat, elon, vx, vy, vz, ref vn, ref ve, ref vu);
            var vneu = VelocityTransformer.ToNeu(coordinates, vxyz);
            return vneu;
        }

        /// <summary>
        /// Compute the ITRF2014 velocity at a point in mm/yr
        /// </summary>
        /// <param name="lat">latitude in radians</param>
        /// <param name="lon">longitude in radians</param>
        /// <param name="region"></param>
        /// <returns></returns>
        public static VelocityInfo ComputeVelocity(double lat, double lon, int region)
        {
            throw new NotImplementedException();
        }
    }
}
