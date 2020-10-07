using AngleSharp.Dom;
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
using System.Linq;
using System.Text;

namespace ListGenerator.Web.UnitTests.ShoppingListTests
{
    [TestFixture]
    public class OnInitializedAsyncTests : BUnitTestContext
    {
        private Mock<IItemService> _mockItemService;

        private Mock<IReplenishmentService> _mockReplenishmentService;

        private Mock<IDateTimeProvider> _mockDateTimeProvider;

        private Mock<NavigationManager> _mockNavigationManager;

        private Mock<IItemBuilder> _mockItemBuilder;

        private Mock<IReplenishmentBuilder> _mockReplenishmentBuilder;


        [SetUp]
        public void Init()
        {
            _mockItemService = new Mock<IItemService>();
            _mockReplenishmentService = new Mock<IReplenishmentService>();
            _mockDateTimeProvider = new Mock<IDateTimeProvider>();
            _mockNavigationManager = new Mock<NavigationManager>();
            _mockItemBuilder = new Mock<IItemBuilder>();
            _mockReplenishmentBuilder = new Mock<IReplenishmentBuilder>();

            Services.AddSingleton(_mockItemService.Object);
            Services.AddSingleton(_mockReplenishmentService.Object);
            Services.AddSingleton(_mockDateTimeProvider.Object);
            Services.AddSingleton(_mockNavigationManager.Object);
            Services.AddSingleton(_mockItemBuilder.Object);
            Services.AddSingleton(_mockReplenishmentBuilder.Object);
        }

        [Test]
        public void Should_DisplayNextShoppingDate_When_ShoppingListInitialized()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var firstShoppingDateValue = cut.Find(".first-shopping-date input").GetAttribute("value");

            Assert.AreEqual("2020-10-04", firstShoppingDateValue);
        }

        [Test]
        public void Should_DisplayNextShoppingDateInputWithMaxValueAtTheSecondShoppingDate_When_ShoppingListInitialized()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var firstShoppingDateMaxValue = cut.Find(".first-shopping-date input").GetAttribute("max");

