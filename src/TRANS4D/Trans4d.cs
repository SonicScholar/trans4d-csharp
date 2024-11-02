using System;
using System.Runtime.CompilerServices;
using TRANS4D.BlockData;
using TRANS4D.Compatibility;

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

        public static (int latitude, int longitude, int ellipsoidHeight) 
            TransformPosition(double latitudeDegrees, double longitudeDegrees, double ellipsoidHeight,
                Datum inDatum, DateTime inDate, Datum outDatum, DateTime outDate)
        {
            int inDateMjdMinutes = inDate.ToModifiedJulianDateMinutes();
            int outDateMjdMinutes = outDate.ToModifiedJulianDateMinutes();

            double latitudeRadians = latitudeDegrees.ToRadians();
            double longitudeRadians = longitudeDegrees.ToRadians();

            //todo: confirm these variable names are meaningful
            double latitudeRadiansOut = 0;
            double longitudeRadiansOut = 0;
            double ellipsoidHeightOut = 0;
            if (inDate == outDate)
            {
                latitudeRadiansOut = latitudeRadians;
                longitudeRadiansOut = longitudeRadians;
                ellipsoidHeightOut = ellipsoidHeight;
            }
            else
            {
                int jregn;
                double vn, ve, vu;
                //PREDV() requires longitude in positive west
               // trans4d::PREDV(rlat, -rlon, eht, inDate, inOpt, jregn, vn, ve, vu);
                //if (jregn == 0)
                //    return;

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
        /// Geodetic coordinates in radians.
        /// </param>
        /// <param name="date"></param>
        /// <param name="ioptDatum"></param>
        /// <returns></returns>
        //todo: convert iopt int variable to Datum enum
        public VelocityInfo PredictVelocity(GeodeticCoordinates coordinates, DateTime date, Datum ioptDatum)
        {
            // Predict velocity in iopt reference frame

            // Convert ylon to east longitude
            // todo: remove --> double elon = -ylon; coordinates should be provided in positive east.

            // Initialize variables
            //TOXYZ(ylat, elon, eht, ref x, ref y, ref z);
            var cartesianCoords  = GRS80.GeodeticToCartesian(coordinates);

            var itrf2014Coordinates = new GeodeticCoordinates();

            // Check reference frame option
            if (ioptDatum == Datum.ITRF2014)
            {
                itrf2014Coordinates.Latitude = coordinates.Latitude;
                itrf2014Coordinates.Longitude = coordinates.Longitude;
            }
            else
            {
                //XTOITRF2014(x, y, z, ref rlat, ref rlon, ref eht2014, date, iopt);
                itrf2014Coordinates = TransformToItrf2014(cartesianCoords, date, ioptDatum);
            }

            // Get deformation region
            int? jregn;
            //GETREG(rlat, rlon, out int jregnTemp);
            int jregnTemp = 0; // Temporary assignment
            jregn = jregnTemp == 0 ? (int?)null : jregnTemp;

            if (jregn == null)
            {
                return new VelocityInfo(null, 0.0, 0.0, 0.0);
            }

            double vn = 0, ve = 0, vu = 0;
            double sn = 0, se = 0, su = 0; // Standard deviations of velocities (unused)
            //COMVEL(rlat, rlon, jregnTemp, out vn, out ve, out vu, out sn, out se, out su);

            // Convert velocity to reference frame if iopt != ITRF2014
            if (ioptDatum != Datum.ITRF2014)
            {
                double vx = 0, vy = 0, vz = 0;
                //TOVXYZ(ylat, elon, vn, ve, vu, ref vx, ref vy, ref vz);
                //VTRANF(x, y, z, ref vx, ref vy, ref vz, 16, iopt);
                //TOVNEU(ylat, elon, vx, vy, vz, ref vn, ref ve, ref vu);
            }

            return new VelocityInfo(jregn, vn, ve, vu);
        }

        //XTOITRF2014(x, y, z, ref rlat, ref rlon, ref eht2014, date, iopt);
        // Converts X,Y,Z in specified datum to latitude and
        // longitude (in radians) and height (meters) in ITRF2014
        // datum with longitude positive west.
        public static GeodeticCoordinates TransformToItrf2014(CartesianCoordinates cartesianCoords,
            DateTime date, Datum ioptDatum)
        {
            var result = TransformToItrf2014(
                cartesianCoords.X, cartesianCoords.Y, cartesianCoords.Z, date, (int)ioptDatum);

            throw new NotImplementedException();
        }

        public static (double latRadians, double westLongitudeRadians, double ellipsoidHeight)
            TransformToItrf2014(double x, double y, double z, DateTime date, int iopt)
        {
            double X1, Y1, Z1;
            double ELON;

            // Convert to cartesian coordinates in ITRF2014
            if (iopt == 16)
            {
                X1 = x;
                Y1 = y;
                Z1 = z;
            }
            else
            {
                //to_itrf2014(x, y, z, X1, Y1, Z1, DATE, IOPT);
            }

            //// Convert to geodetic coordinates
            //if (!FRMXYZ(X1, Y1, Z1, RLAT, ELON, EHT14))
            //    // C++ port, don't want to kill the app in the library code STOP(666);
            //    std::cout << "FRMXYZ failed! X:" << X << " Y:" << Y << " Z:" << Z << std::endl;

            //WLON = -ELON;
            //while (WLON < 0)
            //{
            //    WLON = WLON + TWOPI;
            //}
            throw new NotImplementedException();
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
