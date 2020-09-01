using ListGenerator.Web.Shared.ViewModels;
using ListGenerator.Web.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;

namespace ListGenerator.Web.Client.Components
{
    public partial class AddItemDialog
    {
        public ItemViewModel ItemToAdd { get; set; } = new ItemViewModel();

        [Inject]
        public IItemService ItemService { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public bool ShowDialog { get; set; }

        public void Show()
        {
            ItemToAdd = new ItemViewModel();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            await ItemService.AddItem(ItemToAdd);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
