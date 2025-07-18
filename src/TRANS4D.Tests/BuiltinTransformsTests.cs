

namespace TRANS4D.Tests
{
    public class BuiltinTransformsTests
    {
        [Fact]
        public void SupportedTransformationsCount_Is_21()
        {
            Assert.Equal(25, BuiltInTransforms.SupportedTransformations.Length);
        }

        [Fact]
        public void SupportedTransform_01_Is_ITRF_TO_NAD_1983_2011()
        {
            var datum = Datum.Nad83_2011_or_CORS96;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_NAD_1983_2011;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_02_Is_ITRF_TO_ITRF_1988()
        {
            var datum = Datum.ITRF88;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_1988;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_03_Is_ITRF_TO_ITRF_1989()
        {
            var datum = Datum.ITRF89;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_1989;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_04_Is_ITRF_TO_ITRF_1990()
        {
            var datum = Datum.ITRF90;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_1990;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_05_Is_ITRF_TO_ITRF_1991()
        {
            var datum = Datum.ITRF91;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_1991;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_06_Is_ITRF_TO_ITRF_1992()
        {
            var datum = Datum.ITRF92;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_1992;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_07_Is_ITRF_TO_ITRF_1993()
        {
            var datum = Datum.ITRF93;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_1993;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_08_Is_ITRF_TO_ITRF_1994_Or_1996()
        {
            var datum = Datum.ITRF94_or_96;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_1994_OR_1996;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_09_Is_ITRF_TO_ITRF_1997()
        {
            var datum = Datum.ITRF97;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_1997;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_10_Is_ITRF_TO_ITRF_2014_PMM_NorthAmerica()
        {
            var datum = Datum.ITRF2014_PMM_NorthAmerica;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_2014_PMM_NORTH_AMERICA;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_11_Is_ITRF_TO_ITRF_2000()
        {
            var datum = Datum.ITRF2000;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_2000;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_12_Is_ITRF_TO_PACP00_Or_PA11()
        {
            var datum = Datum.PACP00_or_PA11;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_PACP_2000_OR_PA_2011;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_13_Is_ITRF_TO_MARP00_Or_MA11()
        {
            var datum = Datum.MARP00_or_MA11;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_MARP_2000_OR_MA_2011;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_14_Is_ITRF_TO_ITRF_2005()
        {
            var datum = Datum.ITRF2005;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_2005;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_15_Is_ITRF_TO_ITRF_2008_Or_IGS08_Or_IGb08()
        {
            var datum = Datum.ITRF2008_or_IGS08_or_IGb08;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_2008_OR_IGS08_OR_IGB08;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_16_Is_ITRF_TO_ITRF_2014()
        {
            var datum = Datum.ITRF2014;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_2014;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_17_Is_ITRF_TO_PMM_Caribbean()
        {
            var datum = Datum.PMM_Caribbean;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_CATRF_2022;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_18_Is_ITRF_TO_PMM_Pacific()
        {
            var datum = Datum.PMM_Pacific;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_PMM_PACIFIC;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_19_Is_ITRF_TO_PMM_Mariana()
        {
            var datum = Datum.PMM_Mariana;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_PMM_MARIANA;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_20_Is_ITRF_TO_PMM_Bering()
        {
            var datum = Datum.PMM_Bering;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_PMM_BERING;
            Assert.Equal(builtInTransform, transform);
        }

        [Fact]
        public void SupportedTransform_21_Is_ITRF_TO_ITRF_20()
        {
            var datum = Datum.ITRF20;
            var transform = datum.GetTransformationParametersForItrf2014();
            var builtInTransform = BuiltInTransforms.ITRF_2014_TO_ITRF_2020;
            Assert.Equal(builtInTransform, transform);
        }

    }
}
