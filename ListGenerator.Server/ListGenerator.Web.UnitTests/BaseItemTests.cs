using ListGenerator.Client.ViewModels;
using ListGenerator.Shared.Dtos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests
{
    public abstract class BaseItemTests : AssertAllTest
    {
        protected ItemDto nonUrgentItemDto;
        protected PurchaseItemViewModel nonUrgentPurchaseItemViewModel;

        [SetUp]
        public void Init()
        {
            nonUrgentItemDto = BuildNotUrgentItemDto();
            nonUrgentPurchaseItemViewModel = BuildNonUrgentPurchaseItemViewModel();
        }

        private ItemDto BuildNotUrgentItemDto()
        {
            var item = new ItemDto()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1
            };

            return item;
        }

        private PurchaseItemViewModel BuildNonUrgentPurchaseItemViewModel()
        {
            var purchaseItem = new PurchaseItemViewModel()
            {
                ItemId = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
            };

            return purchaseItem;
        }
    }
}
