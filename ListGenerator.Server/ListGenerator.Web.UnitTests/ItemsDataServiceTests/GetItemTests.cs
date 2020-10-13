using AutoMapper;
using FluentAssertions;
using ListGenerator.Data.Entities;
using ListGenerator.Data.Interfaces;
using ListGenerator.Server.Interfaces;
using ListGenerator.Server.Services;
using ListGenerator.Shared.Dtos;
using ListGenerator.Web.UnitTests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemsDataServiceTests
{
    [TestFixture]
    public class GetItemTests
    {
        private Mock<IRepository<Item>> _itemsRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private IItemsDataService _itemsDataService;

        [SetUp]
        public void Init()
        {
            _itemsRepositoryMock = new Mock<IRepository<Item>>();
            _mapperMock = new Mock<IMapper>();
            _itemsDataService = new ItemsDataService(_itemsRepositoryMock.Object, _mapperMock.Object);
        }

        [Test]
        public void Should_ReturnSuccessResponseWithItem_When_CurrentUserHasItemWithThisId()
        {
            //Arrange
            var allItems = ItemsTestHelper.BuildItemsCollection();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItem = new Item()
            {
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { filteredItem };


            var filteredItemDto = new ItemDto()
            {
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2,
            };

            var filteredItemDtos = new List<ItemDto>() { filteredItemDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemDto, object>>[]>()))
             .Returns(filteredItemDtos.AsQueryable());


            //Act
            var result = _itemsDataService.GetItem(2, "ab70793b-cec8-4eba-99f3-cbad0b1649d0");


            //Assert
            AssertHelper.AssertAll(
                () => result.Data.Should().NotBeNull(),
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull()
                );
        }


        [Test]
        public void Should_ReturnResponseWithCorrectItemProperties_When_CurrentUserHasItemWithThisId()
        {
            //Arrange
            var allItems = ItemsTestHelper.BuildItemsCollection();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItem = new Item()
            {
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { filteredItem };


            var filteredItemDto = new ItemDto()
            {
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2,
            };

            var filteredItemDtos = new List<ItemDto>() { filteredItemDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemDto, object>>[]>()))
             .Returns(filteredItemDtos.AsQueryable());


            //Act
            var result = _itemsDataService.GetItem(2, "ab70793b-cec8-4eba-99f3-cbad0b1649d0");


            //Assert
            AssertHelper.AssertAll(
                () => result.Data.Id.Should().Be(2),
                () => result.Data.Name.Should().Be("Cheese"),
                () => result.Data.NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 08)),
                () => result.Data.ReplenishmentPeriod.Should().Be(2)
                );
        }
    }
}
