﻿using ListGenerator.Web.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Builders
{
    public interface IItemBuilder
    {
        ItemViewModel BuildItemViewModel();
    }
}