            Assert.AreEqual("2020-10-11", firstShoppingDateMaxValue);
        }

        [Test]
        public void Should_DisplayNextShoppingDateAsInputWithTypeDate_When_ShoppingListInitialized()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var firstShoppingDateInputType = cut.Find(".first-shopping-date input").GetAttribute("type");

            Assert.AreEqual("date", firstShoppingDateInputType);
        }

        [Test]
        public void Should_DisplaySecondShoppingDate_When_ShoppingListInitialized()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var secondShoppingDateValue = cut.Find(".second-shopping-date input").GetAttribute("value");

            Assert.AreEqual("2020-10-11", secondShoppingDateValue);
        }

        [Test]
        public void Should_DisplaySecondShoppingDateInputWithMinValueAtTheFirstShoppingDate_When_ShoppingListInitialized()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var secondShoppingDateMin = cut.Find(".second-shopping-date input").GetAttribute("min");

            Assert.AreEqual("2020-10-04", secondShoppingDateMin);
        }

        [Test]
        public void Should_DisplaySecondShoppingDateAsInputWithTypeDate_When_ShoppingListInitialized()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var secondShoppingDateType = cut.Find(".second-shopping-date input").GetAttribute("type");

            Assert.AreEqual("date", secondShoppingDateType);
        }

        [Test]
        public void Should_DisplayShoppingListDate_When_ShoppingListInitialized()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var shoppingListDate = cut.Find(".shopping-list-date").TextContent;

            shoppingListDate.MarkupMatches("4.10.2020");
        }

        [Test]
        public void Should_DisplayFiveShoppingItemsInTheShoppingList_When_FiveItemsNeedReplenishment()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var shoppingListItemsCount = cut.FindAll(".items-shopping-list-table tbody tr").Count;

            Assert.AreEqual(5, shoppingListItemsCount);
        }

        [Test]
        public void Should_DisplayFirstShoppingItemDataCorrectly()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var firstItemName = cut.FindAll(".replenishment-item-name").First().TextContent;
            var firstItemNextReplenishmentDate = cut.FindAll(".replenishment-item-next-replenishment-date").First().TextContent;
            var firstItemQuantityToBuy = cut.FindAll(".replenishment-item-quantity-to-buy option").First(x=>x.HasAttribute("selected")).TextContent;
            var firstItemShoppingDate = cut.FindAll(".replenishment-item-shopping-date input").First().GetAttribute("value");

            AssertAll(
                () => firstItemName.MarkupMatches("Bread"),
                () => firstItemNextReplenishmentDate.MarkupMatches("6.10.2020"),
                () => firstItemQuantityToBuy.MarkupMatches("5"),
                () => firstItemShoppingDate.MarkupMatches("2020-10-01")
            );
        }


        [Test]
        public void Should_DisplayAllShoppingItemsShoppingDatesWithCorrectAttributes()
        {
            //Arrange
            InitializeShoppingList();

            //Act
            var cut = RenderComponent<ShoppingList>();

            // Assert
            var allShoppingDateInputs = cut.FindAll(".replenishment-item-shopping-date input");

            foreach (var input in allShoppingDateInputs)
            {
                var max = input.GetAttribute("max");
                max.MarkupMatches("2020-10-01");

                var type = input.GetAttribute("type");
                type.MarkupMatches("date");
            }
        }

            //[Test]
            //public void Should_DisplayShoppingItemNextReplenishmentDate_When_ThereIsOneNonUrgentItemThatNeedsReplenishment()
            //{
            //    //Arrange
            //    InitializeNonUrgentShoppingList();

            //    //Act
            //    var cut = RenderComponent<ShoppingList>();

            //    // Assert
            //    var shoppingItemName = cut.Find(".items-shopping-list-table tbody tr .replenishment-item-next-replenishment-date").TextContent;

            //    shoppingItemName.MarkupMatches("6.10.2020");
            //}

            private void InitializeNonUrgentShoppingList()
        {
            var mockDate = new DateTime(2020, 10, 01);
            _mockDateTimeProvider.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var itemDtoList = ItemsTestHelper.BuildNonUrgentItemDtoCollection();

            _mockItemService.Setup(c => c.GetShoppingListItems(secondReplenishmentDate))
                .ReturnsAsync(itemDtoList);


            var purchaseItemsCollection = ItemsTestHelper.BuildNonUrgentPurchaseItemVMCollection();

            _mockItemBuilder.Setup(c => c.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, itemDtoList))
             .Returns(purchaseItemsCollection);
        }

        private void InitializeShoppingList()
        {
            var mockDate = new DateTime(2020, 10, 01);
            _mockDateTimeProvider.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var itemDtoList = ItemsTestHelper.BuildItemsDtosCollection();

            _mockItemService.Setup(c => c.GetShoppingListItems(secondReplenishmentDate))
                .ReturnsAsync(itemDtoList);


            var purchaseItemsCollection = ItemsTestHelper.BuildPurchaseItemsViewModelsCollection();

            _mockItemBuilder.Setup(c => c.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, itemDtoList))
             .Returns(purchaseItemsCollection);
        }

        private void InitializeUrgentShoppingList()
        {
            var mockDate = new DateTime(2020, 10, 01);
            _mockDateTimeProvider.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            var firstReplenishmentDate = new DateTime(2020, 10, 04);
            var secondReplenishmentDate = new DateTime(2020, 10, 11);

            var itemDtoList = ItemsTestHelper.BuildUrgentItemDtoCollection();

            _mockItemService.Setup(c => c.GetShoppingListItems(secondReplenishmentDate))
                .ReturnsAsync(itemDtoList);


            var purchaseItemsCollection = ItemsTestHelper.BuildUrgentPurchaseItemVMCollection();

            _mockItemBuilder.Setup(c => c.BuildPurchaseItemViewModels(firstReplenishmentDate, secondReplenishmentDate, itemDtoList))
             .Returns(purchaseItemsCollection);
        }
    }
}
