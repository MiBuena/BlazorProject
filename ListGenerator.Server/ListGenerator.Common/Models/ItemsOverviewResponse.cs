using ListGenerator.Web.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ListGenerator.Common.Models
{
    public class ItemsOverviewResponse : ApiResponse
    {
        public IEnumerable<ItemViewModel> Items { get; set; } = new List<ItemViewModel>();
    }
}
