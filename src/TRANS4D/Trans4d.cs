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

        public static GeodeticCoordinates TransformPosition(
            GeodeticCoordinates coordinates, DatumEpoch sourceDatumEpoch, DatumEpoch targetDatumEpoch)
        {
            int inDateMjdMinutes = sourceDatumEpoch.Epoch.ToModifiedJulianDateMinutes();
            int outDateMjdMinutes = targetDatumEpoch.Epoch.ToModifiedJulianDateMinutes();

            GeodeticCoordinates result = null;
            if (sourceDatumEpoch.Epoch == targetDatumEpoch.Epoch)
            {
                result = GeodeticCoordinates.FromRadians(coordinates.Latitude, coordinates.Longitude, coordinates.Height);
            }
            else
            {
                int jregn;
                double vn, ve, vu;
                
                // trans4d::PREDV(rlat, -rlon, eht, inDate, inOpt, jregn, vn, ve, vu);
                var velocityInfo = PredictVelocity(coordinates, sourceDatumEpoch.Epoch, sourceDatumEpoch.Datum);
                if (velocityInfo == VelocityInfo.Zero) return coordinates;

                //double dn, de, du; //output displacement values (unused)
                //trans4d::NEWCOR(rlat, -rlon, eht, inDateMINS, outDateMINS, rlat1, rlon1, eht1, dn, de, du, vn, ve, vu);
                //rlon1 = -rlon1;
            }
            double x, y, z;
            //trans4d::TOXYZ(rlat1, rlon1, eht1, x, y, z);
            //double x1, y1, z1;
            //trans4d::to_itrf2014(x, y, z, x1, y1, z1, outDate, inOpt);
            //double x2, y2, z2;
            //trans4d::from_itrf2014(x1, y1, z1, x2, y2, z2, outDate, outOpt);

            //if (!trans4d::FRMXYZ(x2, y2, z2, newLat, newLon, newEht))
            //    return;

            //// convert transformed coordinates from radians back to decimal degrees
            //// ellipsoid height should already be in meters
            //newLat = newLat * degPerRad;
            //newLon = newLon * degPerRad;

            throw new NotImplementedException();
        }


        /// <summary>
        /// Compute the ITRF2014 velocity at a point in mm/yr todo: ?
        /// </summary>
        /// <param name="coordinates">
        /// Geodetic coordinates in radians. (Positive east)
        /// </param>
        /// <param name="date">The reference epoch of the input coordinates</param>
        /// <param name="outputDatum">The datum to output the velocity in.</param>
        /// <returns></returns>
        public static VelocityInfo PredictVelocity(GeodeticCoordinates coordinates, DateTime date, Datum outputDatum)
        {
            // Predict velocity in output datum reference frame

            //TOXYZ(ylat, elon, eht, ref x, ref y, ref z);
            var cartesianCoords  = GRS80.GeodeticToCartesian(coordinates);

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




        //todo what is this for, and can we declare it as a literal?
        // 11/19 - I think this is just for looking up bluebook format things
        // likely don't need this. commenting out for now.
        // would save some processing time
        //internal static void SETRF()
        //{
        //    //*** From blue book identifier to HTDP indentifier
        //    //*** WGS 72 Precise
        //    IRFCON[1] = 1;

        //    //*** WGS 84 (orig) Precise (set  equal to NAD 83)
        //    IRFCON[2] = 1;

        //    //*** WGS 72 Broadcast
        //    IRFCON[3] = 1;

        //    //*** WGS 84 (orig) Broadcast (set equal to NAD 83)
        //    IRFCON[4] = 1;

        //    //*** ITRF89
        //    IRFCON[5] = 3;

        //    //*** PNEOS 90 or NEOS 91.25 (set equal to ITRF90)
        //    IRFCON[6] = 4;

        //    //*** NEOS 90 (set equal to ITRF90)
        //    IRFCON[7] = 4;

        //    //*** ITRF91
        //    IRFCON[8] = 5;

        //    //*** SIO/MIT 92.57 (set equal to ITRF91)
        //    IRFCON[9] = 5;

        //    //*** ITRF91
        //    IRFCON[10] = 5;

        //    //*** ITRF92
        //    IRFCON[11] = 6;

        //    //*** ITRF93
        //    IRFCON[12] = 7;

        //    //*** WGS 84 (G730) Precise (set equal to ITRF91)
        //    IRFCON[13] = 5;

        //    //*** WGS 84 (G730) Broadcast (set equal to ITRF91)
        //    IRFCON[14] = 5;

        //    //*** ITRF94
        //    IRFCON[15] = 8;

        //    //*** WGS 84 (G873) Precise  (set equal to ITRF94)
        //    IRFCON[16] = 8;

        //    //*** WGS 84 (G873) Broadcast (set equal to ITRF94)
        //    IRFCON[17] = 8;

        //    //*** ITRF96
        //    IRFCON[18] = 8;

        //    //*** ITRF97
        //    IRFCON[19] = 9;

        //    //*** IGS97
        //    IRFCON[20] = 9;

        //    //*** ITRF00
        //    IRFCON[21] = 11;

        //    //*** IGS00
        //    IRFCON[22] = 11;

        //    //*** WGS 84 (G1150)
        //    IRFCON[23] = 11;

        //    //*** IGb00
        //    IRFCON[24] = 11;

        //    //*** ITRF2005
        //    IRFCON[25] = 14;

        //    //*** IGS05
        //    IRFCON[26] = 14;

        //    //*** ITRF2008 or IGS08
        //    IRFCON[27] = 15;

        //    //*** IGb08
        //    IRFCON[28] = 15;

        //    //*** ITRF2014
        //    IRFCON[29] = 16;

        //    //*** From HTDP identifier to blue book identifier
        //    //*** NAD 83 (set equal to WGS 84 (transit))
        //    JRFCON[1] = 2;

        //    //*** ITRF88 (set equal to ITRF89)
        //    JRFCON[2] = 5;

        //    //*** ITRF89
        //    JRFCON[3] = 5;

        //    //*** ITRF90 (set equal to NEOS 90)
        //    JRFCON[4] = 7;

        //    //*** ITRF91
        //    JRFCON[5] = 8;

        //    //*** ITRF92
        //    JRFCON[6] = 11;

        //    //*** ITRF93
        //    JRFCON[7] = 12;

        //    //*** ITRF96 (= ITRF94)
        //    JRFCON[8] = 18;

        //    //*** ITRF97
        //    JRFCON[9] = 19;

        //    //*** NA12
        //    JRFCON[10] = 0;

        //    //*** ITRF00
        //    JRFCON[11] = 21;

        //    //*** NAD 83(PACP00) or NAD 83(PA11)
        //    JRFCON[12] = 2;

        //    //*** NAD 83(MARP00) or NAD 83(MA11)
        //    JRFCON[13] = 2;

        //    //*** ITRF2005 or IGS05
        //    JRFCON[14] = 26;

        //    //*** ITRF2008 or IGS08/IGb08
        //    JRFCON[15] = 27;

        //    //*** ITRF2014
        //    JRFCON[16] = 29;

        //    //*** NA_ICE-6G
        //    JRFCON[17] = 0;
        //}
        }
}
