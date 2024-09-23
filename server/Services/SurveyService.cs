using QuestApp.DTO;
using System.Collections.Generic;
using System.Linq;

namespace QuestApp.Services
{
    public class SurveyService
    {
        public SurveyService(DBService dbService)
        {
            Quest = dbService.GetQuestionnaire().Result;
            AnswerIds = Quest.Sections.SelectMany(section => section.Categories.SelectMany(cat => cat.Questions.Select(q => q.SelectedAnswerId)));
        }

        public QuestionnaireDTO Quest { get; }

        public IEnumerable<int> AnswerIds { get; }
    }
}
