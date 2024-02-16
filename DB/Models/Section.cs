using System.Collections.Generic;
namespace DB.Models
{
    public class Section
    {
        public int Id { get; set; }
        public string Title_pl { get; set; }
        public string Title_en { get; set; }
        public string Title_de { get; set; }
        public string Title_nl { get; set; }
        public string Text_pl { get; set; }
        public string Text_en { get; set; }
        public string Text_de { get; set; }
        public string Text_nl { get; set; }
        public string Comment_pl { get; set; }
        public string Comment_en { get; set; }
        public string Comment_de { get; set; }
        public string Comment_nl { get; set; }
        public int Index { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public int QuestionnaireId { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
    }
}
