using ListGenerator.Web.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Server.Interfaces
{
    public interface IReplenishmentDataService
    {
        void ReplenishItems(ReplenishmentDto dto);

        DateTime RegenerateNextReplenishmentDateTime(int itemId, double newItemReplenishmentPeriod, DateTime previousReplenishmentDate);
    }
}
