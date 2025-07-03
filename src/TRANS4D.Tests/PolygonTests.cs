using TRANS4D.Geometry;

namespace TRANS4D.Tests
{
    public class PolygonTests
    {
        public static List<(double X, double Y)> SimpleBox = new List<(double, double y)>
        {
            (0, 0),
            (0, 3),
            (3, 3),
            (3, 0)
        };

        public static List<(double X, double Y)> SimpleStar = new List<(double x, double y)>
        {
            (0,4), //top
            (1,0), (3,0), (1,-1), (3,-4), (0,-2), //right half
            (-3,-4), (-1, -1), (-3,0), (-1,0), (0,4) //left half
        };

        [Theory]
        [InlineData(2, 2, true)]  // Point inside the box
        [InlineData(0, 1, true)]  // Point on edge of the box
        [InlineData(0, 0, true)]  // Point on corner of the box
        [InlineData(-1, -1, false)] // Point outside of the box
        public void TestContainsPoint_OnSimpleBox(double x, double y, bool expected)
        {
            // Define a box with corners at (0,0), (0,3), (3,3), (3,0)
            var vertices = SimpleBox;

            var xPts = vertices.Select(v => v.X);
            var yPts = vertices.Select(v => v.Y);

            Polygon box = new Polygon(xPts, yPts);

            bool result = box.ContainsPoint(x, y);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(0,0,false, true)]
        [InlineData(0,0,true, true)]
        [InlineData(4,0, true, false)]
        [InlineData(4,0, false, false)]
        [InlineData(1,-1, false, false)]
        [InlineData(1,-1, true, true)]
        [InlineData(2,0, false, false)]
        [InlineData(2,0, true, true)]
        [InlineData(0,4,false, false)]
        [InlineData(0,4,true, true)]
        public void TestContainsPoint_OnStarShape(double x, double y, bool includeEdges, bool expected)
        {
            var vertices = SimpleStar;

            var xPts = vertices.Select(v => v.X);
            var yPts = vertices.Select(v => v.Y);

            Polygon star = new Polygon(xPts, yPts);

            bool result = star.ContainsPoint(x, y, includeEdges);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void Polygon_Constructor_InvalidArguments_Throws()
        {
            Assert.Throws<ArgumentException>(() =>
                _ =new Polygon(new[] { 0.0 }, new[] { 0.0, 1 })
            );
        }
    }
}
