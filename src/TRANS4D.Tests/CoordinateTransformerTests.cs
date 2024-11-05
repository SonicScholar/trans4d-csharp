namespace TRANS4D.Tests
{
    public class CoordinateTransformerTests
    {
        [Fact]
        public void Transform_IntoItrf2014_XYZ_ToLatLongHeight_AtOrigin_ThrowsException()
        {
            var xyz = new XYZ(0, 0, 0);
            var inputDatum = Datum.ITRF2014;
            var inputDate = new DateTime(2014, 1, 1);

            Assert.Throws<InvalidOperationException>(
                () => CoordinateTransformer.TransformToItrf2014(xyz, inputDate, inputDatum));
        }

        [Fact]
        public void Transform_IntoItrf2014_XYZ_ToLatLongHeight_AtNorthPole_ThrowsExceptio()
        {
            var grs80 = Ellipsoid.GRS80;
            var xyz = new XYZ(0, 0, grs80.B);
            var inputDatum = Datum.ITRF2014;

            //input date is irrelevant since transformation parameters from ITRF2014 to ITRF2014
            var inputDate = new DateTime(2014, 1, 1);

            Assert.Throws<InvalidOperationException>(
                               () => CoordinateTransformer.TransformToItrf2014(xyz, inputDate, inputDatum));
        }

        [Fact]
        public void Transform_IntoItrf2014_XYZ_ToLatLongHeight_AtSouthPole_ThrowsException()
        {
            var grs80 = Ellipsoid.GRS80;
            var xyz = new XYZ(0, 0, -grs80.B);
            var inputDatum = Datum.ITRF2014;

            //input date is irrelevant since transformation parameters from ITRF2014 to ITRF2014
            var inputDate = new DateTime(2014, 1, 1);

            Assert.Throws<InvalidOperationException>(
                                              () => CoordinateTransformer.TransformToItrf2014(xyz, inputDate, inputDatum));
        }

        [Fact]
        public void Transform_IntoItrf2014_XYZ_ToLatLongHeigt_AtPrimeMeridian_and_SemiMajorAxis()
        {
            var grs80 = Ellipsoid.GRS80;
            var xyz = new XYZ(grs80.A, 0, 0);
            var inputDatum = Datum.ITRF2014;

            //input date is irrelevant since transformation parameters from ITRF2014 to ITRF2014
            var inputDate = new DateTime(2014, 1, 1);

            var result = CoordinateTransformer.TransformToItrf2014(xyz, inputDate, inputDatum);

            Assert.Equal(0.0, result.Latitude);
            Assert.Equal(0.0, result.Longitude);
            Assert.Equal(0.0, result.Height);
        }

        [Fact]
        public void Transform_IntoItrf2014_XYZ_ToLatLongHeight_AtPrimeMeridian_and_SemiMajorAxis_WithHeight()
        {
            var grs80 = Ellipsoid.GRS80;
            var xyz = new XYZ(grs80.A+100, 0, 0);
            var inputDatum = Datum.ITRF2014;

            //input date is irrelevant since transformation parameters from ITRF2014 to ITRF2014
            var inputDate = new DateTime(2014, 1, 1);

            var result = CoordinateTransformer.TransformToItrf2014(xyz, inputDate, inputDatum);

            Assert.Equal(0.0, result.Latitude);
            Assert.Equal(0.0, result.Longitude);
            Assert.Equal(100.0, result.Height);
        }

        [Fact]
        public void Transform_IntoItrf2014_XYZ_ToLatLongHeight_At90DegreesLongitude_and_SemiMajorAxis()
        {
            var grs80 = Ellipsoid.GRS80;
            var xyz = new XYZ(0, grs80.A, 0);
            var inputDatum = Datum.ITRF2014;

            //input date is irrelevant since transformation parameters from ITRF2014 to ITRF2014
            var inputDate = new DateTime(2014, 1, 1);

            var result = CoordinateTransformer.TransformToItrf2014(xyz, inputDate, inputDatum);

            Assert.Equal(0.0, result.Latitude);
            Assert.Equal(Math.PI / 2, result.Longitude);
            Assert.Equal(0.0, result.Height);
        }

        /*
         *
         * TRANSFORMING POSITIONS FROM ITRF2014 or IGS14            (EPOCH = 01-01-2010 (2010.0000))
           TO NAD_83(2011/CORS96/2007)     (EPOCH = 01-01-2010 (2010.0000))
           
           ITRF14_to_NAD83
           LATITUDE   40  0  0.00002 N
           LONGITUDE 105  0  0.00004 W
           ELLIP HT. 1500m
           
           ITRF14_to_NAD83         
           LATITUDE     40 00  0.00002 N     39 59 59.97972 N
           LONGITUDE   105 00  0.00004 W    104 59 59.95473 W
           ELLIP. HT.            1500.000            1500.865 m  
           X                 -1266623.310        -1266622.548 m  
           Y                 -4727102.544        -4727103.851 m  
           Z                  4078949.754         4078949.830 m  
         */

        [Fact]
        public void Transform_IntoItrf2014_From_WestUsNad83_Is_Corrct()
        {
            var inputXyz = new XYZ(-1266622.548, -4727103.851, 4078949.830);

            var inputDate = new DateTime(2010, 1, 1);
            var result = CoordinateTransformer.TransformToItrf2014(
                inputXyz, inputDate, Datum.Nad83_2011_or_CORS96);

            double expectedHeight = 1500.000;
            double expectedLatitude = Utilities.DmsToDecimalDegrees(40, 00, .00002);
            double expectedLongitude = Utilities.DmsToDecimalDegrees(-105,00,-.00004);

            Assert.Equal(expectedHeight, result.Height, precision:3);
            Assert.Equal(expectedLatitude, result.LatitudeDegrees, 7);
            Assert.Equal(expectedLongitude, result.LongitudeDegrees, 7);
        }
    }
}
