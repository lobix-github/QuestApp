using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using QuestApp.DTO;
using System.Threading.Tasks;
using Radzen;

namespace QuestApp.Pages
{
    public class EditProfileBase : RegisterBase
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] protected DialogService DialogService { get; set; }
        [Inject] protected new IStringLocalizer<EditProfile> L { get; set; }

        protected virtual async Task ShowInlineDialog() => await Task.FromResult((object)null);

        protected override async Task<UserDTO> GetUser()
        {
            var user = await AuthenticationStateProvider.GetUserAsync(db);
            var userDTO = user.Map<UserDTO>();
            userDTO.ConfirmPassword = userDTO.Password;
            return await Task.FromResult(userDTO);
        }

        protected override async Task OnValidSubmit()
        {
            await dbService.SubmitUserAsync(user);
            await ShowInlineDialog();
        }
    }
}
