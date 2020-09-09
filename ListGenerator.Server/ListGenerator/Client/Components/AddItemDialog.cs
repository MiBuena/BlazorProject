using ListGenerator.Client.ViewModels;
using ListGenerator.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using ListGenerator.Client.Builders;
using ListGenerator.Shared.Interfaces;

namespace ListGenerator.Client.Components
{
    public partial class AddItemDialog
    {
        public ItemViewModel ItemToAdd { get; set; }

        [Inject]
        public IItemService ItemService { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        [Inject]
        public IItemBuilder ItemBuilder { get; set; }

        public bool ShowDialog { get; set; }

        public void Show()
        {
            ItemToAdd = ItemBuilder.BuildItemViewModel();
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
