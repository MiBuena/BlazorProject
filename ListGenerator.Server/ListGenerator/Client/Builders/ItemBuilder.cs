using AutoMapper;
using ListGenerator.Shared.Dtos;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Shared.Constants;

namespace ListGenerator.Client.Builders
{
    public class ItemBuilder : IItemBuilder
    {
        private IDateTimeProvider _dateTimeProvider;
        private IMapper _mapper;

        public ItemBuilder(IDateTimeProvider dateTimeProvider, IMapper mapper)
        {
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
        }

        public ItemViewModel BuildItemViewModel()
        {
            var model = new ItemViewModel()
            {
                NextReplenishmentDate = _dateTimeProvider.GetDateTimeNowDate(),
                ReplenishmentPeriodString = "1"
            };

            return model;
        }

        public List<PurchaseItemViewModel> BuildPurchaseItemViewModels(DateTime firstReplenishmentDate, DateTime secondReplenishmentDate, IEnumerable<ItemDto> itemsDtos)
        {
            var replenishmentItems = itemsDtos.Select(x => _mapper.Map<ItemDto, PurchaseItemViewModel>(x)).ToList();

            var dateTimeNowDate = _dateTimeProvider.GetDateTimeNowDate();

            foreach (var item in replenishmentItems)
            {
                item.ReplenishmentSignalClass =
                    item.NextReplenishmentDate < firstReplenishmentDate
                    ? Constants.ReplenishmentSignalClass
                    : string.Empty;

                item.ReplenishmentDate = dateTimeNowDate;

                var itemDto = itemsDtos.FirstOrDefault(x => x.Id == item.ItemId);

                item.Quantity = RecommendedPurchaseQuantity(itemDto.ReplenishmentPeriod, itemDto.NextReplenishmentDate, secondReplenishmentDate).ToString();
            }

            return replenishmentItems;
        }

        public IEnumerable<ItemDto> BuildItemsDtosCollection()
        {
            var collection = new List<ItemDto>();

            var firstItem = new ItemDto()
            {
                Id = 1,
                Name = "Bread",
                NextReplenishmentDate = new DateTime(2020, 10, 06),
                ReplenishmentPeriod = 1
            };

            var secondItem = new ItemDto()
            {
                Id = 2,
                Name = "Cheese",
                NextReplenishmentDate = new DateTime(2020, 10, 08),
                ReplenishmentPeriod = 2
            };

            var thirdItem = new ItemDto()
            {
                Id = 3,
                Name = "Biscuits",
                NextReplenishmentDate = new DateTime(2020, 10, 07),
                ReplenishmentPeriod = 2
            };

            var fourthItem = new ItemDto()
            {
                Id = 4,
                Name = "Oats",
                NextReplenishmentDate = new DateTime(2020, 10, 1),
                ReplenishmentPeriod = 7
            };

            var fifthItem = new ItemDto()
            {
                Id = 5,
                Name = "Cake",
                NextReplenishmentDate = new DateTime(2020, 10, 2),
                ReplenishmentPeriod = 5
            };

            collection.Add(firstItem);
            collection.Add(secondItem);
            collection.Add(thirdItem);
            collection.Add(fourthItem);
            collection.Add(fifthItem);

            return collection;
        }

        private double RecommendedPurchaseQuantity(double itemReplenishmentPeriod, DateTime nextReplenishmentDate, DateTime secondReplenishmentDate)
        {
            var daysToBeCoveredWithSupplies = (secondReplenishmentDate - nextReplenishmentDate).Days;

            var neededQuantity = Math.Ceiling(daysToBeCoveredWithSupplies / itemReplenishmentPeriod);

            return neededQuantity;
        }
    }
}
