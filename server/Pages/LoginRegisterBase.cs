using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using QuestApp.DbContexts;
using QuestApp.DTO;
using QuestApp.Services;
using System;
using System.Threading.Tasks;

namespace QuestApp.Pages
{
    public class LoginRegisterBase : ComponentBase
    {
        [Inject] protected IJSRuntime JS { get; set; }
        [Inject] protected DBService dbService { get; set; }
        [Inject] protected QuestDbContext db { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] private IStringLocalizer<Register> ErrorL { get; set; }

        protected UserDTO user;
        protected string ValidationErrorMsg;
        protected bool IsEdit;

        protected bool DontShowError { get; set; } = true;
        protected bool Progressing { get; set; }

        protected virtual async Task<UserDTO> GetUser() => await Task.FromResult(new UserDTO());

        protected void DisplayError(string errorMsg)
        {
            ValidationErrorMsg = ErrorL[errorMsg];
            DontShowError = false;
            Progressing = false;
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JS.InvokeVoidAsync("focusById", IsEdit ? "inputName" : "inputEmail");
            }
            Progressing = false;
        }

        protected override async Task OnInitializedAsync()
        {
            user = await GetUser();
            IsEdit = NavigationManager.Uri.EndsWith("edit");
        }
    }
}
