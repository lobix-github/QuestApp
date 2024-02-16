using System.Collections.Generic;
namespace DB.Models
{
    public class Question
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
        public double Wage { get; set; }
        public int Index { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public ICollection<Answer> Answers { get; set; } = new List<Answer>();
    }
}
