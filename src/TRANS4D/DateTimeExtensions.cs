using System;
using System.Collections.Generic;
using System.Text;

namespace TRANS4D
{
    public static class DateTimeExtensions
    {
        public static readonly DateTime ModifiedJulianDateEpoch = new DateTime(1858, 11, 17);

        //todo: determine if this is needed
        //public static int ToModifiedJulianDate(this DateTime dateTime)
        //{
        //    return (int)(dateTime - ModifiedJulianDateEpoch).TotalDays;
        //}

        public static int ToModifiedJulianDateMinutes(this DateTime dateTime)
        {
            return (int)(dateTime - ModifiedJulianDateEpoch).TotalMinutes;
        }

    }
}
