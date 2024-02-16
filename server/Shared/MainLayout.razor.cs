using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using QuestApp.Services;

namespace QuestApp.Layouts
{
    public partial class MainLayoutComponent
    {
        [Inject] private IJSRuntime JS { get; set; }
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] protected IStringLocalizer<App> L { get; set; }
        [Inject] protected ICultureService<App> CS { get; set; }

        protected async void LogOut()
        {
            await JS.InvokeVoidAsync("SignOut", "/");
        }

        protected string _welcomeText;
        protected string WelcomeText { get { GetWelcomeText(); return _welcomeText; } }

        private async void GetWelcomeText()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            _welcomeText = $"{L["Hello"]}, {user.Identity.Name}!";
        }
    }
}
