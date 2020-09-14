using System;

namespace ListGenerator.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateString(this DateTime date, string dateFormat = Constants.Constants.DateFormat)
        {
            return date.ToString(dateFormat);
        }
    }
}
