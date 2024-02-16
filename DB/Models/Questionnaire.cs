using System;
using System.Collections.Generic;

namespace DB.Models
{
    public class Questionnaire
    {
        public int Id { get; set; }
        public string Description_pl { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_nl { get; set; }
        public User Author { get; set; }
        public int AuthorId { get; set; }
        public List<User> Editors { get; set; } = new List<User>();
        public List<Section> Sections { get; set; } = new List<Section>();
        public List<Submit> Submits { get; set; } = new List<Submit>();

        public bool IsDeleted { get; set; }
        public DateTime Created { get; set; }
    }
}
