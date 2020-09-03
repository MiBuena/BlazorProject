using ListGenerator.Web.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Web.Client.Models
{
    public class ItemsOverviewResponse : ApiResponse
    {
        public IEnumerable<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
    }
}
