using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestApp.Shared
{
    public class TableGeneralGraphBase : GraphDataSource
    {
        protected Dictionary<SectorId, List<double>> rows { get; set; } = new Dictionary<SectorId, List<double>>();
        
        protected override Dictionary<int, double> GetValues => Values;

        protected override void GenerateReport()
        {
            foreach (var sectorId in Enum.GetValues<SectorId>())
            {
                var answers = allSubmits.Where(s => s.SectorId == (int)sectorId).SelectMany(s => s.Answers);
                var questions = categories.SelectMany(x => x.Questions).ToList();
                questions.ForEach(q => q.SelectedAnswers = answers.Where(a => q.Answers.Select(x => x.Id).Contains(a.Id)).ToArray());

                rows[sectorId] = categories.Select(c => c.Questions.Sum(q => q.Wage * q.SelectedAnswers.Select(x => GetValue(x.Index)).AverageOrZero())).ToList();
            }
        }
    }
}
