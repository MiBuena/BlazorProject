using System;

namespace ListGenerator.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateString(this DateTime date, string format = Constants.Constants.DateFormat)
        {
            return date.ToString(format);
        }
    }
}
