using ListGenerator.Web.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Builders
{
    public interface IReplenishmentBuilder
    {
        ReplenishmentDto BuildReplenishmentDto();
    }
}
