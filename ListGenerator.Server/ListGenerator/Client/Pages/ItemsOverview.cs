using AutoMapper;
using ListGenerator.Shared.Dtos;
using ListGenerator.Client.ViewModels;
using ListGenerator.Client.Components;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using ListGenerator.Client.Services;

namespace ListGenerator.Client.Pages
{
    [Authorize]
    public partial class ItemsOverview
    {
        [Inject]
        public IItemService ItemsService { get; set; }

        [Inject]
        public ITableService TableService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        protected Table OverviewTable { get; set; }

        protected AddItemDialog AddItemDialog { get; set; }

        protected EditItemDialog EditItemDialog { get; set; }

        protected DeleteItemDialog DeleteItemDialog { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            this.OverviewTable = await InitializeTable();
        }

        private async Task<Table> InitializeTable()
        {
            var dtos = await ItemsService.GetItemsOverviewModels();
            var items = dtos.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));

            var table = TableService.GetTable(items);
            return table;
        }

        protected void Sort(int id)
        {
            this.OverviewTable = this.TableService.Sort(id, this.OverviewTable.Items);
            StateHasChanged();

        }

        protected void QuickAddItem()
        {
            AddItemDialog.Show();
        }

        public async void AddItemDialog_OnDialogClose()
        {
            this.OverviewTable = await InitializeTable();
            StateHasChanged();
        }

        protected void QuickEditItem(int id)
        {
            EditItemDialog.Show(id);
        }

        public async void EditItemDialog_OnDialogClose()
        {
            this.OverviewTable = await InitializeTable();
            StateHasChanged();
        }
        protected void DeleteItemQuestion(int id)
        {
            DeleteItemDialog.Show(id);
        }

        public async void DeleteItemDialog_OnDialogClose()
        {
            this.OverviewTable = await InitializeTable();
            StateHasChanged();
        }

        protected void NavigateToListGeneration()
        {
            NavigationManager.NavigateTo("/shoppinglist");
        }
    }
}
