using AutoMapper;
using ListGenerator.Api.Interfaces;
using ListGenerator.Common.Interfaces;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Api.Services
{
    public class ReplenishmentDataService : IReplenishmentDataService
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IRepository<Replenishment> _replenishmentRepository;
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;

        public ReplenishmentDataService(IRepository<Item> items, IRepository<Replenishment> replenishmentRepository, IDateTimeProvider dateTimeProvider, IMapper mapper, IRepository<Purchase> purchaseRepository) 
        {
            _itemsRepository = items;
            _purchaseRepository = purchaseRepository;
            _dateTimeProvider = dateTimeProvider;
            _replenishmentRepository = replenishmentRepository;
            _mapper = mapper;
        }

        public void ReplenishItems(ReplenishmentDto replenishmentData)
        {
            var allItems = _itemsRepository.All().ToList();

            var replenishment = AddReplenishmentDataToDb();

            foreach (var purchaseItem in replenishmentData.Purchaseitems)
            {
                var purchase = _mapper.Map<PurchaseItemDto, Purchase>(purchaseItem);

                replenishment.Purchase.Add(purchase);


                var item = allItems.FirstOrDefault(x => x.Id == purchaseItem.ItemId);

                item.NextReplenishmentDate = CalculateNextPurchaseDateTime(item.ReplenishmentPeriod, purchaseItem.Quantity, item.NextReplenishmentDate);
            }

            _itemsRepository.SaveChanges();
        }

        public DateTime RegenerateNextPurchaseDateTime(int itemId, double newItemReplenishmentPeriod, DateTime previousReplenishmentDate)
        {
            var lastPurchaseQuantity = GetItemLastPurchasedQuantity(itemId);

            var newPurchaseDate = CalculateNextPurchaseDateTime(newItemReplenishmentPeriod, lastPurchaseQuantity, previousReplenishmentDate);

            return newPurchaseDate;
        }

        private int GetItemLastPurchasedQuantity(int itemId)
        {
            var lastPurchaseQuantity = _purchaseRepository.All()
                .Where(x => x.ItemId == itemId)
                .OrderByDescending(y => y.Replenishment.Date)
                .Select(x => x.Quantity)
                .FirstOrDefault();

            return lastPurchaseQuantity;
        }

        private DateTime CalculateNextPurchaseDateTime(double itemReplenishmentPeriod, int purchasedQuantity, DateTime previousReplenishmentDate)
        {
            var coveredDays = double.Parse(purchasedQuantity.ToString()) * itemReplenishmentPeriod;

            var dateTimeNow = _dateTimeProvider.GetDateTimeNow();
            var baseDateToCalculateNextReplDate = dateTimeNow;

            if(previousReplenishmentDate > dateTimeNow)
            {
                baseDateToCalculateNextReplDate = previousReplenishmentDate;
            }

            var newReplenishmentDate = baseDateToCalculateNextReplDate.AddDays(coveredDays);

            return newReplenishmentDate;
        }

        public Replenishment AddReplenishmentDataToDb()
        {
            var replenishment = new Replenishment()
            {
                Date = _dateTimeProvider.GetDateTimeNow()
            };

            _replenishmentRepository.Add(replenishment);

            return replenishment;
        }
    }
}
