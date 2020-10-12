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
        public void Should_ReturnOneItem_When_OneItemNameOfThisUserContainsSearchWord()
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

            var filteredItems = new List<Item>();
            filteredItems.Add(filteredItem);


            var filteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var filteredItemNameDtos = new List<ItemNameDto>();
            filteredItemNameDtos.Add(filteredItemNameDto);

            _mapperMock
                .Setup(c => c.ProjectTo<ItemNameDto>(
                    It.IsAny<IQueryable>(),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("d", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.Count().Should().Be(1);
        }


        [Test]
        public void Should_ReturnItemsNames_When_ItemsNamesOfThisUserContainSearchWord()
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

            var filteredItems = new List<Item>();
            filteredItems.Add(filteredItem);


            var filteredItemNameDto = new ItemNameDto()
            {
                Name = "Bread",
            };

            var filteredItemNameDtos = new List<ItemNameDto>();
            filteredItemNameDtos.Add(filteredItemNameDto);

            _mapperMock
                .Setup(c => c.ProjectTo<ItemNameDto>(
                    It.IsAny<IQueryable>(),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());

            //Act
            var result = _itemsDataService.GetItemsNames("d", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

            //Assert
            result.Data.First().Name.Should().Be("Bread");
        }
    }
}
