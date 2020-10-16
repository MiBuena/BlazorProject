using AutoMapper;
using ListGenerator.Server.Builders;
using ListGenerator.Server.Interfaces;
using ListGenerator.Shared.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.ReplenishmentItemBuilderTests
{
    [TestFixture]
    public class BuildReplenishmentItemsDtosTests
    {
        [SetUp]
        public virtual void Init()
        {
            DateTimeProviderMock = new Mock<IDateTimeProvider>(MockBehavior.Strict);
            MapperMock = new Mock<IMapper>(MockBehavior.Strict);
            ReplenishmentItemBuilder = new ReplenishmentItemBuilder(DateTimeProviderMock.Object, MapperMock.Object);
        }

        protected Mock<IDateTimeProvider> DateTimeProviderMock { get; private set; }
        protected Mock<IMapper> MapperMock { get; private set; }
        protected IReplenishmentItemBuilder ReplenishmentItemBuilder { get; private set; }




    }
}
