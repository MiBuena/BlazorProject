using ListGenerator.Web.Shared.ViewModels;
using ListGenerator.Web.Client.Services;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using ListGenerator.Web.Client.Builders;
using ListGenerator.Web.Shared.Interfaces;

namespace ListGenerator.Web.Client.Components
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

        protected override void OnInitialized()
        {
            ItemToAdd = ItemBuilder.BuildItemViewModel();
        }

        public void Show()
        {
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
