using System;
using System.Collections.Generic;
using System.Linq;
using DB.Models;
using QuestApp.DTO;
using QuestApp.Misc;

namespace QuestApp.Shared
{
    public class GeneralGraphBase : GraphDataSource
    {
        protected static double MARGIN = 0.2d;

        protected List<GraphChartData> ChartPoints { get; set; } = new List<GraphChartData> { new GraphChartData() { Category = "", High = 1, Custom = 0, Low = 2 } };
        
        protected override Dictionary<int, double> GetValues => Values;

        protected virtual IEnumerable<Submit> Submits => allSubmits;
        protected virtual double CustomValue(CategoryDTO c) => c.Questions.Sum(q => q.Wage * q.SelectedAnswers.Select(x => GetValue(x.Index)).AverageOrZero());

        protected override void GenerateReport()
        {
            var submits = Submits;
            var answers = submits.SelectMany(s => s.Answers);
            var questions = categories.SelectMany(x => x.Questions).ToList();
            questions.ForEach(q => q.SelectedAnswers = answers.Where(a => q.Answers.Select(x => x.Id).Contains(a.Id)).ToArray());

            var margin = (int)(submits.Count() * MARGIN);
            ChartPoints = categories.Select(c => new GraphChartData()
            {
                Category = c.Title,
                High = c.Questions.Sum(q => q.Wage * q.SelectedAnswers.OrderByDescending(x => GetValue(x.Index)).Take(margin).Select(x => GetValue(x.Index)).AverageOrZero()),
                Custom = CustomValue(c),
                Low = c.Questions.Sum(q => q.Wage * q.SelectedAnswers.OrderBy(x => GetValue(x.Index)).Take(margin).Select(x => GetValue(x.Index)).AverageOrZero())
            }).ToList();
        }
    }
}
