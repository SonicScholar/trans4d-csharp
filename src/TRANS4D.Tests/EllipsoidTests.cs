using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRANS4D.Tests
{
    public class EllipsoidTests
    {
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
    }
}
