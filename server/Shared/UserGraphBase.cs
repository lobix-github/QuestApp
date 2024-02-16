using DB.Models;
using Microsoft.AspNetCore.Components;
using QuestApp.DTO;
using QuestApp.Misc;
using System.Collections.Generic;
using System.Linq;

namespace QuestApp.Shared
{
    public class UserGraphBase : GeneralGraphBase
    {
        [Parameter] public ResultsPageFilter Filter { get; set; }
        [Parameter] public bool Section2Filter { get; set; }

        protected override IEnumerable<Submit> Submits =>
            Section2Filter
                ? allSubmits
                    .Where(s => Filter.selectedAnswers1.Any(sa => s.Answers.Select(a => a.Id).Contains(sa.Id)))
                    .Where(s => Filter.selectedAnswers2.Any(sa => s.Answers.Select(a => a.Id).Contains(sa.Id)))
                    .Where(s => Filter.selectedAnswers3.Any(sa => s.Answers.Select(a => a.Id).Contains(sa.Id)))
                : allSubmits
                    .Where(s => Filter.selectedCountries.Select(x => (int) x.Id).ToList().Contains(s.CountryId))
                    .Where(s => Filter.selectedCitySizes.Select(x => (int) x.Id).ToList().Contains(s.CitySizeId))
                    .Where(s => Filter.selectedNumOfEmployees.Select(x => (int) x.Id).ToList().Contains(s.NumberOfEmployeesId))
                    .Where(s => Filter.selectedSectors.Select(x => (int) x.Id).ToList().Contains(s.SectorId))
                    .Where(s => Filter.selectedServiceAreas.Select(x => (int) x.Id).ToList().Contains(s.ServiceAreaId))
                    .Where(s => Filter.selectedOperationTimes.Select(x => (int) x.Id).ToList().Contains(s.OperationTimeId))
                    .Where(s => Filter.selectedTurnovers.Select(x => (int) x.Id).ToList().Contains(s.TurnoverId))
                    .Where(s => Filter.selectedEnterpriseRoles.Select(x => (int) x.Id).ToList().Contains(s.EnterpriseRoleId));

        protected override double CustomValue(CategoryDTO c) => c.Questions.Sum(q => q.Wage * GetValue(Filter.SelectedSubmit.Answers.Where(a => q.Answers.Select(x => x.Id).Contains(a.Id)).Single().Index));
    }
}
