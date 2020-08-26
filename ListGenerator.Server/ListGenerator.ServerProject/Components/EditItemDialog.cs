using AutoMapper;
using ListGenerator.Common.Models;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.ViewModels;
using ListGenerator.ServerProject.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Components
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
