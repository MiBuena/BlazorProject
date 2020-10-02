using AutoMapper;
using ListGenerator.Shared.Dtos;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

            foreach (var item in replenishmentItems)
            {
                item.ReplenishmentSignalClass =
                    item.NextReplenishmentDate < firstReplenishmentDate
                    ? "itemNeedsReplenishment"
                    : "";

                item.ReplenishmentDate = _dateTimeProvider.GetDateTimeNowDate();

                var itemDto = itemsDtos.FirstOrDefault(x => x.Id == item.ItemId);

                item.Quantity = RecommendedPurchaseQuantity(itemDto.ReplenishmentPeriod, itemDto.NextReplenishmentDate, secondReplenishmentDate).ToString();
            }

            return replenishmentItems;
        }

        private double RecommendedPurchaseQuantity(double itemReplenishmentPeriod, DateTime nextReplenishmentDate, DateTime secondReplenishmentDate)
        {
            var daysToBeCoveredWithSupplies = (secondReplenishmentDate - nextReplenishmentDate).Days;

            var neededQuantity = Math.Ceiling(daysToBeCoveredWithSupplies / itemReplenishmentPeriod);

            return neededQuantity;
        }
    }
}
