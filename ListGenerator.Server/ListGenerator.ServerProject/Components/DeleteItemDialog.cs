using ListGenerator.Common.Models;
using ListGenerator.ServerProject.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Components
{
    public partial class DeleteItemDialog
    {
        public ApiResponse ApiResponse { get; set; } = new ApiResponse();

        [Inject]
        public IItemService ItemService { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public int ItemId { get; set; }

        public string ItemName { get; set; }

        public bool ShowDialog { get; set; }

        protected async Task DeleteItem()
        {
            await ItemService.DeleteItem(ItemId);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        public async void Show(int id)
        {
            var response = await this.ItemService.GetItem(id);
            this.ItemId = response.Item.Id;
            this.ItemName = response.Item.Name;
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }
    }
}
