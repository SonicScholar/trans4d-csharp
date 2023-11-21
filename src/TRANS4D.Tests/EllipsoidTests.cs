using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRANS4D.Tests
{
    public class EllipsoidTests
    {
        private const int FromXYZTolerance = 7;
        private const int EHTTolerance = 2;

        [Fact]
        public void GRS80_PrimeMeridianEquatorRadius_ToXYZ()
        {
            var grs80 = Ellipsoid.GRS80;

            //radians
            double latitude = 0.0;
            double longitude = 0.0;
            double height = 0.0;

            var (x, y, z) = grs80.LatLongHeightToXYZ(latitude, longitude, height);

            double semiMajor = grs80.A;
            Assert.Equal(6378137.0, semiMajor);
            Assert.Equal(semiMajor, x);
            Assert.Equal(0.0, y);
            Assert.Equal(0.0, z);
        }

        [Fact]
        public void GRS80_PrimeMeridianEquatorRadius_ToXYZ_WithHeight()
        {
            var grs80 = Ellipsoid.GRS80;

            //radians
            double latitude = 0.0;
            double longitude = 0.0;
            double height = 100.0;

            var (x, y, z) = grs80.LatLongHeightToXYZ(latitude, longitude, height);

            double semiMajor = grs80.A;
            Assert.Equal(6378137.0, semiMajor);
            Assert.Equal(semiMajor + height, x);
            Assert.Equal(0.0, y);
            Assert.Equal(0.0, z);
        }

        [Fact]
        public void GRS80_XYZToLatLongHeight_LatitudeOver45Degrees()
        {
            var grs80 = Ellipsoid.GRS80;

            var (x,y,z) = grs80.LatLongHeightToXYZ(50.0.ToRadians(), 0.0, 0.0);


            var (latitude, longitude, height, succeeded) = grs80.XYZToLatLongHeight(x, y, z);

            Assert.True(succeeded);
            Assert.Equal(50.0.ToRadians(), latitude);
            Assert.Equal(0.0, longitude);
            Assert.Equal(0.0, height,8);
        }

        [Fact]
        public void GRS80_XYZToLatLongHeight_NonConverging()
        {
            var grs80 = Ellipsoid.GRS80;
            double lat = 45.0.ToRadians();
            double lon = 45.0.ToRadians();
            //way the hell out there in space... like orders of magnitude bigger than our observable universe
            double eht = Math.Pow(Math.Sqrt(double.MaxValue), 1.001); 

            var (x, y, z) = grs80.LatLongHeightToXYZ(lat, lon, eht);
            (lat, lon, eht, var succeeded) = grs80.XYZToLatLongHeight(x, y, z);

            Assert.False(succeeded);
            Assert.Equal(double.NaN, lat);
            Assert.Equal(double.NaN, lon);
            Assert.Equal(double.NaN, eht);
            
        }

        //todo: test transform between two datums with different epochs, but doesn't use velocity model

        // Test data run from Trans4d 4.1 Fortran program
        /* TRANSFORMING POSITIONS FROM ITRF2020 or IGS20            (EPOCH = 01-01-2010 (2010.0000))
           TO NAD_83(2011/CORS96/2007)     (EPOCH = 01-01-2010 (2010.0000))

           INPUT COORDINATES   OUTPUT COORDINATES

           ITRF20_to_NAD83
           LATITUDE     40 00  0.00000 N     39 59 59.97972 N
           LONGITUDE   105 00  0.00000 W    104 59 59.95474 W
           ELLIP. HT.            1500.000            1500.865 m
           X                 -1266623.309        -1266622.548 m
           Y                 -4727102.545        -4727103.851 m
           Z                  4078949.754         4078949.830 m
        */

        [Fact]
        public void GRS80_WEST_US_ToXYZ()
        {
            var grs80 = Ellipsoid.GRS80;
            double latitude = 40.0.ToRadians();
            double longitude = -105.0.ToRadians();
            double height = 1500.0;
            var (x, y, z) = grs80.LatLongHeightToXYZ(latitude, longitude, height);
            Assert.Equal(-1266623.309, x, 3);
            Assert.Equal(-4727102.545, y, 3);
            Assert.Equal(4078949.754, z, 3);
        }

        [Fact]
        public void GRS80_WEST_US_ToLatLongHeight()
        {
            var grs80 = Ellipsoid.GRS80;
            double x = -1266623.309;
            double y = -4727102.545;
            double z = 4078949.754;
            var (latitude, longitude, height, succeeded) = grs80.XYZToLatLongHeight(x, y, z);
            Assert.True(succeeded);
            Assert.Equal(40.0, latitude.ToDegrees(), FromXYZTolerance);
            Assert.Equal(-105.0, longitude.ToDegrees(), FromXYZTolerance);
            Assert.Equal(1500.0, height, EHTTolerance);
        }
    }
}

