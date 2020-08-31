﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace ListGenerator.Web.Server.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetId(this ClaimsPrincipal claimsPrincipal)
        {
            if (claimsPrincipal == null)
            {
                return null;
            }

            if (claimsPrincipal.Identity.IsAuthenticated)
            {
                return claimsPrincipal.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            }

            return null;
        }
    }
}