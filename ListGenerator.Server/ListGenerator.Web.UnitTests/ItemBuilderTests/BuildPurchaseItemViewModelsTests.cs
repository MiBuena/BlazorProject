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
using System.Linq;
using ListGenerator.Web.UnitTests.Helpers;

namespace ListGenerator.Web.UnitTests.ItemBuilderTests
{
    [TestFixture]
    public class BuildPurchaseItemViewModelsTests : AssertAllTest
    {
        private Mock<IDateTimeProvider> _dateTimeProviderMock;
        private Mock<IMapper> _mapperMock;
        private IItemBuilder _itemBuilder;
        private ItemDto nonUrgentItemDto;
        private PurchaseItemViewModel nonUrgentPurchaseItemViewModel;


        [SetUp]
        public void Init()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _mapperMock = new Mock<IMapper>();
            _itemBuilder = new ItemBuilder(_dateTimeProviderMock.Object, _mapperMock.Object);
            nonUrgentItemDto = BuildNotUrgentItemDto();
            nonUrgentPurchaseItemViewModel = ItemsTestHelper.BuildNonUrgentPurchaseItemViewModel();
        }

        [Test]
        public void Should_ReturnCollectionWith1ItemViewModel_When_InputDtoCollectionHas1ItemDto()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>();
            nonUrgentItemDtoCollection.Add(nonUrgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            result.Count.Should().Be(1);
        }


        [Test]
        public void Should_ReturnCollectionWith1Entry_WithSomePropertiesMappedFromInputDto_When_InputCollectionItemDtoIsNotUrgent()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>();
            nonUrgentItemDtoCollection.Add(nonUrgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            AssertAll(
                () => result.FirstOrDefault().ItemId.Should().Be(1),
                () => result.FirstOrDefault().Name.Should().Be("Bread"),
                () => result.FirstOrDefault().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 06))
            );
        }


        [Test]
        public void Should_ReturnCollectionWith1Entry_WithCorrectRecommendedPurchaseQuantity_When_InputCollectionItemDtoIsNotUrgent()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>();
            nonUrgentItemDtoCollection.Add(nonUrgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().Quantity.Should().Be("5");
        }


        [Test]
        public void Should_ReturnCollectionWith1Entry_WithCorrectReplenishmentDate_When_InputCollectionItemDtoIsNotUrgent()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>();
            nonUrgentItemDtoCollection.Add(nonUrgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().ReplenishmentDate.Should().BeSameDateAs(mockDate);
        }

        [Test]
        public void Should_ReturnCollectionWith1Entry_WithEmptyReplenishmentSignalClass_When_InputCollectionItemDtoIsNotUrgent()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>();
            nonUrgentItemDtoCollection.Add(nonUrgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().ReplenishmentSignalClass.Should().BeEmpty();
        }

        [Test]
        public void Should_ReturnCollectionWith1Entry_WithSomePropertiesMappedFromInputItemDto_When_InputCollectionItemDtoIsUrgent()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var urgentItemDto = BuildUrgentItemDto();
            var urgentItemViewModel = BuildUrgentPurchaseItemViewModel();

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(urgentItemDto))
                .Returns(urgentItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var urgentItemDtoCollection = new List<ItemDto>();
            urgentItemDtoCollection.Add(urgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, urgentItemDtoCollection);


            //Assert
            AssertAll(
                () => result.FirstOrDefault().ItemId.Should().Be(1),
                () => result.FirstOrDefault().Name.Should().Be("Bread"),
                () => result.FirstOrDefault().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 02))
            );
        }

        [Test]
        public void Should_ReturnCollectionWith1Entry_WithCorrectRecommendedPurchaseQuantity_When_InputCollectionItemDtoIsUrgent()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var urgentItemDto = BuildUrgentItemDto();
            var urgentItemViewModel = BuildUrgentPurchaseItemViewModel();

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(urgentItemDto))
                .Returns(urgentItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var urgentItemDtoCollection = new List<ItemDto>();
            urgentItemDtoCollection.Add(urgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, urgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().Quantity.Should().Be("9");
        }


        [Test]
        public void Should_ReturnCollectionWith1Entry_WithCorrectReplenishmentDate_When_InputCollectionItemDtoIsUrgent()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var urgentItemDto = BuildUrgentItemDto();
            var urgentItemViewModel = BuildUrgentPurchaseItemViewModel();

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(urgentItemDto))
                .Returns(urgentItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var urgentItemDtoCollection = new List<ItemDto>();
            urgentItemDtoCollection.Add(urgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, urgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().ReplenishmentDate.Should().BeSameDateAs(mockDate);
        }

        [Test]
        public void Should_ReturnCollectionWith1Entry_WithFilledReplenishmentSignalClass_When_InputCollectionItemDtoIsUrgent()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var urgentItemDto = BuildUrgentItemDto();
            var urgentItemViewModel = BuildUrgentPurchaseItemViewModel();

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(urgentItemDto))
                .Returns(urgentItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var urgentItemDtoCollection = new List<ItemDto>();
            urgentItemDtoCollection.Add(urgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, urgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().ReplenishmentSignalClass.Should().Be("itemNeedsReplenishment");
        }


        [Test]
        public void Should_ReturnCollectionWith1Entry_WithEmptyReplenishmentSignalClass_When_ItemNextReplenishmentDateIsOnFirstReplenishmentDate()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var itemDto = BuildItemDtoWithNextReplenishmentDateOnFirstReplenishmentDate();
            var itemViewModel = BuildPurchaseItemViewModelWithNextReplenishmentDateOnFirstReplenishmentDate();

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(itemDto))
                .Returns(itemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var itemDtoCollection = new List<ItemDto>();
            itemDtoCollection.Add(itemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, itemDtoCollection);


            //Assert
            result.FirstOrDefault().ReplenishmentSignalClass.Should().BeEmpty();
        }

        [Test]
        public void Should_CallMapperMapMethod_WithItemDto_Once_When_BuildMethodIsCalledWithANonEmptyInputCollection()
        {
            //Arrange
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var itemDto = BuildItemDtoWithNextReplenishmentDateOnFirstReplenishmentDate();
            var itemViewModel = BuildPurchaseItemViewModelWithNextReplenishmentDateOnFirstReplenishmentDate();

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(itemDto))
                .Returns(itemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var itemDtoCollection = new List<ItemDto>();
            itemDtoCollection.Add(itemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, itemDtoCollection);


            //Assert
            _mapperMock.Verify(c => c.Map<ItemDto, PurchaseItemViewModel>(itemDto), Times.Once());
        }

        private ItemDto BuildNotUrgentItemDto()
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

        private PurchaseItemViewModel BuildPurchaseItemViewModelWithNextReplenishmentDateOnFirstReplenishmentDate()
        {
            var purchaseItem = new PurchaseItemViewModel()
            {
                ItemId = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 04),
            };

            return purchaseItem;
        }


        private ItemDto BuildUrgentItemDto()
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

        private PurchaseItemViewModel BuildUrgentPurchaseItemViewModel()
        {
            var purchaseItem = new PurchaseItemViewModel()
            {
                ItemId = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 02),
            };

            return purchaseItem;
        }

        private ItemDto BuildItemDtoWithNextReplenishmentDateOnFirstReplenishmentDate()
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
    }
}
