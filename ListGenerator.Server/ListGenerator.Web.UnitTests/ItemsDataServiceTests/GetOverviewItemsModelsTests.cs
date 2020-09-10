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
    [TestFixture]
    public class GetOverviewItemsModelsTests
    {
        private Mock<IRepository<Item>> _itemsRepository;
        private Mock<IMapper> _mapperMock;
        private IItemsDataService _itemsDataService;


        [SetUp]
        public void Init()
        {
            _itemsRepository = new Mock<IRepository<Item>>();
            _mapperMock = new Mock<IMapper>();
            _itemsDataService = new ItemsDataService(_itemsRepository.Object, _mapperMock.Object);
        }


    }
}
