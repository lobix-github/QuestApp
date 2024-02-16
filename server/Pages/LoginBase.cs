using DB;
using DB.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Linq;

namespace QuestApp.Pages
{
    public class LoginBase : LoginRegisterBase
    {
        [Inject] protected IStringLocalizer<Login> L { get; set; }

        protected async void HandleValidSubmit()
        {
            User userModel;
            Progressing = true;

            if (!user.Email.IsValidEmail()) DisplayError("InvalidEmail");
            else if ((userModel = db.Users.FirstOrDefault(x => x.Email == user.Email))?.Password != Crypto.GetDbPassword(user.Email, user.Password)) DisplayError("WrongEmailOrPassword");
            else
                await JS.InvokeVoidAsync("SignIn", user.Email, userModel.Role, "/welcome");
        }
    }
}
