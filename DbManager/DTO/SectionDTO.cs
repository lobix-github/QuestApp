using System.Collections.Generic;
using System.Linq;

namespace DbManager.DTO
{
    internal class SectionDTO
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
        public int Index { get; set; }

        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();

        public SectionDTO Clone()
        {
            return new SectionDTO
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
                Index = Index,
                Categories = Categories.Select(x => x.Clone()).ToList(),
            };
        }
    }
}
