using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Shared.Responses
{
    public class Response<T>
    {
        public T Data { get; set; }

        public bool IsSuccess { get; set; }

        public string ErrorMessage { get; set; }

        public string SuccessMessage { get; set; }
    }
}
