using ListGenerator.Common.Models;
using ListGenerator.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.ServerProject.Pages
{
    public partial class ItemEdit
    {
        [Parameter]
        public string ItemId { get; set; }

        public ItemViewModel ItemToAdd { get; set; } = new ItemViewModel();

        public ApiResponse ApiResponse { get; set; } = new ApiResponse();



        protected override Task OnInitializedAsync()
        {
            return base.OnInitializedAsync();
        }
        protected async Task HandleValidSubmit()
        {
            //var apiResponse = await ItemService.AddItem(ItemToAdd);

            //if (!apiResponse.IsSuccess)
            //{
            //    this.ApiResponse = apiResponse;
            //}
            //else
            //{
            //    //ToDo navigate to overview page
            //}
        }
    }
}
