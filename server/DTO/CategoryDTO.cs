using System.Collections.Generic;

namespace QuestApp.DTO
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Comment { get; set; }
        public string Prequestion { get; set; }
        public string PrequestionComment { get; set; }
        public int Index { get; set; }
        public SectionDTO Section { get; set; }
        public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();
    }
}
