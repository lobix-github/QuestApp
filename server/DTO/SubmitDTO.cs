using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;

namespace QuestApp.DTO
{
    public class SubmitDTO
    {
        public int Id { get; set; }

        public UserDTO User { get; set; }
        public QuestionnaireDTO Questionnaire { get; set; }
        public List<AnswerDTO> Answers { get; set; } = new List<AnswerDTO>();

        public EnumIdText<CountryId> Country { get; set; } 
        public EnumIdText<CitySizeId> CitySize { get; set; } 
        public EnumIdText<NumberOfEmployeesId> NumberOfEmployees { get; set; } 
        public EnumIdText<SectorId> Sector { get; set; } 
        public EnumIdText<ServiceAreaId> ServiceArea { get; set; } 
        public EnumIdText<OperationTimeId> OperationTime { get; set; } 
        public EnumIdText<TurnoverId> Turnover { get; set; } 
        public EnumIdText<EnterpriseRoleId> EnterpriseRole { get; set; }

        public DateTime Created { get; set; }

        public int Index { get; set; }
        public string Title => $"{L["SurveyDropDownLabel"]} {Index + 1} ({Created})";
        public IStringLocalizer<Pages.ResultsPage> L { get; set; }
    }
}
