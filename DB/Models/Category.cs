using System.Collections.Generic;
namespace DB.Models
{
    public class Category
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
        public string Prequestion_pl { get; set; }
        public string Prequestion_en { get; set; }
        public string Prequestion_de { get; set; }
        public string Prequestion_nl { get; set; }
        public string PrequestionComment_pl { get; set; }
        public string PrequestionComment_en { get; set; }
        public string PrequestionComment_de { get; set; }
        public string PrequestionComment_nl { get; set; }
        public int Index { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public List<Question> Questions { get; set; } = new List<Question>();
    }
}
