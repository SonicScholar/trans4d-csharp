using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRANS4D.Tests
{
    public class MiscTests
    {
        [Fact]
        public void Constants_AreCorrect()
        {
            Assert.Equal(6.378137e6, Constants.A);
            Assert.Equal(1.0 / 298.25722101, Constants.F);
            Assert.Equal(0.6694380022903146e-2, Constants.E2);
            Assert.Equal(6.378137e6 / (1.0 - 1.0 / 298.25722101), Constants.AF);
            Assert.Equal((1.0 / 298.25722101) * (2.0 - 1.0 / 298.25722101) / (Math.Pow((1.0 - 1.0 / 298.25722101), 2)), Constants.EPS);
            Assert.Equal(Math.PI, Constants.PI);
            Assert.Equal(180.0 / Math.PI, Constants.DEGREES_PER_RADIAN);
            Assert.Equal((180.0 * 3600.0) / Math.PI, Constants.RHOSEC);
            Assert.Equal(2.0 * Math.PI, Constants.TWOPI);
        }

        [Fact]
        public void ToRadians_Extension()
        {
            Assert.Equal(0.0, 0.0.ToRadians());
            Assert.Equal(Math.PI, 180.0.ToRadians());
            Assert.Equal(2.0 * Math.PI, 360.0.ToRadians());
        }

        [Fact]
        public void ToDegrees_Extension()
        {
            Assert.Equal(0.0, 0.0.ToDegrees());
            Assert.Equal(180.0, Math.PI.ToDegrees());
            Assert.Equal(360.0, 2.0 * Math.PI.ToDegrees());
        }
    }
}
