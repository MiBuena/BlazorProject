using ListGenerator.Common.Models;
using ListGenerator.Models.ViewModels;
using ListGenerator.ServerProject.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Components
{
    public partial class AddItemDialog
    {
        public ItemViewModel ItemToAdd { get; set; } = new ItemViewModel();

        public ApiResponse ApiResponse { get; set; } = new ApiResponse();

        [Inject]
        public IItemService ItemService { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public bool ShowDialog { get; set; }

        public void Show()
        {
            ItemToAdd = new ItemViewModel();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            await ItemService.AddItem(ItemToAdd);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
