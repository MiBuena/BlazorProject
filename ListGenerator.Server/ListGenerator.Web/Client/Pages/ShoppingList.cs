using AutoMapper;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.ViewModels;
using ListGenerator.Web.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Web.Shared.Interfaces;

namespace ListGenerator.Web.Client.Pages
{
    [Authorize]
    public partial class ShoppingList
    {
        [Inject]
        public IItemService ItemService { get; set; }

        [Inject]
        public IReplenishmentService ReplenishmentService { get; set; }

        [Inject]
        public IDateTimeProvider DateTimeProvider { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public List<PurchaseItemViewModel> ReplenishmentItems { get; set; }

        public DateTime FirstReplenishmentDate { get; set; }

        public DateTime SecondReplenishmentDate { get; set; }

        public DayOfWeek UsualShoppingDay { get; set; }

        public DateTime DateTimeNow { get; set; }


        protected void ChangeFirstReplenishmentDateValue(ChangeEventArgs e)
        {
            this.FirstReplenishmentDate = DateTime.Parse(e.Value.ToString());
        }

        protected async Task ChangeSecondReplenishmentDateValue(ChangeEventArgs e)
        {
            this.SecondReplenishmentDate = DateTime.Parse(e.Value.ToString());
            await InitializeReplenishmentItemsCollection();
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            this.DateTimeNow = DateTimeProvider.GetDateTimeNow();
            this.UsualShoppingDay = DayOfWeek.Sunday;

            await GenerateListFromDayOfWeek();
        }

        private async Task InitializeReplenishmentItemsCollection()
        {
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


        protected async Task RegenerateListFromDayOfWeek(ChangeEventArgs e)
        {
            this.UsualShoppingDay = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), e.Value.ToString());

            await GenerateListFromDayOfWeek();
        }

        protected async Task GenerateListFromDayOfWeek()
        {
            this.FirstReplenishmentDate = GetNextShoppingDay(UsualShoppingDay);
            this.SecondReplenishmentDate = this.FirstReplenishmentDate.AddDays(7);

            await InitializeReplenishmentItemsCollection();
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
