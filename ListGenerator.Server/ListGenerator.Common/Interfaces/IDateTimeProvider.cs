using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Common.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime GetDateTimeNow();
    }
}
