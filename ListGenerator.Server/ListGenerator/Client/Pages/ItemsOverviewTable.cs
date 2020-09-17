using AutoMapper;
using ListGenerator.Client.Components;
using ListGenerator.Client.Services;
using ListGenerator.Client.ViewModels;
using ListGenerator.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Radzen;
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


        protected override async Task OnInitializedAsync()
        {
            await InitializeItems();
        }

        private async Task InitializeItems()
        {
            var dtos = await this.ItemsService.GetItemsOverviewModels(0, 5, null);
            var items = dtos.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));

            this.DisplayItems = items;

            Count = 25;
        }

        protected async Task LoadData(LoadDataArgs args)
        {
            var dtos = await this.ItemsService.GetItemsOverviewModels(args.Skip, args.Top, args.OrderBy);
            var items = dtos.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));

            this.DisplayItems = items;

            Count = 25;

            if (!string.IsNullOrEmpty(args.Filter))
            {
                //query = query.Where(args.Filter);
            }

            if (!string.IsNullOrEmpty(args.OrderBy))
            {
                //query = query.OrderBy(args.OrderBy);
            }

            //employees = query.Skip(args.Skip.Value).Take(args.Top.Value).ToList();

            //count = dbContext.Employees.Count();

            await InvokeAsync(StateHasChanged);
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
            await InitializeItems();
            StateHasChanged();
        }

        protected void QuickEditItem(int id)
        {
            EditItemDialog.Show(id);
        }

        public async void EditItemDialog_OnDialogClose()
        {
            await this.InitializeItems();
            StateHasChanged();
        }
        protected void DeleteItemQuestion(int id)
        {
            DeleteItemDialog.Show(id);
        }

        public async void DeleteItemDialog_OnDialogClose()
        {
            await InitializeItems();
            StateHasChanged();
        }

        protected void NavigateToListGeneration()
        {
            NavigationManager.NavigateTo("/shoppinglist");
        }
    }
}
