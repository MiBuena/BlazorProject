using ListGenerator.Client.Interfaces;
using ListGenerator.Client.Models;
using ListGenerator.Client.Services;
using ListGenerator.Shared.Responses;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Pages
{
    public partial class ChangeLanguage
    {
        [Inject]
        private ICultureService CultureService { get; set; }

        private void OnSelected(ChangeEventArgs e)
        {
            var culture = (string)e.Value;
            CultureService.ChangeCulture(culture);
        }
    }
}
