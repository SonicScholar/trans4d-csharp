using TRANS4D.BlockData;

namespace TRANS4D.Tests
{
    public class RegionManagerTests
    {
        [Fact]
        public void InitializeBoundary_Succeeds()
        {
            var regions = RegionManager.Regions;

            Assert.NotNull(regions);
            Assert.Equal(24, regions.Count);

            var region = regions[0];
            Assert.Equal(4, region.Boundary.Vertices.Count);
        }
    }
}
