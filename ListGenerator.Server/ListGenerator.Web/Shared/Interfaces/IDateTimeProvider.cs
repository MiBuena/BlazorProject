using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.Shared.Interfaces
{
    public interface IDateTimeProvider
    {
        DateTime GetDateTimeNow();

        DateTime GetDateTimeNowDate();
    }
}
