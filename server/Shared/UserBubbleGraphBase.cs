using DB.Models;
using Microsoft.AspNetCore.Components;
using QuestApp.Misc;
using System.Collections.Generic;
using System.Linq;

namespace QuestApp.Shared
{
    public class UserBubbleGraphBase : GraphDataSource
    {
        [Parameter] public ResultsPageFilter Filter { get; set; }

        protected override Dictionary<int, double> GetValues => Values;

        protected List<BubbleChartData> ChartPoints { get; set; } = new List<BubbleChartData>();

        private Dictionary<int, double> XValues => new Dictionary<int, double> { { 1, -2 }, { 2, -1 }, { 3, 0 }, { 4, 1 }, { 5, 2 } };
        private IEnumerable<Submit> Submits => allSubmits
            .Where(s => Filter.selectedCountries.Select(x => (int)x.Id).ToList().Contains(s.CountryId))
            .Where(s => Filter.selectedCitySizes.Select(x => (int)x.Id).ToList().Contains(s.CitySizeId))
            .Where(s => Filter.selectedNumOfEmployees.Select(x => (int)x.Id).ToList().Contains(s.NumberOfEmployeesId))
            .Where(s => Filter.selectedSectors.Select(x => (int)x.Id).ToList().Contains(s.SectorId))
            .Where(s => Filter.selectedServiceAreas.Select(x => (int)x.Id).ToList().Contains(s.ServiceAreaId))
            .Where(s => Filter.selectedOperationTimes.Select(x => (int)x.Id).ToList().Contains(s.OperationTimeId))
            .Where(s => Filter.selectedTurnovers.Select(x => (int)x.Id).ToList().Contains(s.TurnoverId))
            .Where(s => Filter.selectedEnterpriseRoles.Select(x => (int)x.Id).ToList().Contains(s.EnterpriseRoleId));

        protected override void GenerateReport()
        {
            ChartPoints = Submits.Select(s => new BubbleChartData()
            {
                X = sections[1].Categories[1].Questions.Average(q => q.Wage * GetValue(s.Answers.Single(a => q.Answers.Any(x => x.Id == a.Id)).Index, () => XValues)),
                Y = categories.Sum(c => c.Questions.Sum(q => q.Wage * GetValue(s.Answers.Single(a => q.Answers.Any(x => x.Id == a.Id)).Index))),
                Size = s.Id == Filter.SelectedSubmit.Id ? 10 : 2,
                Color = s.Id == Filter.SelectedSubmit.Id ? "#F3C3E2" : "#71CDDC"
            }).ToList();
        }
    }
}
