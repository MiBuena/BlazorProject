using ListGenerator.Common.Models;
using ListGenerator.Models.Entities;
using ListGenerator.Models.ViewModels;
using ListGenerator.ServerProject.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Pages
{
    public partial class ItemAdd
    {
        public ItemViewModel ItemToAdd { get; set; } = new ItemViewModel();

        public ApiResponse ApiResponse { get; set; } = new ApiResponse();

        [Inject]
        public IItemService ItemService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        protected async Task HandleValidSubmit()
        {
            var apiResponse = await ItemService.AddItem(ItemToAdd);

            if (!apiResponse.IsSuccess)
            {
                this.ApiResponse = apiResponse;
            }
            else
            {
                NavigateToOverview();
            }
        }

        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo($"/allitems");
        }
    }
}
