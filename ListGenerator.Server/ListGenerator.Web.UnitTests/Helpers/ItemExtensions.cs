using ListGenerator.Data.Entities;
using ListGenerator.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.Helpers
{
    public static class ItemExtensions
    {
        public static bool HasTheSameProperties(this Item item, string userId, ItemDto itemDto)
        {
            var haveTheSameProperties = item.Id == itemDto.Id
                && item.Name == itemDto.Name
                && item.NextReplenishmentDate == itemDto.NextReplenishmentDate
                && item.ReplenishmentPeriod == itemDto.ReplenishmentPeriod
                && item.UserId == userId;

            return haveTheSameProperties;
        }
    }
}
