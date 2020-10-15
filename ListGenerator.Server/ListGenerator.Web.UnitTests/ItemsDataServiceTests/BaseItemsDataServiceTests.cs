﻿using AutoMapper;
using ListGenerator.Data.Entities;
using ListGenerator.Data.Interfaces;
using ListGenerator.Server.Interfaces;
using ListGenerator.Server.Services;
using ListGenerator.Shared.Dtos;
using ListGenerator.Web.UnitTests.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemsDataServiceTests
{
    public class BaseItemsDataServiceTests
    {
        [SetUp]
        protected virtual void Init()
        {
            ItemsRepositoryMock = new Mock<IRepository<Item>>(MockBehavior.Strict);
            MapperMock = new Mock<IMapper>(MockBehavior.Strict);
            ItemsDataService = new ItemsDataService(ItemsRepositoryMock.Object, MapperMock.Object);
        }

        protected Mock<IRepository<Item>> ItemsRepositoryMock { get; private set; }
        protected Mock<IMapper> MapperMock { get; private set; }
        protected IItemsDataService ItemsDataService { get; private set; }

        protected void InitializeMocksWithEmptyCollection()
        {
            var allItems = new List<Item>().AsQueryable();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItems = new List<Item>();
            var filteredItemNameDtos = new List<ItemNameDto>();

            MapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    null,
                    It.Is<Expression<Func<ItemNameDto, object>>[]>(x => x.Length == 0)))
                .Returns(filteredItemNameDtos.AsQueryable());
        }

        protected FilterPatemetersDto BuildParametersDto(int skipItems = 0, int pageSize = 2)
        {
            var filterParameters = new FilterPatemetersDto()
            {
                PageSize = pageSize,
                SkipItems = skipItems
            };

            return filterParameters;
        }

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

        protected IQueryable<Item> BuildAdditionalItemsCollection()
        {
            var sixthItemPurchases = BuildSixthItemPurchases();

            var sixthItem = new Item()
            {
                Id = 6,
                Name = "Barley",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 2,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0",
                Purchases = sixthItemPurchases
            };

            var seventhItem = new Item()
            {
                Id = 7,
                Name = "Bacon",
                NextReplenishmentDate = new DateTime(2020, 10, 2),
                ReplenishmentPeriod = 5,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var collection = new List<Item>() { sixthItem, seventhItem };
            return collection.AsQueryable();
        }

        private ICollection<Purchase> BuildSixthItemPurchases()
        {
            var firstPurchase = new Purchase()
            {
                ReplenishmentDate = new DateTime(2020, 10, 02),
                Quantity = 2,
                ItemId = 6
            };

            var list = new List<Purchase>() { firstPurchase };
            return list;
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

        protected Item BuildFirstItemWithoutId()
        {
            var firstItem = new Item()
            {
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            return firstItem;
        }

        protected ItemNameDto BuildFirstItemNameDto()
        {
            var firstItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            return firstItemNameDto;
        }

        protected ItemNameDto BuildSecondItemNameDto()
        {
            var secondItemNameDto = new ItemNameDto()
            {
                Name = "Cheese",
            };

            return secondItemNameDto;
        }

        protected ItemNameDto BuildThirdItemNameDto()
        {
            var secondItemNameDto = new ItemNameDto()
            {
                Name = "Biscuits",
            };

            return secondItemNameDto;
        }

        protected ItemDto BuildFirstItemDto()
        {
            var firstItemDto = new ItemDto()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1
            };

            return firstItemDto;
        }

        protected ItemDto BuildFirstItemDtoWithoutId()
        {
            var firstItemDto = new ItemDto()
            {
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1
            };

            return firstItemDto;
        }

        protected ItemDto BuildSecondItemDto()
        {
            var secondItemDto = new ItemDto()
            {
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2
            };

            return secondItemDto;
        }
    }
}
