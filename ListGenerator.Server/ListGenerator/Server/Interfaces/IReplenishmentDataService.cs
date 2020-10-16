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
        Response<IEnumerable<ItemDto>> GetShoppingList(string secondReplenishmentDate, string userId);

        void ReplenishItems(ReplenishmentDto dto);
    }
}
