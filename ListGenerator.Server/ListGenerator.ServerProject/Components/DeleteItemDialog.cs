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
    public partial class DeleteItemDialog
    {
        [Inject]
        public IItemService ItemService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public ItemViewModel Item { get; set; }

        public bool ShowDialog { get; set; }

        protected async Task DeleteItem()
        {
            await ItemService.DeleteItem(Item.Id);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        public async void Show(int id)
        {
            var dto = await this.ItemService.GetItem(id);
            this.Item = Mapper.Map<ItemDto, ItemViewModel>(dto);
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
