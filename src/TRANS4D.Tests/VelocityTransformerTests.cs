namespace TRANS4D.Tests
{
    public class VelocityTransformerTests
    {
        [Fact]
        public void CORS_Cofc_ITRF2014_To_NAD83_2011_Velocity()
        {
            // Position and velocities from cofc.txt (ARP)
            var coord = new XYZ(-1268727.998, -4682467.553, 4129276.359);
            // ITRF2014 velocity in mm/yr
            var velItrf = new XYZ(-0.0148 * 1000, 0.0007 * 1000, -0.0055 * 1000);
            // Expected NAD83(2011) velocity in mm/yr
            var expectedNad83 = new XYZ(0.0024 * 1000, 0.0015 * 1000, -0.0011 * 1000);
            var result = VelocityTransformer.Transform(coord, velItrf, Datum.ITRF2014, Datum.Nad83_2011_or_CORS96);
            Assert.InRange(result.X, expectedNad83.X - 0.1, expectedNad83.X + 0.1);
            Assert.InRange(result.Y, expectedNad83.Y - 0.1, expectedNad83.Y + 0.1);
            Assert.InRange(result.Z, expectedNad83.Z - 0.1, expectedNad83.Z + 0.1);
        }

        [Fact]
        public void CORS_Cofc_NAD83_2011_To_ITRF2014_Velocity()
        {
            // Position and velocities from cofc.txt (ARP)
            var coord = new XYZ(-1268727.998, -4682467.553, 4129276.359);
            // NAD83(2011) velocity in mm/yr
            var velNad83 = new XYZ(0.0024 * 1000, 0.0015 * 1000, -0.0011 * 1000);
            // Expected ITRF2014 velocity in mm/yr
            var expectedItrf = new XYZ(-0.0148 * 1000, 0.0007 * 1000, -0.0055 * 1000);
            var result = VelocityTransformer.Transform(coord, velNad83, Datum.Nad83_2011_or_CORS96, Datum.ITRF2014);
            Assert.InRange(result.X, expectedItrf.X - 0.1, expectedItrf.X + 0.1);
            Assert.InRange(result.Y, expectedItrf.Y - 0.1, expectedItrf.Y + 0.1);
            Assert.InRange(result.Z, expectedItrf.Z - 0.1, expectedItrf.Z + 0.1);
        }
    }
}
