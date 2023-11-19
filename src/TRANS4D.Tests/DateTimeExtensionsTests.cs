using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
