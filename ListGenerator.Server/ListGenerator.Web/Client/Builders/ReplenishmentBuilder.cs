using AutoMapper;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Builders
{
    public class ReplenishmentBuilder : IReplenishmentBuilder
    {
        private readonly IMapper _mapper;

        public ReplenishmentBuilder(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ReplenishmentDto BuildReplenishmentDto(DateTime firstReplenishmentDate, DateTime secondReplenishmentDate, PurchaseItemViewModel viewModel)
        {
            var dto = _mapper.Map<PurchaseItemViewModel, PurchaseItemDto>(viewModel);

            var replenishmentModel = new ReplenishmentDto()
            {
                FirstReplenishmentDate = firstReplenishmentDate,
                SecondReplenishmentDate = secondReplenishmentDate
            };

            replenishmentModel.Purchaseitems.Add(dto);
            return replenishmentModel;
        }
    }
}
