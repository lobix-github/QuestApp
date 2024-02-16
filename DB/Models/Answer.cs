using System.Collections.Generic;

namespace DB.Models
{
    public class Answer
    {
        public int Id { get; set; }
        public string Text_pl { get; set; }
        public string Text_en { get; set; }
        public string Text_de { get; set; }
        public string Text_nl { get; set; }
        public string Comment_pl { get; set; }
        public string Comment_en { get; set; }
        public string Comment_de { get; set; }
        public string Comment_nl { get; set; }
        public int Index { get; set; }
        public Question Question { get; set; }
        public int QuestionId { get; set; }
        public List<Submit> Submits { get; set; } = new List<Submit>();
    }
}
