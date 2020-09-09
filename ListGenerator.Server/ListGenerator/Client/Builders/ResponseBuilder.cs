using ListGenerator.Client.Models;

namespace ListGenerator.Client.Builders
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
    }
}
