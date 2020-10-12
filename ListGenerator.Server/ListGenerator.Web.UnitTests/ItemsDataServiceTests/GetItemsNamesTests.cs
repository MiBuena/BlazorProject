using AutoMapper;
using FluentAssertions;
using FluentAssertions.Common;
using ListGenerator.Client.ViewModels;
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
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemsDataServiceTests
{
    [TestFixture]
    public class GetItemsNamesTests
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
        public void Should_ReturnOneEntry_When_OneItemNameOfThisUserContainsSearchWord()
        {
            //Arrange
            var allItems = ItemsTestHelper.BuildItemsCollection();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItem = new Item()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { filteredItem };


            var filteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var filteredItemNameDtos = new List<ItemNameDto>() { filteredItemNameDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.IsAny<IQueryable<Item>>(),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("d", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.Count().Should().Be(1);
        }

        [Test]
        public void Should_ReturnCorrectItemName_When_OneItemNameOfThisUserContainSearchWord()
        {
            //Arrange
            var allItems = ItemsTestHelper.BuildItemsCollection();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItem = new Item()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { filteredItem };


            var filteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var filteredItemNameDtos = new List<ItemNameDto>() { filteredItemNameDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.IsAny<IQueryable>(),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("d", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.First().Name.Should().Be("Bread");
        }

        [Test]
        public void Should_ReturnTwoEntries_When_TwoItemsNamesOfThisUserContainSearchWord()
        {
            //Arrange
            var allItems = ItemsTestHelper.BuildItemsCollection();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var firstFilteredItem = new Item()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var secondFilteredItem = new Item()
            {
                Id = 3,
                Name = "Biscuits",
                NextReplenishmentDate = new DateTime(2020, 10, 07),
                ReplenishmentPeriod = 5,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { firstFilteredItem, secondFilteredItem };

            var firstFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var secondFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Biscuits",
            };

            var filteredItemNameDtos = new List<ItemNameDto>() { firstFilteredItemNameDto, secondFilteredItemNameDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.IsAny<IQueryable<Item>>(),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("B", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.Count().Should().Be(2);
        }


        [Test]
        public void Should_ReturnCorrectItemsNames_When_TwoItemsNamesOfThisUserContainSearchWord()
        {
            var allItems = ItemsTestHelper.BuildItemsCollection();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var firstFilteredItem = new Item()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var secondFilteredItem = new Item()
            {
                Id = 3,
                Name = "Biscuits",
                NextReplenishmentDate = new DateTime(2020, 10, 07),
                ReplenishmentPeriod = 5,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { firstFilteredItem, secondFilteredItem };

            var firstFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var secondFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Biscuits",
            };

            var filteredItemNameDtos = new List<ItemNameDto>() { firstFilteredItemNameDto, secondFilteredItemNameDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.IsAny<IQueryable<Item>>(),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("B", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.First().Name.Should().Be("Bread");
            result.Data.Skip(1).First().Name.Should().Be("Biscuits");
        }

        [Test]
        public void Should_ReturnOneEntry_When_OneItemNameOfThisUserContainsSearchWord_SearchShouldBeCaseInsensitive()
        {
            //Arrange
            var allItems = ItemsTestHelper.BuildItemsCollection();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItem = new Item()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { filteredItem };


            var filteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var filteredItemNameDtos = new List<ItemNameDto>() { filteredItemNameDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.IsAny<IQueryable<Item>>(),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("R", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.Count().Should().Be(1);
        }


        [Test]
        public void Should_ReturnCorrectItemName_When_OneItemNameOfThisUserContainSearchWord_SearchShouldBeCaseInsensitive()
        {
            //Arrange
            var allItems = ItemsTestHelper.BuildItemsCollection();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItem = new Item()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { filteredItem };


            var filteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var filteredItemNameDtos = new List<ItemNameDto>() { filteredItemNameDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.IsAny<IQueryable>(),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("Re", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.First().Name.Should().Be("Bread");
        }

        [Test]
        public void Should_ReturnNoEntries_When_ThereAreNoItemsInRepository()
        {
            //Arrange
            var allItems = new List<Item>().AsQueryable();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItemNameDtos = new List<ItemNameDto>();

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.IsAny<IQueryable<Item>>(),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("B", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.Count().Should().Be(0);
        }
    }
}
