using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListGenerator.Web.Server.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ListGenerator.Web.Server.Controllers
{
    public abstract class IdentityController : ControllerBase
    {
        protected string UserId => this.User.GetId();
    }
}
