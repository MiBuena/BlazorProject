using AutoMapper;
using ListGenerator.Shared.Dtos;
using ListGenerator.Client.ViewModels;
using ListGenerator.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Shared.Interfaces;
using ListGenerator.Client.Builders;

namespace ListGenerator.Client.Pages
{
    [Authorize]
    public partial class ShoppingList
    {
        [Inject]
        private IItemService ItemService { get; set; }

        [Inject]
        private IReplenishmentService ReplenishmentService { get; set; }

        [Inject]
        private IDateTimeProvider DateTimeProvider { get; set; }

        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IItemBuilder ItemBuilder { get; set; }

        [Inject]
        private IReplenishmentBuilder ReplenishmentBuilder { get; set; }

        protected List<PurchaseItemViewModel> ReplenishmentItems { get; set; } = new List<PurchaseItemViewModel>();

        protected DateTime FirstReplenishmentDate { get; set; }

        protected DateTime SecondReplenishmentDate { get; set; }

        private DayOfWeek UsualShoppingDay { get; set; }

        protected DateTime DateTimeNow { get; set; }


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
            NavigationManager.NavigateTo("/allitemstable");
        }
    }
}
