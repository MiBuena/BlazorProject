using ListGenerator.Common.Models;
using ListGenerator.Models;
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

        public List<ReplenishmentData> ReplenishmentItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var dtos = await ItemService.GetShoppingListItems();

            this.ReplenishmentItems = new List<ReplenishmentData>();

            foreach (var item in dtos)
            {
                var a = new ReplenishmentData()
                {
                    ItemId = item.Id,
                    Name = item.Name
                };

                this.ReplenishmentItems.Add(a);
            }
        }

        protected async Task HandleValidSubmit()
        {
            await this.ItemService.ReplenishItems(this.ReplenishmentItems);
        }
    }
}
