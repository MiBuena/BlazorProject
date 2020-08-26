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
            _dateTimeProvider = dateTimeProvider;
            _replenishmentRepository = replenishmentRepository;
            _purchaseRepository = purchaseRepository;
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

                item.NextReplenishmentDate = CalculateNextPurchaseDateTime(purchaseItem, item);
            }

            _itemsRepository.SaveChanges();
        }

        private DateTime CalculateNextPurchaseDateTime(PurchaseItemDto purchaseItem, Item item)
        {
            var coveredWeeks = double.Parse(purchaseItem.Quantity.ToString()) * item.ReplenishmentPeriod;

            var days = coveredWeeks * 7;

            var newReplenishmentDate = _dateTimeProvider.GetDateTimeNow().AddDays(days);

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
