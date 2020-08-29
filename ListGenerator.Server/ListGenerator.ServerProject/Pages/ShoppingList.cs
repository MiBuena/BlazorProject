using AutoMapper;
using ListGenerator.Common.Models;
using ListGenerator.Models;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.ViewModels;
using ListGenerator.ServerProject.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Pages
{
    public partial class ShoppingList
    {
        [Inject]
        public IItemService ItemService { get; set; }

        [Inject]
        public IReplenishmentService ReplenishmentService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<PurchaseItemViewModel> ReplenishmentItems { get; set; }

        public DateTime FirstReplenishmentDate { get; set; }

        public DateTime SecondReplenishmentDate { get; set; }

        public DayOfWeek UsualShoppingDay { get; set; }


        protected override async Task OnInitializedAsync()
        {
            this.UsualShoppingDay = DayOfWeek.Sunday;

            await InitializeProperties();
        }

        private async Task InitializeProperties()
        {
            this.FirstReplenishmentDate = GetNextShoppingDay(UsualShoppingDay);
            this.SecondReplenishmentDate = this.FirstReplenishmentDate.AddDays(7);

            var dtos = await ItemService.GetShoppingListItems(this.SecondReplenishmentDate);
            var replenishmentItems = dtos.Select(x => Mapper.Map<ItemDto, PurchaseItemViewModel>(x)).ToList();

            foreach (var item in replenishmentItems)
            {
                item.ReplenishmentSignalClass =
                    item.NextReplenishmentDate < this.FirstReplenishmentDate
                    ? "itemNeedsReplenishment"
                    : "";
            }
            this.ReplenishmentItems = replenishmentItems;
        }

        protected async Task RegeneateList(ChangeEventArgs e)
        {
            UsualShoppingDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), e.Value.ToString());

            await InitializeProperties();
        }

        protected async Task RegeneateListDate(ChangeEventArgs e)
        {
            UsualShoppingDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), e.Value.ToString()); 

            await InitializeProperties();
        }

        private DateTime GetNextShoppingDay(DayOfWeek usualShoppingDay)
        {
            DateTime today = DateTime.Today;
            int daysUntilUsualShoppingDay = ((int)usualShoppingDay - (int)today.DayOfWeek + 7) % 7;
            DateTime nextShoppingDay = today.AddDays(daysUntilUsualShoppingDay);

            return nextShoppingDay;
        }

        protected async Task ReplenishItem(int itemId)
        {
            var viewModel = this.ReplenishmentItems.FirstOrDefault(x => x.ItemId == itemId);

            var dto = Mapper.Map<PurchaseItemViewModel, PurchaseItemDto>(viewModel);

            var replenishmentModel = new ReplenishmentDto();
            replenishmentModel.FirstReplenishmentDate = FirstReplenishmentDate;
            replenishmentModel.SecondReplenishmentDate = SecondReplenishmentDate;
            replenishmentModel.Purchaseitems.Add(dto);

            await this.ReplenishmentService.ReplenishItems(replenishmentModel);

            var dtos = await ItemService.GetShoppingListItems(this.SecondReplenishmentDate);
            this.ReplenishmentItems = dtos.Select(x => Mapper.Map<ItemDto, PurchaseItemViewModel>(x)).ToList();

            StateHasChanged();
        }

        protected async Task ReplenishAllItems()
        {
            var replenishmentModel = new ReplenishmentDto()
            {
                FirstReplenishmentDate = FirstReplenishmentDate,
                Purchaseitems = this.ReplenishmentItems.Select(x => Mapper.Map<PurchaseItemViewModel, PurchaseItemDto>(x)).ToList()
            };

            await this.ReplenishmentService.ReplenishItems(replenishmentModel);
            NavigateToAllItems();
        }

        protected void NavigateToAllItems()
        {
            NavigationManager.NavigateTo("/allitems");
        }
    }
}
