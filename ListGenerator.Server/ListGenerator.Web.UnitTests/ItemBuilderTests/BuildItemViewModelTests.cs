using AutoMapper;
using FluentAssertions;
using ListGenerator.Web.Client.Builders;
using ListGenerator.Web.Shared.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemBuilderTests
{
    [TestFixture]
    public class BuildItemViewModelTests : AssertAllTest
    {
        private Mock<IDateTimeProvider> _dateTimeProviderMock;
        private Mock<IMapper> _mapperMock;
        private IItemBuilder _itemBuilder;


        [SetUp]
        public void Init()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _mapperMock = new Mock<IMapper>();
            _itemBuilder = new ItemBuilder(_dateTimeProviderMock.Object, _mapperMock.Object);
        }

        [Test]
        public void Should_HaveNextReplenishmentDate_SetOnlyAsDate()
        {
            //Arrange
            var mockDate = new DateTime(2020, 09, 01);
            _dateTimeProviderMock.Setup(x => x.GetDateTimeNowDate()).Returns(mockDate);

            //Act
            var result = _itemBuilder.BuildItemViewModel();

            //Assert
            result.NextReplenishmentDate.Should().BeSameDateAs(mockDate);
        }
    }
}
