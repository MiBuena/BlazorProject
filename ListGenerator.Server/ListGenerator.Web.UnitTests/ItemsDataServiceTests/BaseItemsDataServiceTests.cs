using AutoMapper;
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
            ItemsRepositoryMock = new Mock<IRepository<Item>>();
            MapperMock = new Mock<IMapper>();
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
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());
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

        protected Item BuildThirdItem()
        {
            var thirdItem = new Item()
            {
                Id = 3,
                Name = "Biscuits",
                NextReplenishmentDate = new DateTime(2020, 10, 07),
                ReplenishmentPeriod = 5,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            return thirdItem;
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
            var firstItem = new Item()
            {
                Id = 1,
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
