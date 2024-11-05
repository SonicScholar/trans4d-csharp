using System;

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

        public static double ToEpoch(this DateTime dateTime)
        {
            double year = dateTime.Year;
            double epoch = year;

            // get fractional year component of the day in the year, and hours, minutes, seconds
            double daysInYear = DateTime.IsLeapYear(dateTime.Year) ? 366 : 365;
            epoch += (dateTime.DayOfYear - 1) / daysInYear;

            double hoursInYear = daysInYear * 24;
            epoch += dateTime.Hour / hoursInYear;

            double minutesInYear = hoursInYear * 60;
            epoch += dateTime.Minute / minutesInYear;

            double secondsInYear = minutesInYear * 60;
            epoch += dateTime.Second / secondsInYear;

            return epoch;
        }

        public static DateTime ToDateTime(this double epoch)
        {
            int year = (int)epoch;
            epoch -= year;

            double daysInYear = DateTime.IsLeapYear(year) ? 366 : 365;
            int dayOfYear = (int)(epoch * daysInYear);
            epoch -= dayOfYear / daysInYear;

            double hoursInYear = daysInYear * 24;
            const int hourPrecision = 12;
            int hour = (int)Math.Round(epoch * hoursInYear, hourPrecision);
            epoch -= hour / hoursInYear;

            double minutesInYear = hoursInYear * 60;
            int minute = (int)(epoch * minutesInYear);
            epoch -= minute / minutesInYear;

            double secondsInYear = minutesInYear * 60;
            int second = (int)(epoch * secondsInYear);

            return new DateTime(year, 1, 1, hour, minute, second).AddDays(dayOfYear);
        }
    }
}
