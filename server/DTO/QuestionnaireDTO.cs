using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace QuestApp.DTO
{
    public class QuestionnaireDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public List<SectionDTO> Sections { get; set; } = new List<SectionDTO>();
    }
}
