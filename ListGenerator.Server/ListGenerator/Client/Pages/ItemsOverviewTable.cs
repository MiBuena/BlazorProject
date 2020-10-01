using AutoMapper;
using ListGenerator.Client.Components;
using ListGenerator.Client.Services;
using ListGenerator.Client.ViewModels;
using ListGenerator.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Pages
{
    [Authorize]
    public partial class ItemsOverviewTable
    {
        public string SearchWord { get; set; }

        public DateTime? SearchDate { get; set; }

        public int Count { get; set; }

        [Inject]
        public IItemService ItemsService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public IEnumerable<ItemNameDto> ItemsNames { get; set; }

        public IEnumerable<ItemNameDto> DisplayItemsNames { get; set; }

        public IEnumerable<ItemOverviewViewModel> DisplayItems { get; set; }

        protected AddItemDialog AddItemDialog { get; set; }

        protected EditItemDialog EditItemDialog { get; set; }

        protected DeleteItemDialog DeleteItemDialog { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected RadzenGrid<ItemOverviewViewModel> Table { get; set; }


        protected async Task LoadData(LoadDataArgs args)
        {
            await InitializeData(args.Top, args.Skip, args.OrderBy, this.SearchWord, this.SearchDate);
            await InvokeAsync(StateHasChanged);
        }

        private async Task InitializeData(int? pageSize, int? skipItems, string orderBy, string searchWord, DateTime? searchDate)
        {
            var dto = await this.ItemsService.GetItemsOverviewPageModel(pageSize, skipItems, orderBy, searchWord, searchDate);
            var items = dto.OverviewItems.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));

            this.DisplayItems = items;
            this.Count = dto.TotalItemsCount;
        }

        protected async void ClearFilters()
        {
            this.SearchWord = null;
            this.SearchDate = null;

            await Table.Reload();
            StateHasChanged();
        }

        protected async void LoadAutoCompleteData(LoadDataArgs args)
        {
            this.DisplayItemsNames = await ItemsService.GetAllItemsNames(args.Filter);
            await InvokeAsync(StateHasChanged);
        }

        protected async void Search()
        {
            await Table.Reload();
            StateHasChanged();
        }

        protected void QuickAddItem()
        {
            AddItemDialog.Show();
        }

        public async void AddItemDialog_OnDialogClose()
        {
            await Table.Reload();
            StateHasChanged();
        }

        protected void QuickEditItem(int id)
        {
            EditItemDialog.Show(id);
        }

        public async void EditItemDialog_OnDialogClose()
        {
            await Table.Reload();
            StateHasChanged();
        }

        protected void DeleteItemQuestion(int id)
        {
            DeleteItemDialog.Show(id);
        }

        public async void DeleteItemDialog_OnDialogClose()
        {
            await Table.Reload();
            StateHasChanged();
        }

        protected void NavigateToListGeneration()
        {
            NavigationManager.NavigateTo("/shoppinglist");
        }
    }
}
