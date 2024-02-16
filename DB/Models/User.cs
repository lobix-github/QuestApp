using System;
using System.Collections.Generic;

namespace DB.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public int CountryId { get; set; }
        public int CitySizeId { get; set; }
        public int NumberOfEmployeesId { get; set; }
        public int SectorId { get; set; }
        public int ServiceAreaId { get; set; }
        public int OperationTimeId { get; set; }
        public int TurnoverId { get; set; }
        public int EnterpriseRoleId { get; set; }
        public List<Questionnaire> Questionnaires { get; set; } = new List<Questionnaire>();
        public List<Questionnaire> EditableQuestionnaires { get; set; } = new List<Questionnaire>();
        public List<Submit> Submits { get; set; } = new List<Submit>();
        
        public DateTime Created { get; set; }
    }
}
