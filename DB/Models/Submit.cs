using System;
using System.Collections.Generic;

namespace DB.Models
{
    public class Submit
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public Questionnaire Questionnaire { get; set; }
        public int QuestionnaireId { get; set; }
        public List<Answer> Answers { get; set; } = new List<Answer>();

        public int CountryId { get; set; }
        public int CitySizeId { get; set; }
        public int NumberOfEmployeesId { get; set; }
        public int SectorId { get; set; }
        public int ServiceAreaId { get; set; }
        public int OperationTimeId { get; set; }
        public int TurnoverId { get; set; }
        public int EnterpriseRoleId { get; set; }


        public DateTime Created { get; set; }
    }
}
