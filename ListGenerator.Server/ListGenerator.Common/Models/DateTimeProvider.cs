﻿using ListGenerator.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Common.Models
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public DateTime GetDateTimeNow()
        {
            return DateTime.Now;
        }
    }
}
