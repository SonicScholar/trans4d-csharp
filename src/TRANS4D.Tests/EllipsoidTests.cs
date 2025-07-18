﻿namespace TRANS4D.Tests
{
    public class EllipsoidTests
    {
        private const int FromXYZTolerance = 7;
        private const int EHTTolerance = 2;

        [Fact]
        public void GRS80_AF_Is_Correct()
        {
            var grs80 = Ellipsoid.GRS80;
            Assert.Equal(6399593.6258640233, grs80.AF);
        }

        [Fact]
        public void GRS80_EPS_Is_Correct()
        {
            var grs80 = Ellipsoid.GRS80;
            Assert.Equal(0.0067394967754789573, grs80.EPS);
        }

        [Fact]
        public void GRS80_PrimeMeridianEquatorRadius_ToXYZ()
        {
            var grs80 = Ellipsoid.GRS80;

            //radians
            var geodeticCoords = GeodeticCoordinates.FromRadians(0, 0, 0);

            var result = grs80.GeodeticToCartesian(geodeticCoords);

            double semiMajor = grs80.A;
            Assert.Equal(6378137.0, semiMajor);
            Assert.Equal(semiMajor, result.X);
            Assert.Equal(0.0, result.Y);
            Assert.Equal(0.0, result.Z);
        }

        [Fact]
        public void GRS80_PrimeMeridianEquatorRadius_ToXYZ_WithHeight()
        {
            var grs80 = Ellipsoid.GRS80;

            //radians
            //double latitude = 0.0;
            //double longitude = 0.0;
            //double height = 100.0;
            var geodeticCoords = GeodeticCoordinates.FromRadians(0, 0, 100.0);
            var result = grs80.GeodeticToCartesian(geodeticCoords);

            double semiMajor = grs80.A;
            Assert.Equal(6378137.0, semiMajor);
            Assert.Equal(semiMajor + geodeticCoords.Height, result.X);
            Assert.Equal(0.0, result.Y);
            Assert.Equal(0.0, result.Z);
        }

        [Fact]
        public void GRS80_XYZToLatLongHeight_LatitudeOver45Degrees()
        {
            var grs80 = Ellipsoid.GRS80;

            var geodeticCoords = GeodeticCoordinates.FromRadians(50.0.ToRadians(), 0.0, 0.0);
            var cartesianCoords = grs80.GeodeticToCartesian(geodeticCoords);

            var (result, succeeded) = grs80.XYZToLatLongHeight(cartesianCoords);

            Assert.True(succeeded);
            Assert.Equal(50.0.ToRadians(), result.Latitude);
            Assert.Equal(0.0, result.Longitude);
            Assert.Equal(0.0, result.Height, 8);
        }

        [Fact]
        public void GRS80_XYZToLatLongHeight_NonConverging()
        {
            var grs80 = Ellipsoid.GRS80;
            //way the heck out there in space... like orders of magnitude bigger than our observable universe
            double eht = Math.Pow(Math.Sqrt(double.MaxValue), 1.001);
            var geodeticCoords = GeodeticCoordinates.FromRadians(45.0.ToRadians(), 45.0.ToRadians(), eht);

            var cartesianCoords = grs80.GeodeticToCartesian(geodeticCoords);
            var (result, succeeded) = grs80.XYZToLatLongHeight(cartesianCoords);

            Assert.False(succeeded);
            Assert.Equal(double.NaN, result.Latitude);
            Assert.Equal(double.NaN, result.Longitude);
            Assert.Equal(double.NaN, result.Height);
        }

        [Fact]
        public void GRS80_XYZToLatLong_Poles()
        {
            var grs80 = Ellipsoid.GRS80;
            var northPole = new XYZ(0.0, 0.0, grs80.B);
            var southPole = new XYZ(0.0, 0.0, -grs80.B);

            var (northResult, northSucceeded) = grs80.XYZToLatLongHeight(northPole);
            var (southResult, southSucceeded) = grs80.XYZToLatLongHeight(southPole);
            Assert.False(northSucceeded);
            Assert.False(southSucceeded);

        }

        //todo: test transform between two datums with different epochs, but doesn't use velocity model

        // Test data run from Trans4d 4.1 Fortran program
        /* TRANSFORMING POSITIONS FROM ITRF2020 or IGS20            (EPOCH = 01-01-2010 (2010.0000))
           TO NAD_83(2011/CORS96/2007)     (EPOCH = 01-01-2010 (2010.0000))

           INPUT COORDINATES   OUTPUT COORDINATES

           ITRF20_to_NAD83
           LATITUDE     40 00  0.00000 N     39 59 59.97972 N
           LONGITUDE   105 00  0.00000 W    104 59 59.95474 W
           ELLIP. HT.            1500.000            1500.865 m
           X                 -1266623.309        -1266622.548 m
           Y                 -4727102.545        -4727103.851 m
           Z                  4078949.754         4078949.830 m
        */

        [Fact]
        public void GRS80_WEST_US_ToXYZ()
        {
            var grs80 = Ellipsoid.GRS80;
            var geodeticCoords = GeodeticCoordinates.FromRadians(40.0.ToRadians(), -105.0.ToRadians(), 1500.0);
            var result = grs80.GeodeticToCartesian(geodeticCoords);
            Assert.Equal(-1266623.309, result.X, 3);
            Assert.Equal(-4727102.545, result.Y, 3);
            Assert.Equal(4078949.754, result.Z, 3);
        }

        [Fact]
        public void GRS80_WEST_US_ToLatLongHeight()
        {
            var grs80 = Ellipsoid.GRS80;
            var cartesianCoordinates = new XYZ(-1266623.309, -4727102.545, 4078949.754);
            var (result, succeeded) = grs80.XYZToLatLongHeight(cartesianCoordinates);

            Assert.True(succeeded);
            Assert.Equal(40.0, result.LatitudeDegrees, FromXYZTolerance);
            Assert.Equal(-105.0, result.LongitudeDegrees, FromXYZTolerance);
            Assert.Equal(1500.0, result.Height, EHTTolerance);
        }

        [Fact]
        public void GetRadii_AtEquatorAndPole_ReturnsExpectedValues()
        {
            var grs80 = Ellipsoid.GRS80;

            // At equator (latitude = 0)
            grs80.GetRadii(0.0, out double radMeridianEq, out double radParallelEq);

            // At pole (latitude = pi/2)
            grs80.GetRadii(Math.PI / 2, out double radMeridianPole, out double radParallelPole);

            // Corrected values for GRS80
            Assert.Equal(6335439.327083875, radMeridianEq, 6);   // meters
            Assert.Equal(6378137.0, radParallelEq, 6);            // meters
            Assert.Equal(6399593.625864022, radMeridianPole, 6);  // meters
            Assert.Equal(0.0, radParallelPole, 6);                // meters
        }


        [Fact]
        public void ConvertHorizontalVelocityToRadians_ProducesExpectedResults_SpecificCase()
        {
            var grs80 = Ellipsoid.GRS80;
            double rlat = 0.67059433985213446;
            double vn = -0.34065493498120536;
            double ve = 2.0075526460630884;
            grs80.ConvertHorizontalVelocityToRadians(rlat, vn, ve, out double vnr, out double ver);
            Assert.Equal(-5.3561353150016441e-11, vnr, 15);
            Assert.Equal(4.0123454747273557e-10, ver, 15);
        }
    }
}

