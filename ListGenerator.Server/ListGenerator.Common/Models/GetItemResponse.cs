using ListGenerator.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Common.Models
{
    public class GetItemResponse : ApiResponse
    {
        public ItemViewModel Item { get; set; }
    }
}
