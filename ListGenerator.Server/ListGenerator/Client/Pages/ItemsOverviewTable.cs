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

        public IEnumerable<ItemOverviewViewModel> DisplayItems { get; set; }

        public IEnumerable<ItemOverviewViewModel> OriginalItems { get; set; }

        protected AddItemDialog AddItemDialog { get; set; }

        protected EditItemDialog EditItemDialog { get; set; }

        protected DeleteItemDialog DeleteItemDialog { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected RadzenGrid<ItemOverviewViewModel> Table { get; set; }


        protected async Task LoadData(LoadDataArgs args)
        {
            await InitializeData(args.Top, args.Skip, args.OrderBy);
            await InvokeAsync(StateHasChanged);
        }

        private async Task InitializeData(int? pageSize, int? skipItems, string orderBy)
        {
            var dto = await this.ItemsService.GetItemsOverviewPageModel(pageSize, skipItems, orderBy);
            var items = dto.OverviewItems.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));

            this.DisplayItems = items;
            this.Count = dto.TotalItemsCount;
        }

        protected void Search()
        {
            this.DisplayItems = ItemsService.ApplyFilters(this.SearchWord, this.SearchDate, this.OriginalItems);
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
