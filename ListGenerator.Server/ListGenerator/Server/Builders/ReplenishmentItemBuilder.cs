using AutoMapper;
using ListGenerator.Data.Entities;
using ListGenerator.Server.Interfaces;
using ListGenerator.Shared.Dtos;
using ListGenerator.Shared.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Server.Builders
{
    public class ReplenishmentItemBuilder : IReplenishmentItemBuilder
    {
        private IDateTimeProvider _dateTimeProvider;
        private IMapper _mapper;

        public ReplenishmentItemBuilder(IDateTimeProvider dateTimeProvider, IMapper mapper)
        {
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
        }

        public IEnumerable<ReplenishmentItemDto> BuildReplenishmentDtos(DateTime firstReplenishmentDate, DateTime secondReplenishmentDate, IEnumerable<Item> items)
        {
            var dateTimeNowDate = _dateTimeProvider.GetDateTimeNowDate();

            var replenishmentDtos = new List<ReplenishmentItemDto>();

            foreach (var item in items)
            {
                var dto = _mapper.Map<Item, ReplenishmentItemDto>(item);

                dto.ReplenishmentDate = dateTimeNowDate;
                dto.Quantity = RecommendedPurchaseQuantity(item.ReplenishmentPeriod, item.NextReplenishmentDate, secondReplenishmentDate);
                dto.ItemNeedsReplenishmentUrgently = item.NextReplenishmentDate < firstReplenishmentDate;

                replenishmentDtos.Add(dto);
            }

            return replenishmentDtos;
        }

        private double RecommendedPurchaseQuantity(double itemReplenishmentPeriod, DateTime nextReplenishmentDate, DateTime secondReplenishmentDate)
        {
            var daysToBeCoveredWithSupplies = (secondReplenishmentDate - nextReplenishmentDate).Days;

            var neededQuantity = Math.Ceiling(daysToBeCoveredWithSupplies / itemReplenishmentPeriod);

            return neededQuantity;
        }
    }
}
