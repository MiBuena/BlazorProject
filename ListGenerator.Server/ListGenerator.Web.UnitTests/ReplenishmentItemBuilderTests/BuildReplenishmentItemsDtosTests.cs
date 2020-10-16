using AutoMapper;
using FluentAssertions;
using ListGenerator.Data.Entities;
using ListGenerator.Server.Builders;
using ListGenerator.Server.Interfaces;
using ListGenerator.Shared.Dtos;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Web.UnitTests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListGenerator.Web.UnitTests.ReplenishmentItemBuilderTests
{
    [TestFixture]
    public class BuildReplenishmentItemsDtosTests : BaseItemsTests
    {
        [SetUp]
        public virtual void Init()
        {
            DateTimeProviderMock = new Mock<IDateTimeProvider>(MockBehavior.Strict);
            MapperMock = new Mock<IMapper>(MockBehavior.Strict);
            ReplenishmentItemBuilder = new ReplenishmentItemBuilder(DateTimeProviderMock.Object, MapperMock.Object);
        }

        protected Mock<IDateTimeProvider> DateTimeProviderMock { get; private set; }
        protected Mock<IMapper> MapperMock { get; private set; }
        protected IReplenishmentItemBuilder ReplenishmentItemBuilder { get; private set; }


        [Test]
        public void Should_ReturnCollectionWithThreeDtos_When_InputCollectionHasThreeItems()
        {
            //Arrange
            var items = InitializeWithCollection();

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);


            //Act
            var dtos = ReplenishmentItemBuilder.BuildReplenishmentItemsDtos(firstReplenishmentDate, secondReplenishmentDate, items);


            //Assert
            dtos.Count().Should().Be(3);
        }

        [Test]
        public void Should_ReturnCollection_WithUrgentDto_WithCorrectMappedProperties_When_InputCollectionHasAnUrgentItem()
        {
            //Arrange
            var items = InitializeWithCollection();

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);


            //Act
            var dtos = ReplenishmentItemBuilder.BuildReplenishmentItemsDtos(firstReplenishmentDate, secondReplenishmentDate, items);


            //Assert
            AssertHelper.AssertAll(
                () => dtos.First().Id.Should().Be(1),
                () => dtos.First().Name.Should().Be("Popcorn"),
                () => dtos.First().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 02))
                );
        }


        [Test]
        public void Should_ReturnCollection_WithUrgentDto_WithCorrectQuantity_When_InputCollectionHasAnUrgentItem()
        {
            //Arrange
            var items = InitializeWithCollection();

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);


            //Act
            var dtos = ReplenishmentItemBuilder.BuildReplenishmentItemsDtos(firstReplenishmentDate, secondReplenishmentDate, items);


            //Assert
            AssertHelper.AssertAll(
                () => dtos.First().Quantity.Should().Be(3)
                );
        }

        [Test]
        public void Should_ReturnCollection_WithUrgentDto_WithCorrectReplenishmentDate_When_InputCollectionHasAnUrgentItem()
        {
            //Arrange
            var items = InitializeWithCollection();

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);


            //Act
            var dtos = ReplenishmentItemBuilder.BuildReplenishmentItemsDtos(firstReplenishmentDate, secondReplenishmentDate, items);


            //Assert
            AssertHelper.AssertAll(
                () => dtos.First().ReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 01))
                );
        }

        [Test]
        public void Should_ReturnCollection_WithUrgentDto_WithUrgencyPropertySetToTrue_When_InputCollectionHasAnUrgentItem()
        {
            //Arrange
            var items = InitializeWithCollection();

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);


            //Act
            var dtos = ReplenishmentItemBuilder.BuildReplenishmentItemsDtos(firstReplenishmentDate, secondReplenishmentDate, items);


            //Assert
            AssertHelper.AssertAll(
                () => dtos.First().ItemNeedsReplenishmentUrgently.Should().BeTrue()
                );
        }

        [Test]
        public void Should_ReturnCollection_WithDtoWithNextReplDateOnFirstReplDate_WithCorrectMappedProperties_When_InputCollectionHasAnItemWithNextReplDateOnFirstReplDate()
        {
            //Arrange
            var items = InitializeWithCollection();

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);


            //Act
            var dtos = ReplenishmentItemBuilder.BuildReplenishmentItemsDtos(firstReplenishmentDate, secondReplenishmentDate, items);


            //Assert
            AssertHelper.AssertAll(
                () => dtos.Skip(1).First().Id.Should().Be(2),
                () => dtos.Skip(1).First().Name.Should().Be("Brownies"),
                () => dtos.Skip(1).First().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 04))
                );
        }


        [Test]
        public void Should_ReturnCollection_WithDtoWithNextReplDateOnFirstReplDate_WithCorrectQuantity_When_InputCollectionHasAnItemWithNextReplDateOnFirstReplDate()
        {
            //Arrange
            var items = InitializeWithCollection();

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);


            //Act
            var dtos = ReplenishmentItemBuilder.BuildReplenishmentItemsDtos(firstReplenishmentDate, secondReplenishmentDate, items);


            //Assert
            AssertHelper.AssertAll(
                () => dtos.Skip(1).First().Quantity.Should().Be(7)
                );
        }

        [Test]
        public void Should_ReturnCollection_WithDtoWithNextReplDateOnFirstReplDate_WithCorrectReplenishmentDate_When_InputCollectionHasAnItemWithNextReplDateOnFirstReplDate()
        {
            //Arrange
            var items = InitializeWithCollection();

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);


            //Act
            var dtos = ReplenishmentItemBuilder.BuildReplenishmentItemsDtos(firstReplenishmentDate, secondReplenishmentDate, items);


            //Assert
            AssertHelper.AssertAll(
                () => dtos.Skip(1).First().ReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 01))
                );
        }

        [Test]
        public void Should_ReturnCollection_WithDtoWithNextReplDateOnFirstReplDate_WithUrgencyPropertySetToTrue_When_InputCollectionHasAnItemWithNextReplDateOnFirstReplDate()
        {
            //Arrange
            var items = InitializeWithCollection();

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);


            //Act
            var dtos = ReplenishmentItemBuilder.BuildReplenishmentItemsDtos(firstReplenishmentDate, secondReplenishmentDate, items);


            //Assert
            AssertHelper.AssertAll(
                () => dtos.Skip(1).First().ItemNeedsReplenishmentUrgently.Should().BeFalse()
                );
        }

        private IEnumerable<Item> InitializeWithCollection()
        {
            ItemsTestHelper.InitializeDateTimeProviderMock(DateTimeProviderMock);

            var urgentItem = BuildUrgentItem(new DateTime(2020, 10, 02));
            var urgentItemDto = BuildUrgentReplenishmentItemDto(new DateTime(2020, 10, 02));

            var itemOnFirstReplDate = BuildItemWithReplenishmentOnFirstReplDate(new DateTime(2020, 10, 04));
            var itemDtoOnFirstReplDate = BuildItemDtoWithReplenishmentOnFirstReplDate(new DateTime(2020, 10, 04));

            var nonUrgentItem = BuildNonUrgentItem(new DateTime(2020, 10, 06));
            var nonUrgentItemDto = BuildNonUrgentReplenishmentItemDto(new DateTime(2020, 10, 06));

            MapperMock.Setup(c => c.Map<Item, ReplenishmentItemDto>(urgentItem))
                .Returns(urgentItemDto);

            MapperMock.Setup(c => c.Map<Item, ReplenishmentItemDto>(itemOnFirstReplDate))
                .Returns(itemDtoOnFirstReplDate);

            MapperMock.Setup(c => c.Map<Item, ReplenishmentItemDto>(nonUrgentItem))
                .Returns(nonUrgentItemDto);

            var collection = new List<Item>() { urgentItem, itemOnFirstReplDate, nonUrgentItem };

            return collection;
        }


        private Item BuildUrgentItem(DateTime nextReplenishmentDate)
        {
            var item = new Item()
            {
                Id = 1,
                Name = "Popcorn",
                NextReplenishmentDate = nextReplenishmentDate,
                ReplenishmentPeriod = 4,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0",
            };

            return item;
        }

        private static ReplenishmentItemDto BuildUrgentReplenishmentItemDto(DateTime nextReplenishmentDate)
        {
            var itemDto = new ReplenishmentItemDto()
            {
                Id = 1,
                Name = "Popcorn",
                NextReplenishmentDate = nextReplenishmentDate,
                ReplenishmentDate = new DateTime(2020, 10, 01)
            };

            return itemDto;
        }
        private Item BuildItemWithReplenishmentOnFirstReplDate(DateTime nextReplenishmentDate)
        {
            var firstItem = new Item()
            {
                Id = 2,
                Name = "Brownies",
                NextReplenishmentDate = nextReplenishmentDate,
                ReplenishmentPeriod = 1,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0",
            };

            return firstItem;
        }

        private static ReplenishmentItemDto BuildItemDtoWithReplenishmentOnFirstReplDate(DateTime nextReplenishmentDate)
        {
            var itemDto = new ReplenishmentItemDto()
            {
                Id = 2,
                Name = "Brownies",
                NextReplenishmentDate = nextReplenishmentDate,
                ReplenishmentDate = new DateTime(2020, 10, 01)
            };

            return itemDto;
        }

        private Item BuildNonUrgentItem(DateTime nextReplenishmentDate)
        {
            var firstItem = new Item()
            {
                Id = 3,
                Name = "Yoghurt",
                NextReplenishmentDate = nextReplenishmentDate,
                ReplenishmentPeriod = 2,
                UserId = "ab70793b-cec8-4eba-99f3-cbad0b1649d0",
            };

            return firstItem;
        }

        private static ReplenishmentItemDto BuildNonUrgentReplenishmentItemDto(DateTime nextReplenishmentDate)
        {
            var itemDto = new ReplenishmentItemDto()
            {
                Id = 3,
                Name = "Yoghurt",
                NextReplenishmentDate = nextReplenishmentDate,
                ReplenishmentDate = new DateTime(2020, 10, 01)
            };

            return itemDto;
        }
    }
}
