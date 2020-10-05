using ListGenerator.Client.Builders;
using ListGenerator.Client.Pages;
using ListGenerator.Client.Services;
using ListGenerator.Shared.Interfaces;
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
    }
}
