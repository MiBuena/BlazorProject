using AutoMapper;
using FluentAssertions;
using ListGenerator.Client.Builders;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Web.UnitTests.Helpers;
using Moq;
using NUnit.Framework;
using System;

namespace ListGenerator.Web.UnitTests.ItemBuilderTests
{
    [TestFixture]
    public class BuildItemViewModelTests : BaseItemBuilderTests
    {
        [SetUp]
        public override void Init()
        {
            base.Init();
        }

        [Test]
        public void Should_HaveNextReplenishmentDate_SetOnlyAsDate()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            //Act
            var result = _itemBuilder.BuildItemViewModel();

            //Assert
            result.NextReplenishmentDate.Should().BeSameDateAs(new DateTime(2020, 10, 01));
        }


        [Test]
        public void Should_HaveReplenishmentPeriod_SetToIntDefaultValueAsString()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            //Act
            var result = _itemBuilder.BuildItemViewModel();

            //Assert
            result.ReplenishmentPeriodString.Should().Be("1");
        }

        [Test]
        public void Should_HaveAllOtherPropertiesExceptForNextReplenishmentDateAndReplenishmentPeriod_SetToTheirDefaultValues()
        {
            //Arrange
            ItemsTestHelper.InitializeDateTimeProviderMock(_dateTimeProviderMock);

            //Act
            var result = _itemBuilder.BuildItemViewModel();

            //Assert
            AssertHelper.AssertAll(
                () => result.Id.Should().Be(0),
                () => result.Name.Should().BeNull()
                );
        }
    }
}
