using DB.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using QuestApp.DbContexts;
using QuestApp.DTO;
using QuestApp.Services;
using QuestApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestApp.Pages
{
    public partial class Survey
    {
        [Inject] private AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IStringLocalizer<Survey> L { get; set; }
        [Inject] protected QuestDbContext db { get; set; }
        [Inject] protected DBService dbService { get; set; }
        [Inject] protected SurveyService surveyService { get; set; }

        [Parameter] public int? Index { get; set; }

        private bool pageReloaded;

        protected Dictionary<int, CategoryDTO> categories;
        protected bool isSubmitDisabled = true;
        protected bool isPrevDisabled => Index <= 1;
        protected bool isNextDisabled => Index >= categories.Count;
        protected SectionDTO section => category.Section;
        protected CategoryDTO category => categories[Index.Value];
        protected QuestionnaireDTO quest => surveyService.Quest;

        protected AnchorNavigation navigation;

        protected override void OnInitialized()
        {
            categories = quest.Sections.SelectMany(x => x.Categories).ToDictionary(c => c.Index, c => c);
            Index = Math.Max(1, Math.Min(categories.Count, Index ?? 1));
        }

        protected override void OnParametersSet()
        {
            pageReloaded = true;
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            var jump = pageReloaded;
            pageReloaded = false;
            var firstIndexes = quest.Sections.Select(x => x.Categories.OrderBy(c => c.Index).First().Index).ToList();
            if (!firstRender && jump && !firstIndexes.Contains(Index.Value))
            {
                await navigation.ScrollToFragment("#category-start");
            }
        }

        protected async void HandleValidSubmit()
        {
            var user = await AuthenticationStateProvider.GetUserAsync(db);
            var questionnaire = db.Questionnaires.Single(x => x.Id == quest.Id);
            var answers = db.Answers.Where(x => surveyService.AnswerIds.Contains(x.Id)).ToList();
            var submit = user.Map<Submit>();
            submit.Id = 0;
            submit.User = user;
            submit.Questionnaire = questionnaire;
            submit.Answers = answers;
            submit.Created = DateTime.UtcNow;
            var id = await dbService.SubmitAsync(submit);

            NavigationManager.NavigateTo($"/results/{id}");
        }

        protected void OnAnswerChange()
        {
            isSubmitDisabled = surveyService.AnswerIds.Any(x => x == 0);
        }
    }
}
