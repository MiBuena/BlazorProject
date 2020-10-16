using ListGenerator.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListGenerator.Web.UnitTests
{
    public class BaseItemsTests
    {       
        protected IQueryable<Item> BuildItemsCollection()
        {
            var collection = new List<Item>();

            var firstItem = BuildFirstItem();
            var secondItem = BuildSecondItem();
            var thirdItem = BuildThirdItem();

            var fourthItem = new Item()
            {
                Id = 4,
                Name = "Oats",
                NextReplenishmentDate = new DateTime(2020, 10, 1),
                ReplenishmentPeriod = 7,
                UserId = "925912b0-c59c-4e1b-971a-06e8abab7848"
            };

            var fifthItem = new Item()
            {
                Id = 5,
                Name = "Cake",
                NextReplenishmentDate = new DateTime(2020, 10, 2),
                ReplenishmentPeriod = 5,
                UserId = "925912b0-c59c-4e1b-971a-06e8abab7848"
            };

            collection.Add(firstItem);
            collection.Add(secondItem);
            collection.Add(thirdItem);
            collection.Add(fourthItem);
            collection.Add(fifthItem);

            return collection.AsQueryable();
        }

        protected Item BuildThirdItem()
        {
            var purchases = BuildThirdItemPurchases();

            var thirdItem = new Item()
            {
                Id = 3,
                Name = "Biscuits",
                NextReplenishmentDate = new DateTime(2020, 10, 07),
                ReplenishmentPeriod = 5,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0",
                Purchases = purchases
            };

            return thirdItem;
        }
        private ICollection<Purchase> BuildThirdItemPurchases()
        {
            var firstPurchase = new Purchase()
            {
                ReplenishmentDate = new DateTime(2020, 10, 02),
                Quantity = 1,
                ItemId = 3
            };

            var list = new List<Purchase>() { firstPurchase };
            return list;
        }

        protected Item BuildSecondItem()
        {
            var secondItem = new Item()
            {
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            return secondItem;
        }

        protected Item BuildFirstItem()
        {
            var firstItemPurchases = BuildFirstItemPurchases();

            var firstItem = new Item()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0",
                Purchases = firstItemPurchases
            };

            return firstItem;
        }


        private ICollection<Purchase> BuildFirstItemPurchases()
        {
            var firstPurchase = new Purchase()
            {
                ReplenishmentDate = new DateTime(2020, 10, 01),
                Quantity = 2,
                ItemId = 1
            };

            var secondPurchase = new Purchase()
            {
                ReplenishmentDate = new DateTime(2020, 10, 03),
                Quantity = 3,
                ItemId = 1
            };

            var list = new List<Purchase>() { firstPurchase, secondPurchase };
            return list;
        }
    }
}
