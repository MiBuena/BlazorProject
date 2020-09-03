using AutoMapper;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Client.ViewModels;
using ListGenerator.Web.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Web.Shared.Interfaces;
using ListGenerator.Web.Client.Builders;

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

        [Inject]
        public IItemBuilder ItemBuilder { get; set; }

        [Inject]
        public IReplenishmentBuilder ReplenishmentBuilder { get; set; }

        public List<PurchaseItemViewModel> ReplenishmentItems { get; set; }

        public DateTime FirstReplenishmentDate { get; set; }

        public DateTime SecondReplenishmentDate { get; set; }

        public DayOfWeek UsualShoppingDay { get; set; }

        public DateTime DateTimeNow { get; set; }


        protected async Task ChangeFirstReplenishmentDateValue(ChangeEventArgs e)
        {
            this.FirstReplenishmentDate = DateTime.Parse(e.Value.ToString());
            await InitializeReplenishmentItemsCollection();
        }

        protected async Task ChangeSecondReplenishmentDateValue(ChangeEventArgs e)
        {
            this.SecondReplenishmentDate = DateTime.Parse(e.Value.ToString());
            await InitializeReplenishmentItemsCollection();
        }

        protected override async Task OnInitializedAsync()
        {
            this.DateTimeNow = DateTimeProvider.GetDateTimeNowDate();
            this.UsualShoppingDay = DayOfWeek.Sunday;

            await GenerateListFromDayOfWeek();
        }

        private async Task InitializeReplenishmentItemsCollection()
        {
            var dtos = await ItemService.GetShoppingListItems(this.SecondReplenishmentDate);
            this.ReplenishmentItems = ItemBuilder.BuildPurchaseItemViewModels(this.FirstReplenishmentDate, this.SecondReplenishmentDate, dtos);
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
            var replenishmentModel = ReplenishmentBuilder.BuildReplenishmentDto(this.FirstReplenishmentDate, this.SecondReplenishmentDate, viewModel);
            
            await this.ReplenishmentService.ReplenishItems(replenishmentModel);

            var dtos = await ItemService.GetShoppingListItems(this.SecondReplenishmentDate);
            this.ReplenishmentItems = ItemBuilder.BuildPurchaseItemViewModels(this.FirstReplenishmentDate, this.SecondReplenishmentDate, dtos);
        }

        protected async Task ReplenishAllItems()
        {
            var replenishmentModel = ReplenishmentBuilder.BuildReplenishmentDto(this.FirstReplenishmentDate, this.SecondReplenishmentDate, this.ReplenishmentItems);

            await this.ReplenishmentService.ReplenishItems(replenishmentModel);
            NavigateToAllItems();
        }

        protected void NavigateToAllItems()
        {
            NavigationManager.NavigateTo("/allitems");
        }
    }
}
