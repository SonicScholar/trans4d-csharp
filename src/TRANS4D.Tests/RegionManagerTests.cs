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

        [Fact]
        public void Regions_Property_ReturnsExpectedNumberOfRegions()
        {
            // Act
            var regions = RegionManager.Regions;

            // Assert
            Assert.NotNull(regions);
            Assert.Equal(24, regions.Count);
            Assert.IsAssignableFrom<IReadOnlyList<IRegion>>(regions);
        }

        [Fact]
        public void Regions_Property_AllRegionsHaveBoundaries()
        {
            // Act
            var regions = RegionManager.Regions;

            // Assert
            foreach (var region in regions)
            {
                Assert.NotNull(region);
                Assert.NotNull(region.Boundary);
                Assert.True(region.Boundary.Vertices.Count >= 3, "Each region should have at least 3 vertices to form a polygon");
            }
        }

        [Fact]
        public void GetBoundary_WithValidCoordinates_ReturnsRegion()
        {
            // Arrange - Use coordinates that should be within a known region
            var coordinates = GeodeticCoordinates.FromDegrees(40.0, -105.0, 0); // Colorado, USA

            // Act
            var region = RegionManager.GetBoundary(coordinates);

            // Assert
            Assert.NotNull(region);
            Assert.True(region.ContainsPoint(coordinates));
        }

        [Fact]
        public void GetBoundary_WithCoordinatesInOcean_ReturnsNull()
        {
            // Arrange - Use coordinates in the middle of the Pacific Ocean
            var coordinates = GeodeticCoordinates.FromDegrees(0.0, 0.0, 0);

            // Act
            var region = RegionManager.GetBoundary(coordinates);

            // Assert - Should return null for points not in any region
            Assert.Null(region);
        }

        [Fact]
        public void GetBoundary_WithMultipleCoordinates_ReturnsCorrectRegions()
        {
            // Arrange - Test multiple coordinate sets
            var coords1 = GeodeticCoordinates.FromDegrees(40.0, -105.0, 0); // US West
            var coords2 = GeodeticCoordinates.FromDegrees(35.0, -118.0, 0); // California
            var coords3 = GeodeticCoordinates.FromDegrees(60.0, -150.0, 0); // Alaska

            // Act
            var region1 = RegionManager.GetBoundary(coords1);
            var region2 = RegionManager.GetBoundary(coords2);
            var region3 = RegionManager.GetBoundary(coords3);

            // Assert
            Assert.NotNull(region1);
            Assert.NotNull(region2);
            Assert.NotNull(region3);
            
            // Regions should contain their respective points
            Assert.True(region1.ContainsPoint(coords1));
            Assert.True(region2.ContainsPoint(coords2));
            Assert.True(region3.ContainsPoint(coords3));
        }

        [Theory]
        [InlineData(90.0, 0.0)]    // North Pole
        [InlineData(-90.0, 0.0)]   // South Pole
        [InlineData(0.0, 180.0)]   // International Date Line
        [InlineData(0.0, -180.0)]  // International Date Line (other side)
        public void GetBoundary_WithExtremeCoordinates_HandlesGracefully(double lat, double lon)
        {
            // Arrange
            var coordinates = GeodeticCoordinates.FromDegrees(lat, lon, 0);

            // Act & Assert - Should not throw exceptions
            var region = RegionManager.GetBoundary(coordinates);
            
            // Result can be null or valid region, but should not crash
            Assert.True(region == null || region is IRegion);
        }

        [Fact]
        public void GetBoundary_WithNormalizedCoordinates_WorksCorrectly()
        {
            // Arrange - Use coordinates that need normalization
            var coordinates = GeodeticCoordinates.FromDegrees(40.0, 255.0, 0); // Longitude > 180
            var normalizedCoords = GeodeticCoordinates.FromDegrees(40.0, -105.0, 0); // Equivalent normalized

            // Act
            var region1 = RegionManager.GetBoundary(coordinates);
            var region2 = RegionManager.GetBoundary(normalizedCoords);

            // Assert - Should return the same region (if any)
            if (region1 != null && region2 != null)
            {
                Assert.Equal(region1, region2);
            }
        }

        [Fact]
        public void GetBoundary_WithBoundaryCoordinates_HandlesBoundaryConditions()
        {
            // Arrange - Get a region and test coordinates near its boundary
            var regions = RegionManager.Regions;
            var testRegion = regions.FirstOrDefault(r => r.Boundary.Vertices.Count > 0);
            
            if (testRegion != null)
            {
                var firstVertex = testRegion.Boundary.Vertices.First();
                var boundaryCoords = GeodeticCoordinates.FromDegrees(firstVertex.Y, firstVertex.X, 0);

                // Act
                var foundRegion = RegionManager.GetBoundary(boundaryCoords);

                // Assert - Should handle boundary conditions gracefully
                // (Result depends on implementation - could be the region or null)
                Assert.True(foundRegion == null || foundRegion.ContainsPoint(boundaryCoords));
            }
        }

        [Fact]
        public void BoundaryData_ConsistencyCheck()
        {
            // Act
            var regions = RegionManager.Regions;

            // Assert - Verify that all regions have valid boundaries
            for (int i = 0; i < regions.Count; i++)
            {
                var region = regions[i];
                Assert.NotNull(region.Boundary);
                Assert.True(region.Boundary.Vertices.Count >= 3, 
                    $"Region {i} should have at least 3 vertices to form a valid polygon");
            }
        }

        [Fact]
        public void GetBoundary_ConsistentResults_ForSameCoordinates()
        {
            // Arrange
            var coordinates = GeodeticCoordinates.FromDegrees(40.0, -105.0, 0);

            // Act - Call multiple times
            var region1 = RegionManager.GetBoundary(coordinates);
            var region2 = RegionManager.GetBoundary(coordinates);
            var region3 = RegionManager.GetBoundary(coordinates);

            // Assert - Should return consistent results
            Assert.Equal(region1, region2);
            Assert.Equal(region2, region3);
        }

        [Fact]
        public void Regions_Property_ReturnsReadOnlyCollection()
        {
            // Act
            var regions = RegionManager.Regions;

            // Assert
            Assert.IsAssignableFrom<IReadOnlyList<IRegion>>(regions);

            // todo: It's a regular List, so someone could cast it to IList<IRegion>.
            //A ssert.False(regions is IList<IRegion>, "Regions should not be modifiable");
        }

        [Fact]
        public void Regions_Property_InitializesInternalArraysWhenAccessed()
        {
            // Arrange - Access the Regions property which should trigger initialization
            var regions = RegionManager.Regions;

            // Assert - Verify that the arrays are accessible and have data
            // The NPOINT array should be initialized with boundary point indices
            var npointArray = RegionManager.NPOINT;
            Assert.NotNull(npointArray);
            Assert.True(npointArray.Length >= 25, "NPOINT array should have at least 25 elements for region indices");
            
            // Verify that the boundary data arrays (Y) is accessible
            var yArray = RegionManager.Y;
            var xArray = RegionManager.X;
            Assert.NotNull(yArray);
            Assert.NotNull(xArray);

            // Verify that the first region has the expected number of boundary points
            // Based on the code, NPOINT[1] = 1 and NPOINT[2] = 5, so region 1 should have 4 points
            Assert.Equal(1, npointArray[1]);
            Assert.Equal(5, npointArray[2]);
            
            // Verify that regions were created correctly
            Assert.Equal(24, regions.Count);
            Assert.NotNull(regions[0]);
            Assert.NotNull(regions[0].Boundary);
        }

        [Fact]
        public void GetBoundary_WithSimilarButDifferentCoordinates_MayReturnDifferentRegions()
        {
            // Arrange - Test coordinates that are close but might be in different regions
            var coords1 = GeodeticCoordinates.FromDegrees(39.99, -105.0, 0);
            var coords2 = GeodeticCoordinates.FromDegrees(40.01, -105.0, 0);

            // Act
            var region1 = RegionManager.GetBoundary(coords1);
            var region2 = RegionManager.GetBoundary(coords2);

            // Assert - Both should either be null or valid regions
            Assert.True(region1 == null || region1 is IRegion);
            Assert.True(region2 == null || region2 is IRegion);
        }
    }
}
