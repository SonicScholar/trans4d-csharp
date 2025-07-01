using System;
using Xunit;

namespace TRANS4D.Tests
{
    public class DatumEpochTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            var datum = Datum.ITRF2014;
            var date = new DateTime(2020, 1, 1);
            var epoch = new DatumEpoch(datum, date);
            Assert.Equal(datum, epoch.Datum);
            Assert.Equal(date, epoch.Epoch);
        }

        [Fact]
        public void Equals_ReturnsTrueForSameDatumAndDate()
        {
            var d1 = new DatumEpoch(Datum.ITRF2014, new DateTime(2020, 1, 1));
            var d2 = new DatumEpoch(Datum.ITRF2014, new DateTime(2020, 1, 1));
            Assert.True(d1.Equals(d2));
        }

        [Fact]
        public void Equals_ReturnsFalseForDifferentDatumOrDate()
        {
            var d1 = new DatumEpoch(Datum.ITRF2014, new DateTime(2020, 1, 1));
            var d2 = new DatumEpoch(Datum.ITRF2014, new DateTime(2021, 1, 1));
            var d3 = new DatumEpoch(Datum.ITRF2008_or_IGS08_or_IGb08, new DateTime(2020, 1, 1));
            Assert.False(d1.Equals(d2));
            Assert.False(d1.Equals(d3));
        }

        [Fact]
        public void GetHashCode_IsConsistentWithEquals()
        {
            var d1 = new DatumEpoch(Datum.ITRF2014, new DateTime(2020, 1, 1));
            var d2 = new DatumEpoch(Datum.ITRF2014, new DateTime(2020, 1, 1));
            Assert.Equal(d1.GetHashCode(), d2.GetHashCode());
        }
    }
}
