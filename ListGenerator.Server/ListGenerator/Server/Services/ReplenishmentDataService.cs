using AutoMapper;
using ListGenerator.Shared.Dtos;
using ListGenerator.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Data.Interfaces;
using ListGenerator.Data.Entities;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Shared.Helpers;
using ListGenerator.Shared.Responses;
using ListGenerator.Server.Builders;

namespace ListGenerator.Server.Services
{
    public class ReplenishmentDataService : IReplenishmentDataService
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IMapper _mapper;

        public ReplenishmentDataService(IRepository<Item> items, IMapper mapper, IRepository<Purchase> purchaseRepository)
        {
            _itemsRepository = items;
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
        }

        public Response<IEnumerable<ItemDto>> GetShoppingList(string secondReplenishmentDate, string userId)
        {
            try
            {
                var date = DateTimeHelper.ToDateFromTransferDateAsString(secondReplenishmentDate);

                var query = _itemsRepository.All()
                    .Where(x => x.NextReplenishmentDate.Date < date
                    && x.UserId == userId)
                    .OrderBy(x => x.NextReplenishmentDate);

                var itemsNeedingReplenishment = _mapper.ProjectTo<ItemDto>(query).ToList();

                var response = ResponseBuilder.Success<IEnumerable<ItemDto>>(itemsNeedingReplenishment);
                return response;
            }
            catch (Exception ex)
            {
                var response = ResponseBuilder.Failure<IEnumerable<ItemDto>>("An error occured while getting shopping items");
                return response;
            }
        }

        public void ReplenishItems(ReplenishmentDto replenishmentData)
        {
            var allItems = _itemsRepository.All().ToList();

            foreach (var purchaseItem in replenishmentData.Purchaseitems)
            {
                var purchase = _mapper.Map<PurchaseItemDto, Purchase>(purchaseItem);

                _purchaseRepository.Add(purchase);


                var item = allItems.FirstOrDefault(x => x.Id == purchaseItem.ItemId);

                item.NextReplenishmentDate = CalculateNextReplenishmentDateTime(item.ReplenishmentPeriod, purchaseItem.Quantity, item.NextReplenishmentDate, purchaseItem.ReplenishmentDate);
            }

            _itemsRepository.SaveChanges();
        }

        private DateTime CalculateNextReplenishmentDateTime(double itemReplenishmentPeriod, int purchasedQuantity, DateTime previousReplenishmentDate, DateTime replenishmentDate)
        {
            var coveredDays = double.Parse(purchasedQuantity.ToString()) * itemReplenishmentPeriod;

            var baseDateToCalculateNextReplDate = replenishmentDate;

            if (previousReplenishmentDate > replenishmentDate)
            {
                baseDateToCalculateNextReplDate = previousReplenishmentDate;
            }

            var newReplenishmentDate = baseDateToCalculateNextReplDate.AddDays(coveredDays);

            return newReplenishmentDate;
        }
    }
}
