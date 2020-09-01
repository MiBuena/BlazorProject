﻿using AutoMapper;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Server.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Data.Interfaces;
using ListGenerator.Data.Entities;
using ListGenerator.Web.Shared.Interfaces;

namespace ListGenerator.Web.Server.Services
{
    public class ReplenishmentDataService : IReplenishmentDataService
    {
        private readonly IRepository<Item> _itemsRepository;
        private readonly IRepository<Purchase> _purchaseRepository;
        private readonly IDateTimeProvider _dateTimeProvider;
        private readonly IMapper _mapper;

        public ReplenishmentDataService(IRepository<Item> items, IDateTimeProvider dateTimeProvider, IMapper mapper, IRepository<Purchase> purchaseRepository) 
        {
            _itemsRepository = items;
            _purchaseRepository = purchaseRepository;
            _dateTimeProvider = dateTimeProvider;
            _mapper = mapper;
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

        public DateTime RegenerateNextReplenishmentDateTime(int itemId, double newItemReplenishmentPeriod, DateTime previousReplenishmentDate)
        {
            var itemLastPurchase = GetItemLastPurchase(itemId);

            if(itemLastPurchase == null)
            {
                return previousReplenishmentDate;
            }

            var newReplenishmentDate = CalculateNextReplenishmentDateTime(newItemReplenishmentPeriod, itemLastPurchase.Quantity, previousReplenishmentDate, itemLastPurchase.ReplenishmentDate);

            return newReplenishmentDate;
        }

        private Purchase GetItemLastPurchase(int itemId)
        {
            var lastPurchase = _purchaseRepository.All()
                .Where(x => x.ItemId == itemId)
                .OrderByDescending(y => y.ReplenishmentDate)
                .FirstOrDefault();

            return lastPurchase;
        }

        private DateTime CalculateNextReplenishmentDateTime(double itemReplenishmentPeriod, int purchasedQuantity, DateTime previousReplenishmentDate, DateTime replenishmentDate)
        {
            var coveredDays = double.Parse(purchasedQuantity.ToString()) * itemReplenishmentPeriod;

            var baseDateToCalculateNextReplDate = replenishmentDate;

            if(previousReplenishmentDate > replenishmentDate)
            {
                baseDateToCalculateNextReplDate = previousReplenishmentDate;
            }

            var newReplenishmentDate = baseDateToCalculateNextReplDate.AddDays(coveredDays);

            return newReplenishmentDate;
        }
    }
}
