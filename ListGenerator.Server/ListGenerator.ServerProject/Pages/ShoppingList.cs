using AutoMapper;
using ListGenerator.Common.Models;
using ListGenerator.Models;
using ListGenerator.Models.Dtos;
using ListGenerator.Models.ViewModels;
using ListGenerator.ServerProject.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Pages
{
    public partial class ShoppingList
    {
        [Inject]
        public IItemService ItemService { get; set; }

        [Inject]
        public IMapper Mapper { get; set; }

        public IEnumerable<PurchaseItemViewModel> ReplenishmentItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var dtos = await ItemService.GetShoppingListItems();
            this.ReplenishmentItems = dtos.Select(x => Mapper.Map<ItemDto, PurchaseItemViewModel>(x));
        }

        protected async Task HandleValidSubmit()
        {
            await this.ItemService.ReplenishItems(this.ReplenishmentItems);
        }
    }
}
