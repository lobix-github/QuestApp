using System.Collections.Generic;
using System.Linq;

namespace DbManager.DTO
{
    internal class QuestionDTO
    {
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

        public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();
        public int SelectedAnswer { get; set; }

        public QuestionDTO Clone()
        {
            return new QuestionDTO
            {
                Text_pl = Text_pl,
                Text_en = Text_en,
                Text_de = Text_de,
                Text_nl = Text_nl,
                Comment_pl = Comment_pl,
                Comment_en = Comment_en,
                Comment_de = Comment_de,
                Comment_nl = Comment_nl,
                Wage = Wage,
                Index = Index,
                Answers = Answers.Select(x => x.Clone()).ToList(),
            };
        }
    }
}
