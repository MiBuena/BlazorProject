using AutoMapper;
using ListGenerator.Client.Components;
using ListGenerator.Client.Services;
using ListGenerator.Client.ViewModels;
using ListGenerator.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Pages
{
    [Authorize]
    public partial class ItemsOverviewTable
    {
        [Inject]
        public IItemService ItemsService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public IEnumerable<ItemOverviewViewModel> Items { get; set; }

        protected AddItemDialog AddItemDialog { get; set; }

        protected EditItemDialog EditItemDialog { get; set; }

        protected DeleteItemDialog DeleteItemDialog { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        protected override async Task OnInitializedAsync()
        {
            this.Items = await InitializeItems();
        }

        private async Task<IEnumerable<ItemOverviewViewModel>> InitializeItems()
        {
            var dtos = await ItemsService.GetItemsOverviewModels();
            var items = dtos.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));
            return items;
        }

        protected void QuickAddItem()
        {
            AddItemDialog.Show();
        }

        public async void AddItemDialog_OnDialogClose()
        {
            this.Items = await InitializeItems();
            StateHasChanged();
        }

        protected void QuickEditItem(int id)
        {
            EditItemDialog.Show(id);
        }

        public async void EditItemDialog_OnDialogClose()
        {
            this.Items = await InitializeItems();
            StateHasChanged();
        }
        protected void DeleteItemQuestion(int id)
        {
            DeleteItemDialog.Show(id);
        }

        public async void DeleteItemDialog_OnDialogClose()
        {
            this.Items = await InitializeItems();
            StateHasChanged();
        }

        protected void NavigateToListGeneration()
        {
            NavigationManager.NavigateTo("/shoppinglist");
        }
    }
}
