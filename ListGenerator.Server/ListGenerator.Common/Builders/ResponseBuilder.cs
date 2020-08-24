using ListGenerator.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Common.Builders
{
    public static class ResponseBuilder
    {
        public static ApiResponse BuildApiResponse(bool isSuccess, string message = null)
        {
            var response = new ApiResponse()
            { 
                IsSuccess = isSuccess,
                Message = message
            };

            return response;
        }
    }
}
