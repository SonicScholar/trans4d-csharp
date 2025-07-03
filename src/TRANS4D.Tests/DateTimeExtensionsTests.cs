namespace TRANS4D.Tests
{
    public class DateTimeExtensionsTests
    {
        [Fact]
        public void ModifiedJulianDateEpoch_IsCorrect()
        {
            Assert.Equal(new DateTime(1858, 11, 17), DateTimeExtensions.ModifiedJulianDateEpoch);
        }

        [Fact]
        public void ToModifiedJulianDateMinutes_IsCorrect()
        {
            var dateTime = new DateTime(2010, 1, 1);
            int modifiedJulianDateMinutes = dateTime.ToModifiedJulianDateMinutes();
            Assert.Equal(79483680, modifiedJulianDateMinutes);
        }

        [Fact]
        public void ToEpoch_January1st2010_IsCorrect()
        {
            var dateTime = new DateTime(2010, 1, 1);
            double epoch = dateTime.ToEpoch();
            Assert.Equal(2010.0, epoch);
        }

        [Fact]
        public void ToDateTime_2010_IsCorrect()
        {
            double epoch = 2010.0;
            DateTime dateTime = epoch.ToDateTime();
            Assert.Equal(new DateTime(2010, 1, 1), dateTime);
        }

        [Fact]
        public void ToEpoch_HalfwayThru_LeapYear_IsCorrect()
        {
            var dateTime = new DateTime(2012, 7, 2);
            double epoch = dateTime.ToEpoch();
            Assert.Equal(2012.5, epoch);
        }

        [Fact]
        public void ToDateTime_HalfwayThru_LeapYear_IsCorrect()
        {
            double epoch = 2012.5;
            DateTime dateTime = epoch.ToDateTime();
            Assert.Equal(new DateTime(2012, 7, 2), dateTime);
        }

        [Fact]
        public void ToEpoch_HalfwayThru_NonLeapYer_IsCorrect()
        {
            var dateTime = new DateTime(2011, 1, 1);
            dateTime = dateTime.AddDays(365.0 / 2);
            double epoch = dateTime.ToEpoch();
            Assert.Equal(2011.5, epoch);
        }

        [Fact]
        public void ToDateTime_HalfwayThru_NonLeapYear_IsCorrect()
        {
            double epoch = 2011.5;
            DateTime dateTime = epoch.ToDateTime();
            var expectedDateTime = new DateTime(2011, 1, 1).AddDays(365.0 / 2);
            Assert.Equal(expectedDateTime, dateTime);
        }
    }
}
