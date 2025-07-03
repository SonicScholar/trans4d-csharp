namespace TRANS4D.Tests
{
    public class Trans4dTests
    {
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
