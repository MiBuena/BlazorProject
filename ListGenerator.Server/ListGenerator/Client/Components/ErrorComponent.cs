using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Components
{
    public partial class ErrorComponent
    {
        [Parameter]
        public string ErrorMessage { get; set; }

        public void Close()
        {
            ErrorMessage = null;
            StateHasChanged();
        }
    }
}
