using DB.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using QuestApp.DTO;
using QuestApp.Pages;
using QuestApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QuestApp.Shared
{
    public abstract class GraphDataSource : ComponentBase
    {
        [Inject] protected DBService dbService { get; set; }
        [Inject] protected IStringLocalizer<SharedRes> SharedResL { get; set; }

        protected double GetValue(int index, Func<Dictionary<int, double>> getValues = null) => (getValues?.Invoke() ?? GetValues)[index];
        protected abstract Dictionary<int, double> GetValues { get; } 
        protected Dictionary<int, double> Values => new Dictionary<int, double> { { 1, 0 }, { 2, 0 }, { 3, 2 }, { 4, 5 }, { 5, 10 } };
        protected Dictionary<int, double> NoneAnyValues => new Dictionary<int, double> { { 1, 0 }, { 2, 10 }, { 3, 10 }, { 4, 10 }, { 5, 10 } };
        protected Dictionary<int, double> DoubleNoneAnyValues => new Dictionary<int, double> { { 1, 0 }, { 2, 0 }, { 3, 10 }, { 4, 10 }, { 5, 10 } };

        protected List<Submit> allSubmits;
        protected SectionDTO[] sections;
        protected List<CategoryDTO> categories = new List<CategoryDTO>();

        protected override void OnInitialized()
        {
            sections = dbService.GetSections();
            allSubmits = dbService.GetAllSubmits();
            categories = sections[0].Categories.OrderBy(x => x.Index).ToList();

            GenerateReport();
        }

        public void OnChange()
        {
            GenerateReport();
        }

        protected abstract void GenerateReport();
    }
}
