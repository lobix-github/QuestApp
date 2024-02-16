using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using QuestApp.Services;
using System.Threading.Tasks;

namespace QuestApp.Pages
{
    public partial class MainPage
    {
        [Inject] protected DBService dbService { get; set; }
        [Inject] protected IStringLocalizer<MainPage> L { get; set; }
        [Inject] protected ICultureService<MainPage> CS { get; set; }

        protected int usersCount;
        protected int submitsCount;
        protected int generatedReportsCount;

        protected async override Task OnInitializedAsync()
        {
            var stats = await dbService.GetStatsAsync();
            usersCount = stats.AllUsersCount;
            submitsCount = stats.AllSubmitsCount;
            generatedReportsCount = stats.AllGeneratedReportsCount;
        }
    }
}
