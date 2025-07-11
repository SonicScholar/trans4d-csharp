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

        [Fact]
        public void ToNeu_KnownCase()
        {
            // Test ToNeu with a known case (lat=0, lon=0)
            var geo = GeodeticCoordinates.FromRadians(0, 0, 0);
            var ecefVel = new XYZ(1.0, 2.0, 3.0);
            var neu = VelocityTransformer.ToNeu(geo, ecefVel);
            // At equator and prime meridian:
            // vn = 0*1 - 0*2 + 1*3 = 3
            // ve = -0*1 + 1*2 = 2
            // vu = 1*1 + 0*2 + 0*3 = 1
            Assert.Equal(3.0, neu.VelocityNorth, 10);
            Assert.Equal(2.0, neu.VelocityEast, 10);
            Assert.Equal(1.0, neu.VelocityUp, 10);
        }

        [Fact]
        public void ToXyz_WithVelocityInfo_KnownCase()
        {
            // Test ToXyz(VelocityInfo) with a known case (lat=0, lon=0)
            var geo = GeodeticCoordinates.FromRadians(0, 0, 0);
            var neu = new VelocityInfo(3.0, 2.0, 1.0);
            var ecef = VelocityTransformer.ToXyz(geo, neu);
            // At equator and prime meridian:
            // vx = -0*1*3 - 0*2 + 1*1 = 1
            // vy = -0*0*3 + 1*2 + 1*0*1 = 2
            // vz = 1*3 + 0*1 = 3
            Assert.Equal(1.0, ecef.X, 10);
            Assert.Equal(2.0, ecef.Y, 10);
            Assert.Equal(3.0, ecef.Z, 10);
        }

        [Fact]
        public void Transform_ReturnsInputVelocity_WhenInDatumEqualsOutDatum()
        {
            var coord = new XYZ(123.45, 678.90, 234.56);
            var velocity = new XYZ(1.1, 2.2, 3.3);
            var result = VelocityTransformer.Transform(coord, velocity, Datum.ITRF2014, Datum.ITRF2014);
            Assert.Equal(velocity.X, result.X, 10);
            Assert.Equal(velocity.Y, result.Y, 10);
            Assert.Equal(velocity.Z, result.Z, 10);
        }

        [Fact]
        public void ConvertHorizontalVelocityToRadians_ProducesExpectedResults()
        {
            var grs80 = Ellipsoid.GRS80;
            double latitude = 45.0 * Math.PI / 180.0; // 45 degrees in radians
            double vn = 10_000.0; // 10,000 mm/yr north
            double ve = 20_000.0; // 20,000 mm/yr east
            grs80.GetRadii(latitude, out double radMeridian, out double radParallel);
            // Expected
            double expectedVnr = vn / (1000.0 * radMeridian);
            double expectedVer = ve / (1000.0 * radParallel);
            // Use extension method
            grs80.ConvertHorizontalVelocityToRadians(latitude, vn, ve, out double vnr, out double ver);
            Assert.Equal(expectedVnr, vnr, 10);
            Assert.Equal(expectedVer, ver, 10);
        }
    }
}
