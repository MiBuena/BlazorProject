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
        private readonly IDateTimeProvider _dateTimeProvider;

        public ReplenishmentDataService(IRepository<Item> items, IMapper mapper, IRepository<Purchase> purchaseRepository, IDateTimeProvider dateTimeProvider)
        {
            _itemsRepository = items;
            _purchaseRepository = purchaseRepository;
            _mapper = mapper;
            _dateTimeProvider = dateTimeProvider;
        }

        public Response<IEnumerable<ReplenishmentItemDto>> GetShoppingList(string firstReplenishmentDateAsString, string secondReplenishmentDateAsString, string userId)
        {
            try
            {
                var firstReplenishmentDate = DateTimeHelper.ToDateFromTransferDateAsString(firstReplenishmentDateAsString);
                var secondReplenishmentDate = DateTimeHelper.ToDateFromTransferDateAsString(secondReplenishmentDateAsString);

                var itemsNeedingReplenishment = GetShoppingListItems(secondReplenishmentDate, userId);
                var replenishmentDtos = BuildReplenishmentDtos(firstReplenishmentDate, secondReplenishmentDate, itemsNeedingReplenishment);
                var response = ResponseBuilder.Success(replenishmentDtos);
                return response;
            }
            catch (Exception ex)
            {
                var response = ResponseBuilder.Failure<IEnumerable<ReplenishmentItemDto>>("An error occured while getting shopping items");
                return response;
            }
        }

        private IEnumerable<ReplenishmentItemDto> BuildReplenishmentDtos(DateTime firstReplenishmentDate, DateTime secondReplenishmentDate, IEnumerable<Item> items)
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

        private IEnumerable<Item> GetShoppingListItems(DateTime date, string userId)
        {
            var query = _itemsRepository.All()
                .Where(x => x.NextReplenishmentDate.Date < date
                && x.UserId == userId)
                .OrderBy(x => x.NextReplenishmentDate);

            var itemsNeedingReplenishment = query.ToList();

            return itemsNeedingReplenishment;
        }

        private double RecommendedPurchaseQuantity(double itemReplenishmentPeriod, DateTime nextReplenishmentDate, DateTime secondReplenishmentDate)
        {
            var daysToBeCoveredWithSupplies = (secondReplenishmentDate - nextReplenishmentDate).Days;

            var neededQuantity = Math.Ceiling(daysToBeCoveredWithSupplies / itemReplenishmentPeriod);

            return neededQuantity;
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
