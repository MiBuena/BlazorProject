using ListGenerator.Shared.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Server.Builders
{
    public static class ResponseBuilder
    {
        public static Response<T>Success<T>(T data = null, string successMessage = null) where T : class
        {
            var response = new Response<T>()
            {
                IsSuccess = true,
                Data = data,
                SuccessMessage = successMessage
            };

            return response;
        }

        public static Response<T> Failure<T>(string errorMessage = null) where T : class
        {
            var response = new Response<T>()
            {
                IsSuccess = false,
                ErrorMessage = errorMessage,
            };

            return response;
        }
    }
}
