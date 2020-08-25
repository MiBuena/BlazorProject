using ListGenerator.Common.Models;
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

        public ItemsOverviewResponse Response { get; set; }

        protected override async Task OnInitializedAsync()
        {
            this.Response = await ItemService.GetShoppingListItems();
        }
    }
}
