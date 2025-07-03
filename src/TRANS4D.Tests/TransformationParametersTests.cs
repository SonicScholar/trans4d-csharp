namespace TRANS4D.Tests
{
    public class TransformationParametersTests
    {
        [Fact]
        public void SupportedTransformationsCount_Is_21()
        {
            Assert.Equal(21, BuiltInTransforms.SupportedTransformations.Length);
        }

        [Fact]
        public void ITRF_2014_To_ITRF_2014_Is_AllZeros()
        {
            var parameters = BuiltInTransforms.ITRF_2014_TO_ITRF_2014;
            Assert.Equal(0.0, parameters.Tx);
            Assert.Equal(0.0, parameters.Ty);
            Assert.Equal(0.0, parameters.Tz);
            Assert.Equal(0.0, parameters.Rx);
            Assert.Equal(0.0, parameters.Ry);
            Assert.Equal(0.0, parameters.Rz);
            Assert.Equal(0.0, parameters.Scale);

            Assert.Equal(0.0, parameters.Dtx);
            Assert.Equal(0.0, parameters.Dty);
            Assert.Equal(0.0, parameters.Dtz);
            Assert.Equal(0.0, parameters.Drx);
            Assert.Equal(0.0, parameters.Dry);
            Assert.Equal(0.0, parameters.Drz);
            Assert.Equal(0.0, parameters.DScale);

            Assert.Equal(2010.0, parameters.RefEpoch);
        }

        /*
         *
          ITRF20_to_ITRF14
           LATITUDE   40 N
           LONGITUDE 105 W
           ELLIP HT. 1500m
           
           ****************************************
           New latitude   =  40  0  0.00002 N
           New longitude  = 105  0  0.00004 W
           New Ellip. Ht. =     1500.000 meters
           New X          = -1266623.310 meters
           New Y          = -4727102.544 meters
           New Z          =  4078949.754 meters
           ****************************************
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
         *
         */
        [Fact]
        public void WEST_US_ITRF14_To_NAD83_Is_Correct()
        {
            var transform = BuiltInTransforms.ITRF_2014_TO_NAD_1983_2011;
            double latitude = Utilities.DmsToDecimalDegrees(40, 0, 0.00002).ToRadians();
            double longitude = Utilities.DmsToDecimalDegrees(-105, -0, -0.00004).ToRadians();
            double height = 1500.0;
            var inputCoordinates = GeodeticCoordinates.FromRadians(latitude, longitude, height);
            
            var inputXyz = Ellipsoid.GRS80.GeodeticToCartesian(inputCoordinates);
            var result = transform.Transform(inputXyz, transform.RefEpoch); //2010.0

            Assert.Equal(-1266622.548, result.X, 3);
            Assert.Equal(-4727103.851, result.Y, 3);
            Assert.Equal(4078949.830, result.Z, 3);
        }

        //now test the inverse
        [Fact]
        public void WEST_US_NAD83_To_ITRF14_Is_Correct()
        {
            var transform = BuiltInTransforms.ITRF_2014_TO_NAD_1983_2011;
            double latitude = Utilities.DmsToDecimalDegrees(39, 59, 59.97972).ToRadians();
            double longitude = Utilities.DmsToDecimalDegrees(-104, -59, -59.95473).ToRadians();
            double height = 1500.865;
            var geodeticCoords = GeodeticCoordinates.FromRadians(latitude, longitude, height);

            var inputXyz = Ellipsoid.GRS80.GeodeticToCartesian(geodeticCoords);
            var result = transform.TransformInverse(inputXyz, transform.RefEpoch); //2010.0

            Assert.Equal(-1266623.310, result.X, 3);
            Assert.Equal(-4727102.544, result.Y, 4);
            Assert.Equal(4078949.754, result.Z, 4);
        }
    }
}
