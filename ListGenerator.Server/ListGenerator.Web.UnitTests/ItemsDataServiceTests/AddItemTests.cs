using FluentAssertions;
using ListGenerator.Data.Entities;
using ListGenerator.Data.Interfaces;
using ListGenerator.Server.Interfaces;
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

        [Test]
        public void Should_AddItemOnce_When_InputParametersAreValid()
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
            ItemsRepositoryMock.Verify(c => c.Add(It.IsAny<Item>()), Times.Once());
        }


        [Test]
        public void Should_FirstAddItemThenCallSaveChanges_When_InputParametersAreValid()
        {
            //Arrange
            var itemDto = BuildFirstItemDtoWithoutId();
            var item = BuildFirstItemWithoutId();

            MapperMock.Setup(c => c.Map<ItemDto, Item>(itemDto))
                .Returns(item);

            var sequence = new MockSequence();
            ItemsRepositoryMock.InSequence(sequence).Setup(x => x.Add(It.IsAny<Item>()));
            ItemsRepositoryMock.InSequence(sequence).Setup(x => x.SaveChanges());

            //Act
            var result = ItemsDataService.AddItem("ab70793b-cec8-4eba-99f3-cbad0b1649d0", itemDto);

            //Assert
            AssertHelper.AssertAll(
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull()
                );
        }
    }
}
