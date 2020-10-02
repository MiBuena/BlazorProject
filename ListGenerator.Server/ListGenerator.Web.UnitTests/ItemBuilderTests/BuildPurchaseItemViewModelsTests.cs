using AutoMapper;
using ListGenerator.Shared.Interfaces;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ListGenerator.Client.Builders;
using System;
using ListGenerator.Shared.Dtos;
using System.Collections.Generic;
using ListGenerator.Client.ViewModels;

namespace ListGenerator.Web.UnitTests.ItemBuilderTests
{
    [TestFixture]
    public class BuildPurchaseItemViewModelsTests : AssertAllTest
    {
        private Mock<IDateTimeProvider> _dateTimeProviderMock;
        private Mock<IMapper> _mapperMock;
        private IItemBuilder _itemBuilder;


        [SetUp]
        public void Init()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _mapperMock = new Mock<IMapper>();
            _itemBuilder = new ItemBuilder(_dateTimeProviderMock.Object, _mapperMock.Object);
        }

        [Test]
        public void Should_ReturnCollectionWith1ItemViewModel_When_InputDtoCollectionHas1ItemDto()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var nonUrgentItemDto = BuildNotUrgentItemDto();
            var nonUrgentItemViewModel = BuildNonUrgentPurchaseItemViewModel();

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>();
            nonUrgentItemDtoCollection.Add(nonUrgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            result.Count.Should().Be(1);
        }

        private ItemDto BuildNotUrgentItemDto()
        {
            var item = new ItemDto()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 04),
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
                NextReplenishmentDate = new DateTime(2020, 10, 04),
            };

            return purchaseItem;
        }
    }
}
