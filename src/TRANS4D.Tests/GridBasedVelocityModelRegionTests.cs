namespace TRANS4D.Tests
{
    public class GridBasedRegionTests
    {
        [Fact]
        public void GetGridWeights_ReturnsExpectedWeights_ForKnownInput()
        {
            // Arrange
            var region = new GridBasedVelocityModelRegion(null, 3);
            var coords = GeodeticCoordinates.FromRadians(0.67059447295648356, -1.4617038310036168, 0);

            // Act
            var weights = region.GetGridWeights(coords, out int xCellIndex, out int yCellIndex);

            // Assert
            Assert.Equal(373, xCellIndex);
            Assert.Equal(231, yCellIndex);

            Assert.Equal(0.24216207607149695, weights[0][0], 11);
            Assert.Equal(0.74920455770371774, weights[0][1], 11);
            Assert.Equal(0.0021088806272590269, weights[1][0], 11);
            Assert.Equal(0.0065244855975262507, weights[1][1], 11);
        }
    }
}
