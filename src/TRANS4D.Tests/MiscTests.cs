namespace TRANS4D.Tests
{
    public class MiscTests
    {
        [Fact]
        public void Constants_AreCorrect()
        {
            Assert.Equal(6.378137e6, Constants.A);
            Assert.Equal(1.0 / 298.25722101, Constants.F);
            Assert.Equal(0.6694380022903146e-2, Constants.E2);
            Assert.Equal(6.378137e6 / (1.0 - 1.0 / 298.25722101), Constants.AF);
            Assert.Equal((1.0 / 298.25722101) * (2.0 - 1.0 / 298.25722101) / (Math.Pow((1.0 - 1.0 / 298.25722101), 2)), Constants.EPS);
            Assert.Equal(Math.PI, Constants.PI);
            Assert.Equal(180.0 / Math.PI, Constants.DEGREES_PER_RADIAN);
            Assert.Equal((180.0 * 3600.0) / Math.PI, Constants.RHOSEC);
            Assert.Equal(2.0 * Math.PI, Constants.TWOPI);
        }

        [Fact]
        public void ToRadians_Extension()
        {
            Assert.Equal(0.0, 0.0.ToRadians());
            Assert.Equal(Math.PI, 180.0.ToRadians());
            Assert.Equal(2.0 * Math.PI, 360.0.ToRadians());
        }

        [Fact]
        public void ToDegrees_Extension()
        {
            Assert.Equal(0.0, 0.0.ToDegrees());
            Assert.Equal(180.0, Math.PI.ToDegrees());
            Assert.Equal(360.0, 2.0 * Math.PI.ToDegrees());
        }

        [Fact]
        public void DecimalDegreesToDms()
        {
            var dms = Utilities.DecimalDegreesToDms(40);
            Assert.Equal(40, dms.degrees);
            Assert.Equal(0, dms.minutes);
            Assert.Equal(0, dms.seconds);

            dms = Utilities.DecimalDegreesToDms(40.5);
            Assert.Equal(40, dms.degrees);
            Assert.Equal(30, dms.minutes);
            Assert.Equal(0, dms.seconds);

            dms = Utilities.DecimalDegreesToDms(40.75);
            Assert.Equal(40, dms.degrees);
            Assert.Equal(45, dms.minutes);
            Assert.Equal(0, dms.seconds);

            dms = Utilities.DecimalDegreesToDms(40.125);
            Assert.Equal(40, dms.degrees);
            Assert.Equal(7, dms.minutes);
            Assert.Equal(30, dms.seconds);
        }

        [Fact]
        public void NegativeDecimalDegreesToDms()
        {
            var dms = Utilities.DecimalDegreesToDms(-40);
            Assert.Equal(-40, dms.degrees);
            Assert.Equal(0, dms.minutes);
            Assert.Equal(0, dms.seconds);

            dms = Utilities.DecimalDegreesToDms(-40.5);
            Assert.Equal(-40, dms.degrees);
            Assert.Equal(-30, dms.minutes);
            Assert.Equal(0, dms.seconds);

            dms = Utilities.DecimalDegreesToDms(-40.75);
            Assert.Equal(-40, dms.degrees);
            Assert.Equal(-45, dms.minutes);
            Assert.Equal(0, dms.seconds);

            dms = Utilities.DecimalDegreesToDms(-40.125);
            Assert.Equal(-40, dms.degrees);
            Assert.Equal(-7, dms.minutes);
            Assert.Equal(-30, dms.seconds);
        }   

        [Fact]
        public void DmsToDecimalDegrees()
        {
            Assert.Equal(40.0, Utilities.DmsToDecimalDegrees(40, 0, 0));
            Assert.Equal(40.5, Utilities.DmsToDecimalDegrees(40, 30, 0));
            Assert.Equal(40.75, Utilities.DmsToDecimalDegrees(40, 45, 0));
            Assert.Equal(40.125, Utilities.DmsToDecimalDegrees(40, 7, 30));
        }

        [Fact]
        public void GeodeticCoordinates_DefaultTo_Zero()
        {
            var geodeticCoordinates = new GeodeticCoordinates();
            Assert.Equal(0.0, geodeticCoordinates.Latitude);
            Assert.Equal(0.0, geodeticCoordinates.Longitude);
            Assert.Equal(0.0, geodeticCoordinates.Height);
            Assert.Equal(0.0, geodeticCoordinates.LatitudeDegrees);
            Assert.Equal(0.0, geodeticCoordinates.LongitudeDegrees);
        }
    }
}
