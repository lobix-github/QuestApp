using Microsoft.AspNetCore.Components;
using QuestApp.Misc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestApp.Shared
{
    public class SummaryTableBase : GraphDataSource
    {
        [Parameter] public ResultsPageFilter Filter { get; set; }

        protected List<SummaryRow> rows = new List<SummaryRow>();

        protected override Dictionary<int, double> GetValues => Values;
        
        protected override void GenerateReport()
        {
            categories = sections[0].Categories.OrderBy(x => x.Index).ToList();
            var questions = categories.SelectMany(x => x.Questions).ToList();

            var noneAnyValues = categories.Select(c => c.Questions.Sum(q => q.Wage * GetValue(Filter.SelectedSubmit.Answers.Single(a => q.Answers.Any(x => x.Id == a.Id)).Index, () => NoneAnyValues))).ToArray();
            var doubleNoneAnyValues = categories.Select(c => c.Questions.Sum(q => q.Wage * GetValue(Filter.SelectedSubmit.Answers.Single(a => q.Answers.Any(x => x.Id == a.Id)).Index, () => DoubleNoneAnyValues))).ToArray();
            var values = categories.Select(c => c.Questions.Sum(q => q.Wage * GetValue(Filter.SelectedSubmit.Answers.Single(a => q.Answers.Any(x => x.Id == a.Id)).Index, () => Values))).ToArray();

            var selectedSectors = Filter.selectedSectors.Select(s => (int)s.Id).ToArray();
            var industryAnswers = allSubmits.Where(x => selectedSectors.Contains(x.SectorId)).SelectMany(s => s.Answers).ToArray();
            questions.ForEach(q => q.SelectedAnswers = industryAnswers.Where(a => q.Answers.Select(x => x.Id).Contains(a.Id)).ToArray());
            var industryNoneAnyValues = categories.Select(c => c.Questions.Sum(q => q.Wage * q.SelectedAnswers.Select(x => GetValue(x.Index, () => NoneAnyValues)).AverageOrZero())).ToArray();
            var industryDoubleNoneAnyValues = categories.Select(c => c.Questions.Sum(q => q.Wage * q.SelectedAnswers.Select(x => GetValue(x.Index, () => DoubleNoneAnyValues)).AverageOrZero())).ToArray();
            var industryValues = categories.Select(c => c.Questions.Sum(q => q.Wage * q.SelectedAnswers.Select(x => GetValue(x.Index, () => Values)).AverageOrZero())).ToArray();

            var totalAnswers = allSubmits.SelectMany(s => s.Answers).ToArray();
            questions.ForEach(q => q.SelectedAnswers = totalAnswers.Where(a => q.Answers.Select(x => x.Id).Contains(a.Id)).ToArray());
            var totalNoneAnyValues = categories.Select(c => c.Questions.Sum(q => q.Wage * q.SelectedAnswers.Select(x => GetValue(x.Index, () => NoneAnyValues)).AverageOrZero())).ToArray();
            var totalDoubleNoneAnyValues = categories.Select(c => c.Questions.Sum(q => q.Wage * q.SelectedAnswers.Select(x => GetValue(x.Index, () => DoubleNoneAnyValues)).AverageOrZero())).ToArray();
            var totalValues = categories.Select(c => c.Questions.Sum(q => q.Wage * q.SelectedAnswers.Select(x => GetValue(x.Index, () => Values)).AverageOrZero())).ToArray();

            rows.Clear();
            rows.Add(new SummaryRow(ReportTypeId.IPE, noneAnyValues, industryNoneAnyValues, totalNoneAnyValues));
            rows.Add(new SummaryRow(ReportTypeId.IPDC, doubleNoneAnyValues, industryDoubleNoneAnyValues, totalDoubleNoneAnyValues));
            rows.Add(new SummaryRow(ReportTypeId.IPDI, values, industryValues, totalValues));
            rows.Add(new SummaryRow(ReportTypeId.Blank, values));
            rows.Add(new SummaryRow(ReportTypeId.Ratio, values, industryValues, totalValues, noneAnyValues, industryNoneAnyValues, totalNoneAnyValues));
        }

        protected string GetRowStyle(SummaryRow row, int col)
        {
            var colorValue = GetColorValue(row, col);
            if (!string.IsNullOrEmpty(colorValue))
            {
                return $"background: {colorValue};";
            }

            return null;
        }

        private string GetColorValue(SummaryRow row, int col)
        {
            if (double.IsNaN(row.Value(col))) return null;  // ReportTypeId == Blank

            switch (col)
            {
                case -3:
                case -2: return "#71CDDC";
                case -1: return "#5B74AF";
            };
            
            return GetColor(row, col);
        }

        private string GetColor(SummaryRow row, int col) => row.Value(col) switch
        {
            double val when val > row.IndustryValues.AverageOrZero() + 1 => "#7F7F7F",
            double val when val + 1 < row.TotalValues.AverageOrZero() => "#D9D9D9",
            _ => null
        };
    }

    public class SummaryRow
    {
        private readonly ReportTypeId reportType;

        public List<double> TotalValues { get; }
        public List<double> IndustryValues { get; }
        public List<double> Values { get; }
        public List<double> IPETotalValues { get; }
        public List<double> IPEIndustryValues { get; }
        public List<double> IPEValues { get; }

        public SummaryRow(ReportTypeId reportType, IEnumerable<double> values, IEnumerable<double> industryValues = null, IEnumerable<double> totalValues = null,
            IEnumerable<double> ipeValues = null, IEnumerable<double> ipeIndustryValues = null, IEnumerable<double> ipeTotalValues = null)
        {
            this.reportType = reportType;

            TotalValues = totalValues?.ToList();
            IndustryValues = industryValues?.ToList();
            Values = values.ToList();

            IPETotalValues = ipeTotalValues?.ToList();
            IPEIndustryValues = ipeIndustryValues?.ToList();
            IPEValues = ipeValues?.ToList();
        }

        public double Value(int i)
        {
            double result = double.NaN;

            if (new[] { ReportTypeId.IPE, ReportTypeId.IPDC, ReportTypeId.IPDI }.Contains(this.reportType))
            {
                if (i == -1)
                {
                    result = Values.Sum();
                }

                else if (i == -2)
                {
                    result = IndustryValues.Sum();
                }

                else if (i == -3)
                {
                    result = TotalValues.Sum();
                }

                else result = Values[i];
            }
            else if (reportType == ReportTypeId.Ratio)
            {
                if (i == -1)
                {
                    result = Values.Sum() / IPEValues.Sum();
                }

                else if (i == -2)
                {
                    result = IndustryValues.Sum() / IPEIndustryValues.Sum();
                }

                else if (i == -3)
                {
                    result = TotalValues.Sum() / IPETotalValues.Sum();
                }

                else result = Values[i] / IPEValues[i];

                result *= 100;
            }

            return Math.Truncate(result * 100) / 100;
        }

        public string ValueStr(int value) => reportType != ReportTypeId.Blank
                                                    ? $"{Value(value).ToString("0.00")}{(reportType == ReportTypeId.Ratio ? "%" : string.Empty)}"
                                                    : string.Empty;

        public string Title => reportType switch
        {
            ReportTypeId.IPE => "General AnyNone Graph Title",
            ReportTypeId.IPDC => "General Double AnyNone Graph Title",
            ReportTypeId.IPDI => "General Graph Title",
            ReportTypeId.Blank => "&nbsp;",
            ReportTypeId.Ratio => "Summary Ratio Ttile",
            _ => throw new NotImplementedException(),
        };
    }
}
