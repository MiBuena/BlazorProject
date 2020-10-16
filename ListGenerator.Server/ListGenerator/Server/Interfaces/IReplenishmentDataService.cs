using ListGenerator.Shared.Dtos;
using ListGenerator.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Server.Interfaces
{
    public interface IReplenishmentDataService
    {
        Response<IEnumerable<ReplenishmentItemDto>> GetShoppingList(string firstShoppingDateAsString, string secondShoppingDateAsString, string userId);

        void ReplenishItems(ReplenishmentDto dto);
    }
}
