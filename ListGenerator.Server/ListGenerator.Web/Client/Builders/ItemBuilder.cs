using AutoMapper;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.Interfaces;
using ListGenerator.Web.Client.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Builders
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
                ReplenishmentPeriod = "1"
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
            var timeToBeCoveredWithSupplies = (secondReplenishmentDate - nextReplenishmentDate).Days;

            var neededQuantity = Math.Ceiling(timeToBeCoveredWithSupplies / itemReplenishmentPeriod);

            return neededQuantity;
        }
    }
}
