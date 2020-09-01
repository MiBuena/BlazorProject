using AutoMapper;
using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.ViewModels;
using ListGenerator.Web.Client.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Components
{
    public partial class EditItemDialog
    {
        public ItemViewModel ItemToUpdate { get; set; } = new ItemViewModel();

        [Inject]
        public IItemService ItemService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public bool ShowDialog { get; set; }

        public async void Show(int id)
        {
            var dto = await this.ItemService.GetItem(id);
            this.ItemToUpdate = Mapper.Map<ItemDto, ItemViewModel>(dto);
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
