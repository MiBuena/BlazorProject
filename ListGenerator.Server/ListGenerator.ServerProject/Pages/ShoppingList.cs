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
    public class ReplenishmentData
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; } = "1";
    }

    public partial class ShoppingList
    {
        [Inject]
        public IItemService ItemService { get; set; }

        public List<ReplenishmentData> ReplenishmentItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var response = await ItemService.GetShoppingListItems();

            this.ReplenishmentItems = new List<ReplenishmentData>();

            foreach (var item in response.Items)
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
            StateHasChanged();
        }
    }
}
