namespace TRANS4D.Tests
{
    public class GeodeticCoordinatesTests
    {
        [Fact]
        public void Equals_Test_01()
        {
            var gc1 = new GeodeticCoordinates(0, 0, 0);
            var gc2 = new GeodeticCoordinates(0, 0, 0);
            Assert.True(gc1.Equals(gc2));
        }

        [Fact]
        public void Equals_Test_02()
        {
            var gc1 = new GeodeticCoordinates(0, 0, 0);
            var gc2 = gc1;
            Assert.True(gc1.Equals(gc2));
        }

        [Fact]
        public void Equals_Test_03()
        {
            var gc1 = new GeodeticCoordinates(0, 0, 0);
            var gc2 = new GeodeticCoordinates(0, 0, 0) as object;
            Assert.True(gc1.Equals(gc2));
        }

        [Fact]
        public void NotEquals_Test01()
        {
            var gc1 = new GeodeticCoordinates(0, 0, 0);
            var gc2 = new GeodeticCoordinates(0, 0, 1);
            Assert.False(gc1.Equals(gc2));
        }

        [Fact]
        public void NotEquals_Test_02()
        {
            var gc1 = new GeodeticCoordinates(0, 0, 0);
            var gc2 = new object();
            Assert.False(gc1.Equals(gc2));
        }
    }
}
