using AutoMapper;
using ListGenerator.Common.Models;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.ViewModels;
using ListGenerator.Web.Client.Components;
using ListGenerator.Web.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Pages
{
    public partial class ItemsOverview
    {
        [Inject]
        public IItemService ItemService { get; set; }

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
            var dtos = await ItemService.GetItemsOverviewModels();
            this.Items = dtos.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));
        }

        protected void QuickAddItem()
        {
            AddItemDialog.Show();
        }

        public async void AddItemDialog_OnDialogClose()
        {
            var dtos = await ItemService.GetItemsOverviewModels();
            this.Items = dtos.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));
            StateHasChanged();
        }

        protected void QuickEditItem(int id)
        {
            EditItemDialog.Show(id);
        }

        public async void EditItemDialog_OnDialogClose()
        {
            var dtos = await ItemService.GetItemsOverviewModels();
            this.Items = dtos.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));
            StateHasChanged();
        }
        protected void DeleteItemQuestion(int id)
        {
            DeleteItemDialog.Show(id);
        }

        public async void DeleteItemDialog_OnDialogClose()
        {
            var dtos = await ItemService.GetItemsOverviewModels();
            this.Items = dtos.Select(x => Mapper.Map<ItemOverviewDto, ItemOverviewViewModel>(x));
            StateHasChanged();
        }

        protected void NavigateToListGeneration()
        {
            NavigationManager.NavigateTo("/shoppinglist");
        }
    }
}
