using AutoMapper;
using ListGenerator.Shared.Dtos;
using ListGenerator.Client.ViewModels;
using ListGenerator.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Components
{
    public partial class EditItemDialog
    {
        public ItemViewModel ItemToUpdate { get; set; }

        [Inject]
        public IItemService ItemService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public bool ShowDialog { get; set; }

        public string ErrorMessage { get; set; }

        public async void Show(int id)
        {
            var response = await this.ItemService.GetItem(id);

            this.ErrorMessage = response.ErrorMessage;

            if (response.IsSuccess)
            {
                this.ItemToUpdate = Mapper.Map<ItemDto, ItemViewModel>(response.Data);
            }

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
            await ItemService.UpdateItem(ItemToUpdate);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
