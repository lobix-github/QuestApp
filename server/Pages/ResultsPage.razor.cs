using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using QuestApp.DbContexts;
using QuestApp.DTO;
using QuestApp.Misc;
using QuestApp.Services;
using QuestApp.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuestApp.Pages
{
    public partial class ResultsPage : ComponentBase
    {
        [Inject] protected IJSRuntime JS { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        [Inject] protected IStringLocalizer<ResultsPage> L { get; set; }
        [Inject] protected IStringLocalizer<SharedRes> SharedResL { get; set; }
        [Inject] protected QuestDbContext db { get; set; }
        [Inject] protected DBService dbService { get; set; }

        [Parameter] public int? Id { get; set; }
        [Parameter] public int countriesId { get; set; }
        [Parameter] public int citySizesId { get; set; }
        [Parameter] public int numOfEmployeesId { get; set; }
        [Parameter] public int sectorsId { get; set; }
        [Parameter] public int serviceAreasId { get; set; }
        [Parameter] public int operationTimesId { get; set; }
        [Parameter] public int turnoversId { get; set; }
        [Parameter] public int enterpriseRolesId { get; set; }
        [Parameter] public string answers1Id { get; set; }
        [Parameter] public string answers2Id { get; set; }
        [Parameter] public string answers3Id { get; set; }
        [Parameter] public int bubbleCountriesId { get; set; }
        [Parameter] public int bubbleCitySizesId { get; set; }
        [Parameter] public int bubbleNumOfEmployeesId { get; set; }
        [Parameter] public int bubbleSectorsId { get; set; }
        [Parameter] public int bubbleServiceAreasId { get; set; }
        [Parameter] public int bubbleOperationTimesId { get; set; }
        [Parameter] public int bubbleTurnoversId { get; set; }
        [Parameter] public int bubbleEnterpriseRolesId { get; set; }

        private IEnumerable<SubmitDTO> submits;
        private ResultsPageFilter filter = new ResultsPageFilter();
        private ResultsPageFilter bubbleFilter = new ResultsPageFilter();

        protected override async Task OnInitializedAsync()
        {
            submits = await dbService.GetUserSubmitsAsync();
            filter.Init(SharedResL, dbService);
            if (Id.HasValue)
            {
                filter.SelectedSubmit = submits.Single(x => x.Id == Id);
                filter.selectedCountries = filter.countries.Where(x => EnumMapper.GetCollectionFromMappedId<CountryId>(countriesId).Contains(x.Id));
                filter.selectedCitySizes = filter.citySizes.Where(x => EnumMapper.GetCollectionFromMappedId<CitySizeId>(citySizesId).Contains(x.Id));
                filter.selectedNumOfEmployees = filter.numOfEmployees.Where(x => EnumMapper.GetCollectionFromMappedId<NumberOfEmployeesId>(numOfEmployeesId).Contains(x.Id));
                filter.selectedSectors = filter.sectors.Where(x => EnumMapper.GetCollectionFromMappedId<SectorId>(sectorsId).Contains(x.Id));
                filter.selectedServiceAreas = filter.serviceAreas.Where(x => EnumMapper.GetCollectionFromMappedId<ServiceAreaId>(serviceAreasId).Contains(x.Id));
                filter.selectedOperationTimes = filter.operationTimes.Where(x => EnumMapper.GetCollectionFromMappedId<OperationTimeId>(operationTimesId).Contains(x.Id));
                filter.selectedTurnovers = filter.turnovers.Where(x => EnumMapper.GetCollectionFromMappedId<TurnoverId>(turnoversId).Contains(x.Id));
                filter.selectedEnterpriseRoles = filter.enterpriseRoles.Where(x => EnumMapper.GetCollectionFromMappedId<EnterpriseRoleId>(enterpriseRolesId).Contains(x.Id));
                filter.selectedAnswers1 = answers1Id == "-" ? Enumerable.Empty<EnumIdText<int>>() : filter.answers1.Where(a => answers1Id.Split("-").Select(x => Convert.ToInt32(x)).Contains(a.Id));
                filter.selectedAnswers2 = answers2Id == "-" ? Enumerable.Empty<EnumIdText<int>>() : filter.answers2.Where(a => answers2Id.Split("-").Select(x => Convert.ToInt32(x)).Contains(a.Id));
                filter.selectedAnswers3 = answers3Id == "-" ? Enumerable.Empty<EnumIdText<int>>() : filter.answers3.Where(a => answers3Id.Split("-").Select(x => Convert.ToInt32(x)).Contains(a.Id));
            }
            else
            {
                filter.SelectedSubmit = submits.LastOrDefault();
            }
            filter.selectedSectors = filter.sectors.Where(x => x.Id == filter.SelectedSubmit.Sector.Id);

            bubbleFilter.Init(SharedResL, dbService);
            if (Id.HasValue)
            {
                bubbleFilter.SelectedSubmit = submits.Single(x => x.Id == Id);
                bubbleFilter.selectedCountries = bubbleFilter.countries.Where(x => EnumMapper.GetCollectionFromMappedId<CountryId>(bubbleCountriesId).Contains(x.Id));
                bubbleFilter.selectedCitySizes = bubbleFilter.citySizes.Where(x => EnumMapper.GetCollectionFromMappedId<CitySizeId>(bubbleCitySizesId).Contains(x.Id));
                bubbleFilter.selectedNumOfEmployees = bubbleFilter.numOfEmployees.Where(x => EnumMapper.GetCollectionFromMappedId<NumberOfEmployeesId>(bubbleNumOfEmployeesId).Contains(x.Id));
                bubbleFilter.selectedSectors = bubbleFilter.sectors.Where(x => EnumMapper.GetCollectionFromMappedId<SectorId>(bubbleSectorsId).Contains(x.Id));
                bubbleFilter.selectedServiceAreas = bubbleFilter.serviceAreas.Where(x => EnumMapper.GetCollectionFromMappedId<ServiceAreaId>(bubbleServiceAreasId).Contains(x.Id));
                bubbleFilter.selectedOperationTimes = bubbleFilter.operationTimes.Where(x => EnumMapper.GetCollectionFromMappedId<OperationTimeId>(bubbleOperationTimesId).Contains(x.Id));
                bubbleFilter.selectedTurnovers = bubbleFilter.turnovers.Where(x => EnumMapper.GetCollectionFromMappedId<TurnoverId>(bubbleTurnoversId).Contains(x.Id));
                bubbleFilter.selectedEnterpriseRoles = bubbleFilter.enterpriseRoles.Where(x => EnumMapper.GetCollectionFromMappedId<EnterpriseRoleId>(bubbleEnterpriseRolesId).Contains(x.Id));
            }
            else
            {
                bubbleFilter.SelectedSubmit = submits.LastOrDefault();

            }
            bubbleFilter.selectedSectors = bubbleFilter.sectors.Where(x => x.Id == bubbleFilter.SelectedSubmit.Sector.Id);
        }

        protected void GenerateReportClick()
        {
            dbService.IncreaseGeneratedReportsStats();
            var countriesMappedId = EnumMapper.GetMappedId(filter.selectedCountries.Select(x => x.Id).ToList());
            var citySizesMappedId = EnumMapper.GetMappedId(filter.selectedCitySizes.Select(x => x.Id).ToList());
            var numOfEmployeesMappedId = EnumMapper.GetMappedId(filter.selectedNumOfEmployees.Select(x => x.Id).ToList());
            var sectorsMappedId = EnumMapper.GetMappedId(filter.selectedSectors.Select(x => x.Id).ToList());
            var serviceAreasMappedId = EnumMapper.GetMappedId(filter.selectedServiceAreas.Select(x => x.Id).ToList());
            var operationTimesMappedId = EnumMapper.GetMappedId(filter.selectedOperationTimes.Select(x => x.Id).ToList());
            var turnoversTimesMappedId = EnumMapper.GetMappedId(filter.selectedTurnovers.Select(x => x.Id).ToList());
            var enterpriseRolesMappedId = EnumMapper.GetMappedId(filter.selectedEnterpriseRoles.Select(x => x.Id).ToList());
            var answers1Id = string.Join('-', filter.selectedAnswers1.Select(x => x.Id));
            answers1Id = string.IsNullOrEmpty(answers1Id) ? "-" : answers1Id;
            var answers2Id = string.Join('-', filter.selectedAnswers2.Select(x => x.Id));
            answers2Id = string.IsNullOrEmpty(answers2Id) ? "-" : answers2Id;
            var answers3Id = string.Join('-', filter.selectedAnswers3.Select(x => x.Id));
            answers3Id = string.IsNullOrEmpty(answers3Id) ? "-" : answers3Id;
            var bubbleCountriesMappedId = EnumMapper.GetMappedId(bubbleFilter.selectedCountries.Select(x => x.Id).ToList());
            var bubbleCitySizesMappedId = EnumMapper.GetMappedId(bubbleFilter.selectedCitySizes.Select(x => x.Id).ToList());
            var bubbleNumOfEmployeesMappedId = EnumMapper.GetMappedId(bubbleFilter.selectedNumOfEmployees.Select(x => x.Id).ToList());
            var bubbleSectorsMappedId = EnumMapper.GetMappedId(bubbleFilter.selectedSectors.Select(x => x.Id).ToList());
            var bubbleServiceAreasMappedId = EnumMapper.GetMappedId(bubbleFilter.selectedServiceAreas.Select(x => x.Id).ToList());
            var bubbleOperationTimesMappedId = EnumMapper.GetMappedId(bubbleFilter.selectedOperationTimes.Select(x => x.Id).ToList());
            var bubbleTurnoversTimesMappedId = EnumMapper.GetMappedId(bubbleFilter.selectedTurnovers.Select(x => x.Id).ToList());
            var bubbleEnterpriseRolesMappedId = EnumMapper.GetMappedId(bubbleFilter.selectedEnterpriseRoles.Select(x => x.Id).ToList());
            NavigationManager.NavigateTo(@$"/report/{filter.SelectedSubmit.Id}/{countriesMappedId}/{citySizesMappedId}/{numOfEmployeesMappedId}/{sectorsMappedId}/{serviceAreasMappedId}/{operationTimesMappedId}/{turnoversTimesMappedId}/{enterpriseRolesMappedId}/{answers1Id}/{answers2Id}/{answers3Id}/{bubbleCountriesMappedId}/{bubbleCitySizesMappedId}/{bubbleNumOfEmployeesMappedId}/{bubbleSectorsMappedId}/{bubbleServiceAreasMappedId}/{bubbleOperationTimesMappedId}/{bubbleTurnoversTimesMappedId}/{bubbleEnterpriseRolesMappedId}");
        }

        private bool isRendered = false;
        private int spinnersCount = 15;
        protected void OnSpinnerRendered(PreRenderLoadingMessage sender)
        {
            lock(this)
            {
                spinnersCount--;
                if (spinnersCount == 0)
                {
                    isRendered = true;
                }
            }
        }
    }
}
