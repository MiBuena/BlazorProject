using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ListGenerator.Shared.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToDateString(this DateTime date)
        {
            return date.ToString("dd.MM.yyyy");
        }
    }
}
