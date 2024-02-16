using Microsoft.Extensions.Localization;
using QuestApp.DTO;
using QuestApp.Pages;
using QuestApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestApp.Misc
{
    public class ResultsPageFilter
    {
        public SubmitDTO SelectedSubmit { get; set; }
        
        public void Init(IStringLocalizer<SharedRes> sharedResL, DBService dbService)
        {
            questions = dbService.GetSections()[1].Categories[0].Questions;

            answers1 = questions[0].Answers.Select(x => new EnumIdText<int>() { Id = x.Id, Text = x.Text });
            selectedAnswers1 = answers1.ToArray();
            answers2 = questions[1].Answers.Select(x => new EnumIdText<int>() { Id = x.Id, Text = x.Text });
            selectedAnswers2 = answers2.ToArray();
            answers3 = questions[2].Answers.Select(x => new EnumIdText<int>() { Id = x.Id, Text = x.Text });
            selectedAnswers3 = answers3.ToArray();

            countries = Enum.GetValues(typeof(CountryId)).OfType<CountryId>().Select(x => new EnumIdText<CountryId>() { Id = x, Text = sharedResL[x.ToString()] });
            selectedCountries = countries.ToArray();

            citySizes = Enum.GetValues(typeof(CitySizeId)).OfType<CitySizeId>().Select(x => new EnumIdText<CitySizeId>() { Id = x, Text = sharedResL[x.ToString()] });
            selectedCitySizes = citySizes.ToArray();

            numOfEmployees = Enum.GetValues(typeof(NumberOfEmployeesId)).OfType<NumberOfEmployeesId>().Select(x => new EnumIdText<NumberOfEmployeesId>() { Id = x, Text = sharedResL[x.ToString()] });
            selectedNumOfEmployees = numOfEmployees.ToArray();

            sectors = Enum.GetValues(typeof(SectorId)).OfType<SectorId>().Select(x => new EnumIdText<SectorId>() { Id = x, Text = sharedResL[x.ToString()] });
            selectedSectors = sectors.ToArray();

            serviceAreas = Enum.GetValues(typeof(ServiceAreaId)).OfType<ServiceAreaId>().Select(x => new EnumIdText<ServiceAreaId>() { Id = x, Text = sharedResL[x.ToString()] });
            selectedServiceAreas = serviceAreas.ToArray();

            operationTimes = Enum.GetValues(typeof(OperationTimeId)).OfType<OperationTimeId>().Select(x => new EnumIdText<OperationTimeId>() { Id = x, Text = sharedResL[x.ToString()] });
            selectedOperationTimes = operationTimes.ToArray();

            turnovers = Enum.GetValues(typeof(TurnoverId)).OfType<TurnoverId>().Select(x => new EnumIdText<TurnoverId>() { Id = x, Text = sharedResL[x.ToString()] });
            selectedTurnovers = turnovers.ToArray();

            enterpriseRoles = Enum.GetValues(typeof(EnterpriseRoleId)).OfType<EnterpriseRoleId>().Select(x => new EnumIdText<EnterpriseRoleId>() { Id = x, Text = sharedResL[x.ToString()] });
            selectedEnterpriseRoles = enterpriseRoles.ToArray();
        }

        public List<QuestionDTO> questions;
        public IEnumerable<EnumIdText<int>> answers1;
        public IEnumerable<EnumIdText<int>> selectedAnswers1;
        public IEnumerable<EnumIdText<int>> answers2;
        public IEnumerable<EnumIdText<int>> selectedAnswers2;
        public IEnumerable<EnumIdText<int>> answers3;
        public IEnumerable<EnumIdText<int>> selectedAnswers3;

        public IEnumerable<EnumIdText<CountryId>> selectedCountries;
        public IEnumerable<EnumIdText<CountryId>> countries;

        public IEnumerable<EnumIdText<CitySizeId>> selectedCitySizes;
        public IEnumerable<EnumIdText<CitySizeId>> citySizes;

        public IEnumerable<EnumIdText<NumberOfEmployeesId>> selectedNumOfEmployees;
        public IEnumerable<EnumIdText<NumberOfEmployeesId>> numOfEmployees;

        public IEnumerable<EnumIdText<SectorId>> selectedSectors;
        public IEnumerable<EnumIdText<SectorId>> sectors;

        public IEnumerable<EnumIdText<ServiceAreaId>> selectedServiceAreas;
        public IEnumerable<EnumIdText<ServiceAreaId>> serviceAreas;

        public IEnumerable<EnumIdText<OperationTimeId>> selectedOperationTimes;
        public IEnumerable<EnumIdText<OperationTimeId>> operationTimes;

        public IEnumerable<EnumIdText<TurnoverId>> selectedTurnovers;
        public IEnumerable<EnumIdText<TurnoverId>> turnovers;

        public IEnumerable<EnumIdText<EnterpriseRoleId>> selectedEnterpriseRoles;
        public IEnumerable<EnumIdText<EnterpriseRoleId>> enterpriseRoles;
    }
}
