using ListGenerator.Common.Models;
using ListGenerator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Common.Builders
{
    public static class ResponseBuilder
    {
        public static ApiResponse BuildApiResponse(bool isSuccess, string successMessage = null, string errorMessage = null)
        {
            var response = new ApiResponse()
            {
                IsSuccess = isSuccess,
                SuccessMessage = successMessage,
                ErrorMessage = errorMessage
            };

            return response;
        }

        public static ApiResponse BuildApiResponse(bool isSuccess, string errorMessage = null)
        {
            var response = new ApiResponse()
            {
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage
            };

            return response;
        }
        public static GetItemResponse BuildGetItemResponse(ItemViewModel item)
        {
            var isSuccess = true;
            string errorMessage = null;

            if (item == null)
            {
                isSuccess = false;
                errorMessage = "Item doesn't exist";
            }

            var response = new GetItemResponse()
            {
                Item = item,
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage
            };

            return response;
        }

        public static ItemsOverviewResponse BuildItemsOverviewResponse(IEnumerable<ItemViewModel> items)
        {
            var isSuccess = true;
            string errorMessage = null;

            if(items == null)
            {
                isSuccess = false;
                errorMessage = "An error occurred while retrieving items";
            }

            var response = new ItemsOverviewResponse()
            {
                IsSuccess = isSuccess,
                ErrorMessage = errorMessage,
                Items = items
            };

            return response;
        }
    }
}
