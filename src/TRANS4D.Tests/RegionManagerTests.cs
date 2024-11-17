using TRANS4D.BlockData;

namespace TRANS4D.Tests
{
    public class RegionManagerTests
    {
        [Fact]
        public void InitializeBoundary_Succeeds()
        {
            var boundaryPolygons = RegionManager.Regions;

            Assert.NotNull(boundaryPolygons);
            Assert.Equal(24, boundaryPolygons.Count);

            var polygon1 = boundaryPolygons[0];
            Assert.Equal(4, polygon1.Vertices.Count);

        }
    }
}
