using System.Collections.Generic;
using System.Linq;

namespace DbManager.DTO
{
    internal class CategoryDTO
    {
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

        public List<QuestionDTO> Questions { get; set; } = new List<QuestionDTO>();

        public CategoryDTO Clone()
        {
            return new CategoryDTO
            {
                Title_pl = Title_pl,
                Title_en = Title_en,
                Title_de = Title_de,
                Title_nl = Title_nl,
                Text_pl = Text_pl,
                Text_en = Text_en,
                Text_de = Text_de,
                Text_nl = Text_nl,
                Comment_pl = Comment_pl,
                Comment_en = Comment_en,
                Comment_de = Comment_de,
                Comment_nl = Comment_nl,
                Prequestion_pl = Prequestion_pl,
                Prequestion_en = Prequestion_en,
                Prequestion_de = Prequestion_de,
                Prequestion_nl = Prequestion_nl,
                PrequestionComment_pl = PrequestionComment_pl,
                PrequestionComment_en = PrequestionComment_en,
                PrequestionComment_de = PrequestionComment_de,
                PrequestionComment_nl = PrequestionComment_nl,
                Index = Index,
                Questions = Questions.Select(x => x.Clone()).ToList(),
            };
        }
    }
}
