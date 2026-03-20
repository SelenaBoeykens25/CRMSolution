using Microsoft.AspNetCore.Components;
using Blazored.SessionStorage;

namespace CRMProject.Client.Pages
{
    public class AuthenticatedPageBase : ComponentBase
    {
        [Inject]
        protected ISessionStorageService SessionStorage { get; set; } = default!;

        [Inject]
        protected NavigationManager NavigationManager { get; set; } = default!;

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await CheckAuthentication();
                StateHasChanged();
            }
            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task CheckAuthentication()
        {
            var loggedInEmail = await SessionStorage.GetItemAsync<string>("loggedInEmail");
            
            if (string.IsNullOrEmpty(loggedInEmail))
            {
                NavigationManager.NavigateTo("/login");
            }
        }
    }
}
