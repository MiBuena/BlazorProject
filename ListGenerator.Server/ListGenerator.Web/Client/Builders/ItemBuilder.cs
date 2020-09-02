using ListGenerator.Web.Shared.Dtos;
using ListGenerator.Web.Shared.Interfaces;
using ListGenerator.Web.Shared.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Builders
{
    public static class ItemBuilder
    {
        public static ItemViewModel Build(DateTime nextReplenishmentDate)
        {
            var model = new ItemViewModel()
            {
                NextReplenishmentDate = nextReplenishmentDate
            };

            return model;
        }
    }
}
