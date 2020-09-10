﻿using AutoMapper;
using ListGenerator.Shared.Interfaces;
using Moq;
using NUnit.Framework;
using ListGenerator.Client.Builders;

namespace ListGenerator.Web.UnitTests.ItemBuilderTests
{
    [TestFixture]
    public class BuildPurchaseItemViewModelsTests : AssertAllTest
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
    }
}