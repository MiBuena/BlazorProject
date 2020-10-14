using FluentAssertions;
using ListGenerator.Data.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemsDataServiceTests
{
    [TestFixture]
    public class DeleteItemTests : BaseItemsDataServiceTests
    {
        [SetUp]
        protected override void Init()
        {
            base.Init();
        }

        [Test]
        public void Should_ReturnSuccessResponse_When_InputParametersAreValid()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            ItemsRepositoryMock.Setup(c => c.Delete(It.IsAny<Item>()));
            ItemsRepositoryMock.Setup(c => c.SaveChanges());


            //Act
            var response = ItemsDataService.DeleteItem(1, "ab70793b-cec8-4eba-99f3-cbad0b1649d0");


            //Assert
            AssertHelper.AssertAll(
                () => response.IsSuccess.Should().BeTrue(),
                () => response.ErrorMessage.Should().BeNull()
                );
        }


        [Test]
        public void Should_CallSaveMethodAfterDeletemethod_When_InputParametersAreValid()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var sequence = new MockSequence();
            ItemsRepositoryMock.InSequence(sequence).Setup(x => x.Delete(It.IsAny<Item>()));
            ItemsRepositoryMock.InSequence(sequence).Setup(x => x.SaveChanges());


            //Act
            var response = ItemsDataService.DeleteItem(1, "ab70793b-cec8-4eba-99f3-cbad0b1649d0");


            //Assert
            AssertHelper.AssertAll(
                () => response.IsSuccess.Should().BeTrue(),
                () => response.ErrorMessage.Should().BeNull()
                );
        }


        [Test]
        public void Should_CallDeletemethodWithCorrectItem_When_InputParametersAreValid()
        {
            //Arrange
            var allItems = BuildItemsCollection();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var deleteObject = new Item();
            ItemsRepositoryMock.Setup(c => c.Delete(It.IsAny<Item>()))
                        .Callback<Item>((obj) => deleteObject = obj);

            ItemsRepositoryMock.Setup(x => x.SaveChanges());


            //Act
            var response = ItemsDataService.DeleteItem(1, "ab70793b-cec8-4eba-99f3-cbad0b1649d0");


            //Assert
            AssertHelper.AssertAll(
                () => deleteObject.Id.Should().Be(1),
                () => deleteObject.Name.Should().Be("Bread"),
                () => deleteObject.NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 06)),
                () => deleteObject.ReplenishmentPeriod.Should().Be(1), 
                () => deleteObject.UserId.Should().Be("ab70793b-cec8-4eba-99f3-cbad0b1649d0")
                );
        }
    }
}
