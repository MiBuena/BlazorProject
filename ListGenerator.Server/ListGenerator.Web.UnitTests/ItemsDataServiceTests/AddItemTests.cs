using FluentAssertions;
using ListGenerator.Data.Entities;
using ListGenerator.Shared.Dtos;
using ListGenerator.Web.UnitTests.Helpers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemsDataServiceTests
{
    [TestFixture]
    public class AddItemTests : BaseItemsDataServiceTests
    {
        [SetUp]
        protected override void Init()
        {
            base.Init();
        }

        [Test]
        public void Should_AddItem_When_InputParametersAreValid()
        {
            //Arrange
            var itemDto = BuildFirstItemDtoWithoutId();
            var item = BuildFirstItemWithoutId();

            MapperMock.Setup(c => c.Map<ItemDto, Item>(itemDto))
                .Returns(item);

            var saveObject = new Item();
            ItemsRepositoryMock.Setup(c => c.Add(It.IsAny<Item>()))
                    .Callback<Item>((obj) => saveObject = obj);


            //Act
            var result = ItemsDataService.AddItem("ab70793b-cec8-4eba-99f3-cbad0b1649d0", itemDto);


            //Assert
            AssertHelper.AssertAll(
                () => saveObject.Id.Should().Be(0),
                () => saveObject.Name.Should().Be("Bread"),
                () => saveObject.NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 06)),
                () => saveObject.ReplenishmentPeriod.Should().Be(1)
            );
        }
    }
}
