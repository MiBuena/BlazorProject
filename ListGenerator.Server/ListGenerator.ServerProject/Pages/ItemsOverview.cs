using ListGenerator.Common.Models;
using ListGenerator.ServerProject.Components;
using ListGenerator.ServerProject.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Pages
{
    public partial class ItemsOverview
    {
        [Inject]
        public IItemService ItemService { get; set; }

        public ItemsOverviewResponse Response { get; set; }

        protected AddItemDialog AddItemDialog { get; set; }

        protected EditItemDialog EditItemDialog { get; set; }


        [Parameter]
        public string Message { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Response = await ItemService.GetAllItems();
        }

        protected void QuickAddItem()
        {
            AddItemDialog.Show();
        }

        public async void AddItemDialog_OnDialogClose()
        {
            this.Response = await ItemService.GetAllItems();
            StateHasChanged();
        }

        protected void QuickEditItem(int id)
        {
            EditItemDialog.Show(id);
        }

        public async void EditItemDialog_OnDialogClose()
        {
            this.Response = await ItemService.GetAllItems();
            StateHasChanged();
        }
    }
}
