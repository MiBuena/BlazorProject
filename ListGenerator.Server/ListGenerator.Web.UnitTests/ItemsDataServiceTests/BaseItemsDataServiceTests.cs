using AutoMapper;
using ListGenerator.Data.Entities;
using ListGenerator.Data.Interfaces;
using ListGenerator.Server.Interfaces;
using ListGenerator.Server.Services;
using ListGenerator.Shared.Dtos;
using ListGenerator.Web.UnitTests.Helpers;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        protected void InitializeMocksWithEmptyCollection()
        {
            var allItems = new List<Item>().AsQueryable();
            ItemsRepositoryMock.Setup(x => x.All()).Returns(allItems);

            var filteredItems = new List<Item>();
            var filteredItemNameDtos = new List<ItemNameDto>();

            MapperMock
                .Setup(c => c.ProjectTo(
                    It.Is<IQueryable<Item>>(x => ItemsTestHelper.HaveTheSameElements(filteredItems, x)),
                    It.IsAny<object>(),
                    It.IsAny<Expression<Func<ItemNameDto, object>>[]>()))
             .Returns(filteredItemNameDtos.AsQueryable());
        }
    }
}
