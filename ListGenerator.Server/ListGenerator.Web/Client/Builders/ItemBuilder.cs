using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.Interfaces;
using ListGenerator.Web.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Builders
{
    public class ItemBuilder : IItemBuilder
    {
        private IDateTimeProvider _dateTimeProvider;

        public ItemBuilder(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
        }

        public ItemViewModel BuildItemViewModel()
        {
            var model = new ItemViewModel()
            {
                NextReplenishmentDate = _dateTimeProvider.GetDateTimeNowDate()
            };

            return model;
        }

        //public IEnumerable<PurchaseItemViewModel> BuildPurchaseItemViewModels(hmentDate, IEnumerable<ItemDto> itemDtos)
        //{

        //}


    }
}
