using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRANS4D.Tests
{
    public class TransformationParametersTests
    {
        [Fact]
        public void SupportedTransformationsCount_Is_21()
        {
            Assert.Equal(21, TransformationParameters.SupportedTransformations.Length);
        }

        [Fact]
        public void ITRF_2014_To_ITRF_2014_Is_AllZeros()
        {
            var parameters = TransformationParameters.ITRF_2014_TO_ITRF_2014;
            Assert.Equal(0.0, parameters.Tx);
            Assert.Equal(0.0, parameters.Ty);
            Assert.Equal(0.0, parameters.Tz);
            Assert.Equal(0.0, parameters.Rx);
            Assert.Equal(0.0, parameters.Ry);
            Assert.Equal(0.0, parameters.Rz);
            Assert.Equal(0.0, parameters.Scale);

            Assert.Equal(0.0, parameters.Dtx);
            Assert.Equal(0.0, parameters.Dty);
            Assert.Equal(0.0, parameters.Dtz);
            Assert.Equal(0.0, parameters.Drx);
            Assert.Equal(0.0, parameters.Dry);
            Assert.Equal(0.0, parameters.Drz);
            Assert.Equal(0.0, parameters.DScale);

            Assert.Equal(2010.0, parameters.RefEpoch);
        }
    }
}
