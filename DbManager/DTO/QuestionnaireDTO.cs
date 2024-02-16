using System.Collections.Generic;
using System.Linq;

namespace DbManager.DTO
{
    internal class QuestionnaireDTO
    {
        public string Description_pl { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_nl { get; set; }

        public List<SectionDTO> Sections { get; set; } = new List<SectionDTO>();

        public QuestionnaireDTO Clone()
        {
            return new QuestionnaireDTO
            {
                Description_pl = Description_pl,
                Description_en = Description_en,
                Description_de = Description_de,
                Description_nl = Description_nl,
                Sections = Sections.Select(x => x.Clone()).ToList(),
            };
        }
    }
}
