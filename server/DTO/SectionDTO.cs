using System.Collections.Generic;

namespace QuestApp.DTO
{
    public class SectionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Comment { get; set; }
        public int Index { get; set; }
        public List<CategoryDTO> Categories { get; set; } = new List<CategoryDTO>();
    }
}
