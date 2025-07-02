namespace TRANS4D.Tests
{
    public class GeodeticCoordinatesTests
    {
        [Fact]
        public void Equals_Test_01()
        {
            var gc1 = GeodeticCoordinates.FromRadians(0, 0, 0);
            var gc2 = GeodeticCoordinates.FromRadians(0, 0, 0);
            Assert.True(gc1.Equals(gc2));
        }

        [Fact]
        public void Equals_Test_02()
        {
            var gc1 = GeodeticCoordinates.FromRadians(0, 0, 0);
            var gc2 = gc1;
            Assert.True(gc1.Equals(gc2));
        }

        [Fact]
        public void Equals_Test_03()
        {
            var gc1 = GeodeticCoordinates.FromRadians(0, 0, 0);
            var gc2 = GeodeticCoordinates.FromRadians(0, 0, 0) as object;
            Assert.True(gc1.Equals(gc2));
        }

        [Fact]
        public void NotEquals_Test01()
        {
            var gc1 = GeodeticCoordinates.FromRadians(0, 0, 0);
            var gc2 = GeodeticCoordinates.FromRadians(0, 0, 1);
            Assert.False(gc1.Equals(gc2));
        }

        [Fact]
        public void NotEquals_Test_02()
        {
            var gc1 = GeodeticCoordinates.FromRadians(0, 0, 0);
            var gc2 = new object();
            Assert.False(gc1.Equals(gc2));
        }

        [Fact]
        public void Normalize_LongitudeMinus90Degrees_Becomes270DegreesOr3PiOver2()
        {
            // -90 degrees = -pi/2 radians, expect 3pi/2 radians after normalization
            var coords = GeodeticCoordinates.FromDegrees(0, -90, 0);
            var normalized = coords.Normalize();
            double expected = 3 * Math.PI / 2;
            Assert.True(Math.Abs(normalized.Longitude - expected) < 1e-10, $"Expected {expected}, got {normalized.Longitude}");
        }

        [Fact]
        public void Normalize_LongitudeMinus180Degrees_Becomes180DegreesOrPi()
        {
            // -180 degrees = -pi radians, expect pi radians after normalization
            var coords = GeodeticCoordinates.FromDegrees(0, -180, 0);
            var normalized = coords.Normalize();
            double expected = Math.PI;
            Assert.True(Math.Abs(normalized.Longitude - expected) < 1e-10, $"Expected {expected}, got {normalized.Longitude}");
        }
    }
}
