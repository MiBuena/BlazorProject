using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ListGenerator.Web.UnitTests.ReplenishmentDataServiceTests
{
    public class GetShoppingListTests : BaseReplenishmentDataServiceTests
    {
        [SetUp]
        protected override void Init()
        {
            base.Init();
        }

        //[Test]
        //public void Should_ReturnSuccessResponse_When_ValidInputparameters()
        //{
        //    //Arrange
        //    var allItems = BuildItemsCollection();
        //    ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);


        //    //Act
        //    var response = ReplenishmentDataService.GetShoppingList("20-10-2020", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

        //    //Assert
        //    AssertHelper.AssertAll(
        //        () => response.IsSuccess.Should().BeTrue(),
        //        () => response.ErrorMessage.Should().BeNull()
        //        );
        //}


        //[Test]
        //public void Should_ReturnResponseWithAllShoppingItems_When_ValidInputparameters()
        //{
        //    //Arrange
        //    var allItems = BuildItemsCollection();
        //    ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);


        //    //Act
        //    var response = ReplenishmentDataService.GetShoppingList("20-10-2020", "ab70793b-cec8-4eba-99f3-cbad0b1649d0");

        //    //Assert
        //    AssertHelper.AssertAll(
        //        () => response.Data.Count().Should().Be(3),

        //        () => response.Data.First().Id.Should().Be(1),

        //        );
        //}
    }
}
