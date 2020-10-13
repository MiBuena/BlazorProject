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
using System.Collections;
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
        public void Should_ReturnSuccessResponseWithOneEntry_When_OneItemNameOfThisUserContainsSearchWord()
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
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("d", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
                () => result.Data.Count().Should().Be(1),
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull()
                );
        }

        [Test]
        public void Should_ReturnResponseWithCorrectItemName_When_OneItemNameOfThisUserContainSearchWord()
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
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
                .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("d", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.First().Name.Should().Be("Bread");
        }

        [Test]
        public void Should_ReturnSuccessResponseWithTwoEntries_When_TwoItemsNamesOfThisUserContainSearchWord()
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
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("B", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
                () => result.Data.Count().Should().Be(2),
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull()
                );
        }


        [Test]
        public void Should_ReturnResponseWithCorrectItemsNames_When_TwoItemsNamesOfThisUserContainSearchWord()
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
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("B", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
            () => result.Data.First().Name.Should().Be("Bread"),
            () => result.Data.Skip(1).First().Name.Should().Be("Biscuits"));
        }

        [Test]
        public void Should_ReturnSuccessResponseWithOneEntry_When_OneItemNameOfThisUserContainsSearchWord_SearchShouldBeCaseInsensitive()
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
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("R", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
                () => result.Data.Count().Should().Be(1),
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull()
            );
        }

        [Test]
        public void Should_ReturnResponseWithCorrectItemName_When_OneItemNameOfThisUserContainSearchWord_SearchShouldBeCaseInsensitive()
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
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("Re", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.First().Name.Should().Be("Bread");
        }

        [Test]
        public void Should_ReturnSuccessResponseWithNoEntries_When_ThereAreNoItemsInRepository()
        {
            //Arrange
            var allItems = new List<Item>().AsQueryable();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItems = new List<Item>();
            var filteredItemNameDtos = new List<ItemNameDto>();

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("B", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
                () => result.Data.Count().Should().Be(0),
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull()
                );
        }

        [Test]
        public void Should_ReturnErrorResponse_When_AnExceptionOccursInRepositoryAllMethod()
        {
            //Arrange
            var allItems = new List<Item>().AsQueryable();
            _itemsRepositoryMock.Setup(x => x.All()).Throws(new Exception());

            var filteredItems = new List<Item>();
            var filteredItemNameDtos = new List<ItemNameDto>();

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("B", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
                () => result.IsSuccess.Should().BeFalse(),
                () => result.ErrorMessage.Should().Be("An error occured while getting items names."),
                () => result.Data.Should().BeNull()
            );
        }

        [Test]
        public void Should_ReturnErrorResponse_When_AnExceptionOccursInProjectToMethod()
        {
            //Arrange
            var allItems = new List<Item>().AsQueryable();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItems = new List<Item>();

            _mapperMock.Setup(c => c.ProjectTo(
                It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                It.IsAny<object>(),
                It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
                .Throws(new Exception());

            //Act
            var result = _itemsDataService.GetItemsNames("B", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
                () => result.IsSuccess.Should().BeFalse(),
                () => result.ErrorMessage.Should().Be("An error occured while getting items names."),
                () => result.Data.Should().BeNull()
            );
        }

        [Test]
        public void Should_ReturnErrorResponse_When_SearchWordIsNull()
        {
            //Arrange
            var allItems = new List<Item>().AsQueryable();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItems = new List<Item>();
            var filteredItemNameDtos = new List<ItemNameDto>();

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());


            //Act
            var result = _itemsDataService.GetItemsNames(null, "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
                () => result.IsSuccess.Should().BeFalse(),
                () => result.ErrorMessage.Should().Be("An error occured while getting items names."),
                () => result.Data.Should().BeNull()
            );
        }

        [Test]
        public void Should_ReturnErrorResponse_When_UserIdIsNull()
        {
            //Arrange
            var allItems = new List<Item>().AsQueryable();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItems = new List<Item>();
            var filteredItemNameDtos = new List<ItemNameDto>();

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());


            //Act
            var result = _itemsDataService.GetItemsNames("B", null);

            //Assert
            AssertHelper.AssertAll(
                () => result.IsSuccess.Should().BeFalse(),
                () => result.ErrorMessage.Should().Be("An error occured while getting items names."),
                () => result.Data.Should().BeNull()
            );
        }

        [Test]
        public void Should_ReturnErrorResponse_When_UserIdIsEmpty()
        {
            //Arrange
            var allItems = new List<Item>().AsQueryable();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItems = new List<Item>();
            var filteredItemNameDtos = new List<ItemNameDto>();

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("B", string.Empty);

            //Assert
            AssertHelper.AssertAll(
                () => result.IsSuccess.Should().BeFalse(),
                () => result.ErrorMessage.Should().Be("An error occured while getting items names."),
                () => result.Data.Should().BeNull()
            );
        }


        [Test]
        public void Should_ReturnSuccessResponseWithNoEntries_When_UserIdDoesNotExist()
        {
            //Arrange
            var allItems = ItemsTestHelper.BuildItemsCollection();
            _itemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItems = new List<Item>();
            var filteredItemNameDtos = new List<ItemNameDto>();

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("Re", "1111");


            //Assert
            AssertHelper.AssertAll(
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull(),
                () => result.Data.Count().Should().Be(0)
            );
        }

        [Test]
        public void Should_ReturnSuccessResponseWithAllItemsNamesOfThisUser_When_SearchWordIsEmptyString()
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
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var thirdFilteredItem = new Item()
            {
                Id = 3,
                Name = "Biscuits",
                NextReplenishmentDate = new DateTime(2020, 10, 07),
                ReplenishmentPeriod = 5,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { firstFilteredItem, secondFilteredItem, thirdFilteredItem };

            var firstFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var secondFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Cheese",
            };

            var thirdFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Biscuits",
            };

            var filteredItemNameDtos = new List<ItemNameDto>() { firstFilteredItemNameDto, secondFilteredItemNameDto, thirdFilteredItemNameDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames(string.Empty, "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
                () => result.Data.Count().Should().Be(3),
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull());
        }

        [Test]
        public void Should_ReturnResponseWithCorrectItemsNamesOfAllItemsOfThisUser_When_SearchWordIsEmptyString()
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
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var thirdFilteredItem = new Item()
            {
                Id = 3,
                Name = "Biscuits",
                NextReplenishmentDate = new DateTime(2020, 10, 07),
                ReplenishmentPeriod = 5,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0"
            };

            var filteredItems = new List<Item>() { firstFilteredItem, secondFilteredItem, thirdFilteredItem };

            var firstFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var secondFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Cheese",
            };

            var thirdFilteredItemNameDto = new ItemNameDto()
            {
                Name = "Biscuits",
            };

            var filteredItemNameDtos = new List<ItemNameDto>() { firstFilteredItemNameDto, secondFilteredItemNameDto, thirdFilteredItemNameDto };

            _mapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames(string.Empty, "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            AssertHelper.AssertAll(
                () => result.Data.First().Name.Should().Be("Bread"),
                () => result.Data.Skip(1).First().Name.Should().Be("Cheese"),
                () => result.Data.Skip(2).First().Name.Should().Be("Biscuits")
            );
        }
    }
}
