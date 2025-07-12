namespace TRANS4D.Tests
{
    public class Trans4dTests
    {
        [Fact]
        public void ComputeNewCoordinatesFromVelocity_NoTimeChange_ReturnsSameCoordinates()
        {
            // Arrange
            var coordinates = GeodeticCoordinates.FromDegrees(38.0, -83.0, 100.0);
            var date = new DateTime(2010, 1, 1);
            var velocity = new VelocityInfo(10.0, 5.0, 2.0); // mm/yr

            // Act
            var result = Trans4d.ComputeNewCoordinatesFromVelocity(coordinates, date, date, velocity);

            // Assert - should be same coordinates
            Assert.Equal(coordinates.LatitudeDegrees, result.LatitudeDegrees, 10);
            Assert.Equal(coordinates.LongitudeDegrees, result.LongitudeDegrees, 10);
            Assert.Equal(coordinates.Height, result.Height, 10);
        }

        [Fact]
        public void ComputeNewCoordinatesFromVelocity_OneYearDifference_AppliesVelocityCorrectly()
        {
            // Arrange
            var coordinates = GeodeticCoordinates.FromDegrees(38.0, -83.0, 100.0);
            var fromDate = new DateTime(2010, 1, 1);
            var toDate = new DateTime(2011, 1, 1); // 1 year difference
            var velocity = new VelocityInfo(10.0, 5.0, 2.0); // mm/yr (north, east, up)

            // Act
            var result = Trans4d.ComputeNewCoordinatesFromVelocity(coordinates, fromDate, toDate, velocity);

            // Assert - coordinates should have changed
            Assert.NotEqual(coordinates.LatitudeDegrees, result.LatitudeDegrees);
            Assert.NotEqual(coordinates.LongitudeDegrees, result.LongitudeDegrees);
            Assert.NotEqual(coordinates.Height, result.Height);

            // Height should increase by 2mm (converted to meters)
            Assert.Equal(coordinates.Height + 0.002, result.Height, 6);
        }

        [Fact]
        public void TransformPosition_TransformsNad83ToItrf2014_Correctly()
        {
            // Arrange: input values from C++ example
            double latDecimalDegrees = Utilities.DmsToDecimalDegrees(38, 25, 20.01158);
            double lonDecimalDegrees = -Utilities.DmsToDecimalDegrees(83, 44, 58.03314);
            double ellipsoidHeight = 259.772;

            var inDate = new DateTime(2010, 1, 1); // 2010.0
            var outDate = new DateTime(2023, 12, 1); // 2023.9170 (approximate)

            var inDatum = Datum.Nad83_2011_or_CORS96;
            var outDatum = Datum.ITRF2014;

            var coordinates = GeodeticCoordinates.FromDegrees(latDecimalDegrees, lonDecimalDegrees, ellipsoidHeight);
            var inputEpoch = new DatumEpoch(inDatum, inDate);
            var outputEpoch = new DatumEpoch(outDatum, outDate);

            // Act
            var (newLat, newLon, newEht) = Trans4d.TransformPosition(
                coordinates,
                inputEpoch, outputEpoch);

            // Assert: expected values from C++ output
            Assert.Equal(38.42223328, newLat, 6); // 6 decimal places
            Assert.Equal(-83.74946272, newLon, 6);
            Assert.Equal(258.50616906, newEht, 6);
        }
    }
}
