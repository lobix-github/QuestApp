using DB;
using DB.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;

namespace QuestApp.Pages
{
    public class RegisterBase : LoginRegisterBase
    {
        [Inject] protected IStringLocalizer<Register> L { get; set; }

        protected void HandleInvalidSubmit()
        {
            DisplayError("FieldCannotBeBlank");
        }

        protected async void HandleValidSubmit()
        {
            if (!user.Email.IsValidEmail()) DisplayError("InvalidEmail");
            else if (user.Password != user.ConfirmPassword) DisplayError("PasswordsDontMatch");
            else if (!IsEdit && db.Users.Any(x => x.Email == user.Email)) DisplayError("EmailAlreadyExists");
            else
                await OnValidSubmit();
        }

        protected virtual async Task OnValidSubmit()
        {
            var userModel = user.Map<User>();
            userModel.Role = Role.normal.ToString();
            userModel.Password = Crypto.GetDbPassword(user.Email, user.Password);
            await db.Users.AddAsync(userModel);
            await db.SaveChangesAsync();

            await JS.InvokeVoidAsync("SignIn", userModel.Email, userModel.Role, "/welcome");
        }
    }
}
