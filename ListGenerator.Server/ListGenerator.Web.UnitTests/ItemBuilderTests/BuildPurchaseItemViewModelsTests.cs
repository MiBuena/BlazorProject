﻿using AutoMapper;
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
    public class BuildPurchaseItemViewModelsTests : BaseItemBuilderTests
    {
        [SetUp]
        public override void Init()
        {
            base.Init();
        }

        [Test]
        public void Should_ReturnCollectionWithOneItemViewModel_When_InputDtoCollectionHasOneItemDto()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            var nonUrgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 06));
            var nonUrgentPurchaseItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 06));

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>() { nonUrgentItemDto };

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            result.Count.Should().Be(1);
        }


        [Test]
        public void Should_ReturnCollectionWith1Entry_WithSomePropertiesMappedFromInputDto_When_InputCollectionItemDtoIsNotUrgent()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            var nonUrgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 06));
            var nonUrgentPurchaseItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 06));

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>() { nonUrgentItemDto };

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            AssertHelper.AssertAll(
                () => result.FirstOrDefault().ItemId.Should().Be(1),
                () => result.FirstOrDefault().Name.Should().Be("Bread"),
                () => result.FirstOrDefault().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 06))
            );
        }


        [Test]
        public void Should_ReturnCollectionWith1Entry_WithCorrectRecommendedPurchaseQuantity_When_InputCollectionItemDtoIsNotUrgent()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            var nonUrgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 06));
            var nonUrgentPurchaseItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 06));

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>() { nonUrgentItemDto };

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().Quantity.Should().Be("5");
        }


        [Test]
        public void Should_ReturnCollectionWith1Entry_WithCorrectReplenishmentDate_When_InputCollectionItemDtoIsNotUrgent()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            var nonUrgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 06));
            var nonUrgentPurchaseItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 06));

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>() { nonUrgentItemDto };

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().ReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 01));
        }

        [Test]
        public void Should_ReturnCollectionWith1Entry_WithEmptyReplenishmentSignalClass_When_InputCollectionItemDtoIsNotUrgent()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            var nonUrgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 06));
            var nonUrgentPurchaseItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 06));

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var nonUrgentItemDtoCollection = new List<ItemDto>() { nonUrgentItemDto };

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, nonUrgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().ReplenishmentSignalClass.Should().BeEmpty();
        }

        [Test]
        public void Should_ReturnCollectionWith1Entry_WithSomePropertiesMappedFromInputItemDto_When_InputCollectionItemDtoIsUrgent()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            var urgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 02));
            var urgentItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 02));

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(urgentItemDto))
                .Returns(urgentItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var urgentItemDtoCollection = new List<ItemDto>();
            urgentItemDtoCollection.Add(urgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, urgentItemDtoCollection);


            //Assert
            AssertHelper.AssertAll(
                () => result.FirstOrDefault().ItemId.Should().Be(1),
                () => result.FirstOrDefault().Name.Should().Be("Bread"),
                () => result.FirstOrDefault().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 02))
            );
        }

        [Test]
        public void Should_ReturnCollectionWith1Entry_WithCorrectRecommendedPurchaseQuantity_When_InputCollectionItemDtoIsUrgent()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            var urgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 02));
            var urgentItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 02));

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
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            var urgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 02));
            var urgentItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 02));

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(urgentItemDto))
                .Returns(urgentItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var urgentItemDtoCollection = new List<ItemDto>();
            urgentItemDtoCollection.Add(urgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, urgentItemDtoCollection);


            //Assert
            result.FirstOrDefault().ReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 01));
        }

        [Test]
        public void Should_ReturnCollectionWith1Entry_WithFilledReplenishmentSignalClass_When_InputCollectionItemDtoIsUrgent()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            var urgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 02));
            var urgentItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 02));

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

            var itemDtoWithNextReplenishmentDateOnFirstReplenishmentDate = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 04));
            var purchaseItemViewModelWithNextReplenishmentDateOnFirstReplenishmentDate = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 04));

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(itemDtoWithNextReplenishmentDateOnFirstReplenishmentDate))
                .Returns(purchaseItemViewModelWithNextReplenishmentDateOnFirstReplenishmentDate);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var itemDtoCollection = new List<ItemDto>();
            itemDtoCollection.Add(itemDtoWithNextReplenishmentDateOnFirstReplenishmentDate);

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

            var nonUrgentItemDto = ItemsTestHelper.BuildItemDto(new DateTime(2020, 10, 06));
            var nonUrgentPurchaseItemViewModel = ItemsTestHelper.BuildPurchaseItemViewModel(new DateTime(2020, 10, 06));

            _mapperMock.Setup(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto))
                .Returns(nonUrgentPurchaseItemViewModel);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var itemDtoCollection = new List<ItemDto>();
            itemDtoCollection.Add(nonUrgentItemDto);

            //Act
            var result = _itemBuilder.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, itemDtoCollection);


            //Assert
            _mapperMock.Verify(c => c.Map<ItemDto, PurchaseItemViewModel>(nonUrgentItemDto), Times.Once());
        }
    }
}
