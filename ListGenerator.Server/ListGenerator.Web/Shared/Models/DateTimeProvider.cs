using ListGenerator.Web.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.Shared.Models
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
