using FluentAssertions;
using ListGenerator.Data.Entities;
using ListGenerator.Shared.Dtos;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemsDataServiceTests
{
    [TestFixture]
    public class UpdateItemTests : BaseItemsDataServiceTests
    {
        [SetUp]
        protected override void Init()
        {
            base.Init();
        }

        [Test]
        public void Should_UpdateItem_When_InputParametersAreValid()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var saveObject = new Item();
            ItemsRepositoryMock.Setup(c => c.Update(It.IsAny<Item>()))
                    .Callback<Item>((obj) => saveObject = obj);

            ItemsRepositoryMock.Setup(c => c.SaveChanges());

            var updatedItemDto = new ItemDto()
            {
                Id = 1,
                Name = "Bread updated",
                NextReplenishmentDate = new DateTime(2020, 10, 10),
                ReplenishmentPeriod = 4
            };


            //Act
            var result = ItemsDataService.UpdateItem("ab70793b-cec8-4eba-99f3-cbad0b1649d0", updatedItemDto);


            //Assert
            AssertHelper.AssertAll(
                () => saveObject.Id.Should().Be(1),
                () => saveObject.Name.Should().Be("Bread updated"),
                () => saveObject.NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 10)),
                () => saveObject.ReplenishmentPeriod.Should().Be(4)
                );
        }

        [Test]
        public void Should_ReturnSuccessResponse_When_InputParametersAreValid()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var saveObject = new Item();
            ItemsRepositoryMock.Setup(c => c.Update(It.IsAny<Item>()))
                    .Callback<Item>((obj) => saveObject = obj);

            ItemsRepositoryMock.Setup(c => c.SaveChanges());

            var updatedItemDto = new ItemDto()
            {
                Id = 1,
                Name = "Bread updated",
                NextReplenishmentDate = new DateTime(2020, 10, 10),
                ReplenishmentPeriod = 4
            };


            //Act
            var result = ItemsDataService.UpdateItem("ab70793b-cec8-4eba-99f3-cbad0b1649d0", updatedItemDto);


            //Assert
            AssertHelper.AssertAll(
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull()
                );
        }

        [Test]
        public void Should_CallRepositorySaveChangesAfterUpdateMethod_When_InputParametersAreValid()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var saveObject = new Item();
            ItemsRepositoryMock.Setup(c => c.Update(It.IsAny<Item>()))
                    .Callback<Item>((obj) => saveObject = obj);

            ItemsRepositoryMock.Setup(c => c.SaveChanges());

            var updatedItemDto = new ItemDto()
            {
                Id = 1,
                Name = "Bread updated",
                NextReplenishmentDate = new DateTime(2020, 10, 10),
                ReplenishmentPeriod = 4
            };


            var sequence = new MockSequence();

            ItemsRepositoryMock
                .InSequence(sequence)
                .Setup(x => x.Update(It.Is<Item>(x => x.Id == updatedItemDto.Id
            && x.Name == updatedItemDto.Name
            && x.NextReplenishmentDate == updatedItemDto.NextReplenishmentDate
            && x.ReplenishmentPeriod == updatedItemDto.ReplenishmentPeriod)));

            ItemsRepositoryMock.InSequence(sequence).Setup(x => x.SaveChanges());

            //Act
            var result = ItemsDataService.UpdateItem("ab70793b-cec8-4eba-99f3-cbad0b1649d0", updatedItemDto);

            //Assert
            AssertHelper.AssertAll(
                () => result.IsSuccess.Should().BeTrue(),
                () => result.ErrorMessage.Should().BeNull()
                );
        }
    }
}
