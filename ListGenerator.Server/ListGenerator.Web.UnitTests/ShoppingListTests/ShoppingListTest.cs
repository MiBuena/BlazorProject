using Bunit;
using ListGenerator.Client.Builders;
using ListGenerator.Client.Pages;
using ListGenerator.Client.Services;
using ListGenerator.Client.ViewModels;
using ListGenerator.Shared.Dtos;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Web.UnitTests.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.ShoppingListTests
{
    [TestFixture]
    public class ShoppingListTest : BUnitTestContext
    {
        private Mock<IItemService> _mockItemService;

        private Mock<IReplenishmentService> _mockReplenishmentService;

        private Mock<IDateTimeProvider> _dateTimeProviderMock;

        private Mock<NavigationManager> _mockNavigationManager;

        private Mock<IItemBuilder> _mockItemBuilder;

        private Mock<IReplenishmentBuilder> _mockReplenishmentBuilder;


        [SetUp]
        public void Init()
        {
            _mockItemService = new Mock<IItemService>();
            _mockReplenishmentService = new Mock<IReplenishmentService>();
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _mockNavigationManager = new Mock<NavigationManager>();
            _mockItemBuilder = new Mock<IItemBuilder>();
            _mockReplenishmentBuilder = new Mock<IReplenishmentBuilder>();

            Services.AddSingleton(_mockItemService.Object);
            Services.AddSingleton(_mockReplenishmentService.Object);
            Services.AddSingleton(_dateTimeProviderMock.Object);
            Services.AddSingleton(_mockNavigationManager.Object);
            Services.AddSingleton(_mockItemBuilder.Object);
            Services.AddSingleton(_mockReplenishmentBuilder.Object);
        }


        [Test]
        public void Should_DisplayDropdownWithDaysOfTheWeek()
        {
            var mockDate = new DateTime(2020, 10, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var itemDtoList = ItemsTestHelper.BuildNonUrgentItemDtoCollection();

            _mockItemService.Setup(c => c.GetShoppingListItems(secondReplenishmentDate))
                .ReturnsAsync(itemDtoList);


            var purchaseItemsCollection = ItemsTestHelper.BuildNonUrgentPurchaseItemVMCollection();

            _mockItemBuilder.Setup(c => c.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, itemDtoList))
             .Returns(purchaseItemsCollection);

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var renderedMarkup = cut.Find(".normalShoppingDaySelect");
            
            renderedMarkup.MarkupMatches(
                "<select class=\"appInputControl normalShoppingDaySelect\"><!--!-->" + Environment.NewLine +
"                <option value=\"Sunday\">Sunday</option><!--!-->" + Environment.NewLine +
"                <option value=\"Monday\">Monday</option><!--!-->" + Environment.NewLine +
"                <option value=\"Tuesday\">Tuesday</option><!--!-->" + Environment.NewLine +
"                <option value=\"Wednesday\">Wednesday</option><!--!-->" + Environment.NewLine +
"                <option value=\"Thursday\">Thursday</option><!--!-->" + Environment.NewLine +
"                <option value=\"Friday\">Friday</option><!--!-->" + Environment.NewLine +
"                <option value=\"Saturday\">Saturday</option><!--!-->" + Environment.NewLine +
"        </select>");
        }
    }
}
