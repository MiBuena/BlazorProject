using AutoMapper;
using ListGenerator.Data.Entities;
using ListGenerator.Data.Interfaces;
using ListGenerator.Server.Interfaces;
using ListGenerator.Server.Services;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.UnitTests.ItemsDataServiceTests
{
    public class BaseItemsDataServiceTests
    {
        [SetUp]
        public virtual void Init()
        {
            ItemsRepositoryMock = new Mock<IRepository<Item>>();
            MapperMock = new Mock<IMapper>();
            ItemsDataService = new ItemsDataService(ItemsRepositoryMock.Object, MapperMock.Object);
        }

        protected Mock<IRepository<Item>> ItemsRepositoryMock { get; private set; }
        protected Mock<IMapper> MapperMock { get; private set; }
        protected IItemsDataService ItemsDataService { get; private set; }
    }
}
