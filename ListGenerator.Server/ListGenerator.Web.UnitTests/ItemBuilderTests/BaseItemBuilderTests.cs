using AutoMapper;
using ListGenerator.Client.Builders;
using ListGenerator.Shared.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemBuilderTests
{
    public class BaseItemBuilderTests
    {
        protected Mock<IDateTimeProvider> _dateTimeProviderMock;
        protected Mock<IMapper> _mapperMock;
        protected IItemBuilder _itemBuilder;


        [SetUp]
        public virtual void Init()
        {
            _dateTimeProviderMock = new Mock<IDateTimeProvider>();
            _mapperMock = new Mock<IMapper>();
            _itemBuilder = new ItemBuilder(_dateTimeProviderMock.Object, _mapperMock.Object);
        }
    }
}
