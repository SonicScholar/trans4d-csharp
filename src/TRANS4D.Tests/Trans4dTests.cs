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
        public void ComputeNewCoordinatesFromVelocity_ThirteenPointNineYears_AppliesVelocityCorrectly()
        {
            // Arrange
            var coordinates = GeodeticCoordinates.FromRadians(
                0.67059433985213446,   // latitude in radians
                -1.4617037129388828,   // longitude in radians
                259.772                // height in meters
            );

            // just using the Date component (sans hours, minutes, seconds) to compare with legacy
            // Fortran implementation which truncates HMS before calculating minutes.
            var fromDate = 2010.0.ToDateTime().Date;
            var toDate = 2023.917.ToDateTime().Date;

            var velocity = new VelocityInfo(
                -0.34065493498121513,   // north velocity (mm/yr)
                2.0075526460630888,    // east velocity (mm/yr)
                -2.9956672290852335    // up velocity (mm/yr)
            );

            // Act
            var result = Trans4d.ComputeNewCoordinatesFromVelocity(coordinates, fromDate, toDate, velocity);

            // Assert - compare to expected values
            Assert.Equal(0.67059433910689481, result.Latitude, 12);     // radians
            Assert.Equal(-1.4617037073562025, result.Longitude, 12);    // radians
            Assert.Equal(259.73031901202404, result.Height, 9);         // meters
        }


        [Fact]
        public void TransformPosition_TransformsNad83ToItrf2014_Correctly()
        {
            // Arrange: input values from C++ example
            double latDecimalDegrees = Utilities.DmsToDecimalDegrees(38, 25, 20.01158);
            double lonDecimalDegrees = -Utilities.DmsToDecimalDegrees(83, 44, 58.03314);
            double ellipsoidHeight = 259.772;

            var inDate = 2010.0.ToDateTime().Date;
            var outDate = 2023.9170.ToDateTime().Date;

            var inDatum = Datum.Nad83_2011_or_CORS96;
            var outDatum = Datum.ITRF2014;

            var coordinates = GeodeticCoordinates.FromDegrees(latDecimalDegrees, lonDecimalDegrees, ellipsoidHeight);
            var inputEpoch = new DatumEpoch(inDatum, inDate);
            var outputEpoch = new DatumEpoch(outDatum, outDate);

            // Act
            var newCoordinates = Trans4d.TransformPosition(
                coordinates,
                inputEpoch, outputEpoch);

            // Assert: expected values from C++ output
            Assert.Equal(38.42223328, newCoordinates.LatitudeDegrees, 8); // 6 decimal places
            Assert.Equal(-83.74946272, newCoordinates.LongitudeDegrees, 8);
            Assert.Equal(258.50616906, newCoordinates.Height, 5);
        }
    }
}
