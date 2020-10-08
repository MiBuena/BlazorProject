using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListGenerator.Client.Components
{
    public partial class ErrorComponent
    {
        private string ErrorMessage { get; set; }

        private bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public void Show(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ErrorMessage = null;
            ShowDialog = false;
            StateHasChanged();
        }
    }
}
