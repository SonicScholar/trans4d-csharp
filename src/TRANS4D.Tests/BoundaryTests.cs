using TRANS4D.BlockData;

namespace TRANS4D.Tests
{
    public class BoundaryTests
    {
        [Fact]
        public void InitializeBoundary_Succeeds()
        {
            var boundaryPolygons = Boundary.BoundaryPolygons;

            Assert.NotNull(boundaryPolygons);
            Assert.Equal(24, boundaryPolygons.Count);

            var polygon1 = boundaryPolygons[0];
            Assert.Equal(4, polygon1.Vertices.Count);

        }
    }
}
