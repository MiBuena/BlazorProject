using ListGenerator.Client.ViewModels;
using ListGenerator.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.Helpers
{
    public static class ItemsTestHelper
    {
        public static List<PurchaseItemViewModel> BuildNonUrgentPurchaseItemVMCollection()
        {
            var purchaseItemViewModel = BuildNonUrgentPurchaseItemViewModel();

            var purchaseItemVMCollection = new List<PurchaseItemViewModel>();
            purchaseItemVMCollection.Add(purchaseItemViewModel);

            return purchaseItemVMCollection;
        }

        public static IEnumerable<ItemDto> BuildNonUrgentItemDtoCollection()
        {
            var itemDto = BuildNotUrgentItemDto();

            var itemDtoCollection = new List<ItemDto>();
            itemDtoCollection.Add(itemDto);

            return itemDtoCollection;
        }

        public static PurchaseItemViewModel BuildNonUrgentPurchaseItemViewModel()
        {
            var purchaseItem = new PurchaseItemViewModel()
            {
                ItemId = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
            };

            return purchaseItem;
        }

        public static ItemDto BuildNotUrgentItemDto()
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

        public static List<PurchaseItemViewModel> BuildUrgentPurchaseItemVMCollection()
        {
            var purchaseItemViewModel = BuildUrgentPurchaseItemViewModel();

            var purchaseItemVMCollection = new List<PurchaseItemViewModel>();
            purchaseItemVMCollection.Add(purchaseItemViewModel);

            return purchaseItemVMCollection;
        }

        public static IEnumerable<ItemDto> BuildUrgentItemDtoCollection()
        {
            var itemDto = BuildUrgentItemDto();

            var itemDtoCollection = new List<ItemDto>();
            itemDtoCollection.Add(itemDto);

            return itemDtoCollection;
        }

        public static ItemDto BuildUrgentItemDto()
        {
            var item = new ItemDto()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 02),
                ReplenishmentPeriod = 1
            };

            return item;
        }

        public static PurchaseItemViewModel BuildUrgentPurchaseItemViewModel()
        {
            var purchaseItem = new PurchaseItemViewModel()
            {
                ItemId = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 02),
            };

            return purchaseItem;
        }
    }
}
