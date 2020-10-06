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

        public static IEnumerable<ItemDto> BuildItemsDtosCollection()
        {
            var collection = new List<ItemDto>();

            var firstItem = new ItemDto()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1
            };

            var secondItem = new ItemDto()
            {
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2
            };

            var thirdItem = new ItemDto()
            {
                Id = 3,
                Name = "Biscuits",
                NextReplenishmentDate = new DateTime(2020, 10, 07),
                ReplenishmentPeriod = 2
            };

            var fourthItem = new ItemDto()
            {
                Id = 4,
                Name = "Oats",
                NextReplenishmentDate = new DateTime(2020, 10, 1),
                ReplenishmentPeriod = 7
            };

            var fifthItem = new ItemDto()
            {
                Id = 5,
                Name = "Cake",
                NextReplenishmentDate = new DateTime(2020, 10, 2),
                ReplenishmentPeriod = 5
            };

            collection.Add(firstItem);
            collection.Add(secondItem);
            collection.Add(thirdItem);
            collection.Add(fourthItem);
            collection.Add(fifthItem);

            return collection;
        }

        public static List<PurchaseItemViewModel> BuildPurchaseItemsViewModelsCollection()
        {
            var collection = new List<PurchaseItemViewModel>();

            var firstItem = new PurchaseItemViewModel()
            {
                ItemId = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
            };

            var secondItem = new PurchaseItemViewModel()
            {
                ItemId = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
            };

            var thirdItem = new PurchaseItemViewModel()
            {
                ItemId = 3,
                Name = "Biscuits",
                NextReplenishmentDate = new DateTime(2020, 10, 07),
            };

            var fourthItem = new PurchaseItemViewModel()
            {
                ItemId = 4,
                Name = "Oats",
                NextReplenishmentDate = new DateTime(2020, 10, 1),
            };

            var fifthItem = new PurchaseItemViewModel()
            {
                ItemId = 5,
                Name = "Cake",
                NextReplenishmentDate = new DateTime(2020, 10, 2),
            };

            collection.Add(firstItem);
            collection.Add(secondItem);
            collection.Add(thirdItem);
            collection.Add(fourthItem);
            collection.Add(fifthItem);

            return collection;
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
