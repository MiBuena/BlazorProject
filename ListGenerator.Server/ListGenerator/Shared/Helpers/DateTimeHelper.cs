using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ListGenerator.Shared.Helpers
{
    public class DateTimeHelper
    {
        public static string ToInvariantDateAsString(DateTime? date)
        {
            string dateAsString = null;

            if (date.HasValue)
            {
                dateAsString = date.Value.ToString(Constants.Constants.DateTransferFormat, CultureInfo.InvariantCulture);
            }

            return dateAsString;
        }

        public static DateTime ToDateFromInvariantDateAsString(string dateAsString)
        {
            var parsedDate = DateTime.ParseExact(dateAsString, Constants.Constants.DateTransferFormat, CultureInfo.InvariantCulture);
            return parsedDate;
        }
    }
}
