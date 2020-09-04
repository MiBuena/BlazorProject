using AutoMapper;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Client.ViewModels;
using ListGenerator.Web.Client.Components;
using ListGenerator.Web.Client.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace ListGenerator.Web.Client.Pages
{
    [Authorize]
    public partial class ItemsOverview
    {
        [Inject]
        public IItemService ItemsService { get; set; }

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

            var headings = await ItemsService.GetItemsOverviewHeadings();

            return new Table()
            {
                Items = items,
                Headings = headings
            };
        }

        protected void Sort(int id)
        {
            this.ItemsService.Sort(id);
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
