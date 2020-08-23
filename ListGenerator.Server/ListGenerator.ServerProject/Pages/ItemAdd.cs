using ListGenerator.Models.Entities;
using ListGenerator.ServerProject.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Pages
{
    public partial class ItemAdd
    {
        public Item ItemToAdd { get; set; } = new Item();

        [Inject]
        public IItemService ItemService { get; set; }


        protected async Task HandleValidSubmit()
        {
            var addedEmployee = await ItemService.AddItem(ItemToAdd);
        }

        protected void HandleInvalidSubmit()
        {

        }

    }
}
