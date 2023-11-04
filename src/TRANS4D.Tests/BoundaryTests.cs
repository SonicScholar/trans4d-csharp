using TRANS4D.BlockData;

namespace TRANS4D.Tests
{
    public class BoundaryTests
    {
        [Fact]
        public void InitializeBoundary_Succeeds()
        {
            var x = Boundary.X;
            var y = Boundary.Y;
            var npoint = Boundary.NPOINT;

            Assert.NotNull(x);
            Assert.NotNull(y);
            Assert.NotNull(npoint);
        }
    }
}
