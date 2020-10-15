using FluentAssertions;
using ListGenerator.Shared.Dtos;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemsDataServiceTests
{
    [TestFixture]
    public class GetItemsOverviewPageModelTests : BaseItemsDataServiceTests
    {
        [SetUp]
        protected override void Init()
        {
            base.Init();
        }


        [Test]
        public void Should_ReturnResponseWithFirstPageUserItems_When_FirstPage()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filterParameters = BuildParametersDto();

            //Act
            var response = ItemsDataService.GetItemsOverviewPageModel("ab70793b-cec8-4eba-99f3-cbad0b1649d0", filterParameters);


            //Assert
            AssertHelper.AssertAll(
                 () => response.Data.OverviewItems.Count().Should().Be(2),
                 () => response.Data.OverviewItems.First().Id.Should().Be(1),
                 () => response.Data.OverviewItems.First().Name.Should().Be("Bread"),
                 () => response.Data.OverviewItems.First().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 06)),
                 () => response.Data.OverviewItems.First().ReplenishmentPeriod.Should().Be(1),
                 () => response.Data.OverviewItems.First().LastReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 03)),
                 () => response.Data.OverviewItems.First().LastReplenishmentQuantity.Should().Be(3),

                 () => response.Data.OverviewItems.Skip(1).First().Id.Should().Be(2),
                 () => response.Data.OverviewItems.Skip(1).First().Name.Should().Be("Cheese"),
                 () => response.Data.OverviewItems.Skip(1).First().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 08)),
                 () => response.Data.OverviewItems.Skip(1).First().ReplenishmentPeriod.Should().Be(2)
                 //() => response.Data.OverviewItems.Skip(1).First().LastReplenishmentDate.Should().BeNull()
                 //() => response.Data.OverviewItems.Skip(1).First().LastReplenishmentQuantity.Should().BeNull()
                 );
        }


        [Test]
        public void Should_ReturnSuccessResponse_When_FirstPage()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filterParameters = BuildParametersDto();

            //Act
            var response = ItemsDataService.GetItemsOverviewPageModel("ab70793b-cec8-4eba-99f3-cbad0b1649d0", filterParameters);


            //Assert
            AssertHelper.AssertAll(
                 () => response.IsSuccess.Should().BeTrue(),
                 () => response.ErrorMessage.Should().BeNull()
                 );
        }


        [Test]
        public void Should_ReturnResponseWithSecondPageUserItems_When_SecondPage()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filterParameters = BuildParametersDto(2);

            //Act
            var response = ItemsDataService.GetItemsOverviewPageModel("ab70793b-cec8-4eba-99f3-cbad0b1649d0", filterParameters);


            //Assert
            AssertHelper.AssertAll(
                 () => response.Data.OverviewItems.Count().Should().Be(1),
                 () => response.Data.OverviewItems.First().Id.Should().Be(3),
                 () => response.Data.OverviewItems.First().Name.Should().Be("Biscuits"),
                 () => response.Data.OverviewItems.First().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 07)),
                 () => response.Data.OverviewItems.First().ReplenishmentPeriod.Should().Be(5),
                 () => response.Data.OverviewItems.First().LastReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 02)),
                 () => response.Data.OverviewItems.First().LastReplenishmentQuantity.Should().Be(1)
                 );
        }

        [Test]
        public void Should_ReturnSuccessResponse_When_SecondPage()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filterParameters = BuildParametersDto(2);

            //Act
            var response = ItemsDataService.GetItemsOverviewPageModel("ab70793b-cec8-4eba-99f3-cbad0b1649d0", filterParameters);


            //Assert
            AssertHelper.AssertAll(
                 () => response.IsSuccess.Should().BeTrue(),
                 () => response.ErrorMessage.Should().BeNull()
                 );
        }


        [Test]
        public void Should_ReturnResponseWithAllUserItems_When_PageSizeBiggerThanUserItemsCount()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filterParameters = BuildParametersDto(0, 5);


            //Act
            var response = ItemsDataService.GetItemsOverviewPageModel("ab70793b-cec8-4eba-99f3-cbad0b1649d0", filterParameters);



            //Assert
            AssertHelper.AssertAll(
                 () => response.Data.OverviewItems.Count().Should().Be(3),
                 () => response.Data.OverviewItems.First().Id.Should().Be(1),
                 () => response.Data.OverviewItems.First().Name.Should().Be("Bread"),
                 () => response.Data.OverviewItems.First().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 06)),
                 () => response.Data.OverviewItems.First().ReplenishmentPeriod.Should().Be(1),
                 () => response.Data.OverviewItems.First().LastReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 03)),
                 () => response.Data.OverviewItems.First().LastReplenishmentQuantity.Should().Be(3),

                 () => response.Data.OverviewItems.Skip(1).First().Id.Should().Be(2),
                 () => response.Data.OverviewItems.Skip(1).First().Name.Should().Be("Cheese"),
                 () => response.Data.OverviewItems.Skip(1).First().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 08)),
                 () => response.Data.OverviewItems.Skip(1).First().ReplenishmentPeriod.Should().Be(2),
                 //() => response.Data.OverviewItems.Skip(1).First().LastReplenishmentDate.Should().BeNull()
                 //() => response.Data.OverviewItems.Skip(1).First().LastReplenishmentQuantity.Should().BeNull()

                 () => response.Data.OverviewItems.Skip(2).First().Id.Should().Be(3),
                 () => response.Data.OverviewItems.Skip(2).First().Name.Should().Be("Biscuits"),
                 () => response.Data.OverviewItems.Skip(2).First().NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 07)),
                 () => response.Data.OverviewItems.Skip(2).First().ReplenishmentPeriod.Should().Be(5),
                 () => response.Data.OverviewItems.Skip(2).First().LastReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 02)),
                 () => response.Data.OverviewItems.Skip(2).First().LastReplenishmentQuantity.Should().Be(1)
                 );
        }

        [Test]
        public void Should_ReturnSuccessResponse_When_PageSizeBiggerThanUserItemsCount()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filterParameters = BuildParametersDto(2, 5);

            //Act
            var response = ItemsDataService.GetItemsOverviewPageModel("ab70793b-cec8-4eba-99f3-cbad0b1649d0", filterParameters);


            //Assert
            AssertHelper.AssertAll(
                 () => response.IsSuccess.Should().BeTrue(),
                 () => response.ErrorMessage.Should().BeNull()
                 );
        }
    }
}
