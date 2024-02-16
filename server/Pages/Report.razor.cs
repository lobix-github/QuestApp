using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using Microsoft.JSInterop;
using QuestApp.DbContexts;
using QuestApp.DTO;
using QuestApp.Misc;
using QuestApp.Services;
using QuestApp.Shared;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuestApp.Pages
{
    public partial class Report
    {
        [Inject] protected QuestDbContext db { get; set; }
        [Inject] protected IJSRuntime JS { get; set; }
        [Inject] protected NavigationManager NavigationManager { get; set; }
        [Inject] protected IStringLocalizer<Report> ReportsL { get; set; }
        [Inject] protected IStringLocalizer<ResultsPage> L { get; set; }
        [Inject] protected IStringLocalizer<SharedRes> SharedResL { get; set; }
        [Inject] protected IStringLocalizer<Register> FilterL { get; set; }
        [Inject] protected DBService dbService { get; set; }

        [Parameter] public int submitId { get; set; }
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

        private ResultsPageFilter filter = new ResultsPageFilter();
        private ResultsPageFilter bubbleFilter = new ResultsPageFilter();

        protected override async Task OnInitializedAsync()
        {
            var submits = await dbService.GetUserSubmitsAsync();
            filter.Init(SharedResL, dbService);
            filter.SelectedSubmit = submits.Single(x => x.Id == submitId);
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

            bubbleFilter.Init(SharedResL, dbService);
            bubbleFilter.SelectedSubmit = submits.Single(x => x.Id == submitId);
            bubbleFilter.selectedCountries = bubbleFilter.countries.Where(x => EnumMapper.GetCollectionFromMappedId<CountryId>(bubbleCountriesId).Contains(x.Id));
            bubbleFilter.selectedCitySizes = bubbleFilter.citySizes.Where(x => EnumMapper.GetCollectionFromMappedId<CitySizeId>(bubbleCitySizesId).Contains(x.Id));
            bubbleFilter.selectedNumOfEmployees = bubbleFilter.numOfEmployees.Where(x => EnumMapper.GetCollectionFromMappedId<NumberOfEmployeesId>(bubbleNumOfEmployeesId).Contains(x.Id));
            bubbleFilter.selectedSectors = bubbleFilter.sectors.Where(x => EnumMapper.GetCollectionFromMappedId<SectorId>(bubbleSectorsId).Contains(x.Id));
            bubbleFilter.selectedServiceAreas = bubbleFilter.serviceAreas.Where(x => EnumMapper.GetCollectionFromMappedId<ServiceAreaId>(bubbleServiceAreasId).Contains(x.Id));
            bubbleFilter.selectedOperationTimes = bubbleFilter.operationTimes.Where(x => EnumMapper.GetCollectionFromMappedId<OperationTimeId>(bubbleOperationTimesId).Contains(x.Id));
            bubbleFilter.selectedTurnovers = bubbleFilter.turnovers.Where(x => EnumMapper.GetCollectionFromMappedId<TurnoverId>(bubbleTurnoversId).Contains(x.Id));
            bubbleFilter.selectedEnterpriseRoles = bubbleFilter.enterpriseRoles.Where(x => EnumMapper.GetCollectionFromMappedId<EnterpriseRoleId>(bubbleEnterpriseRolesId).Contains(x.Id));
        }

        private bool isRendered = false;
        private int spinnersCount = 15;
        protected void OnSpinnerRendered(PreRenderLoadingMessage sender)
        {
            lock (this)
            {
                spinnersCount--;
            }

            if (spinnersCount == 0)
            {
                isRendered = true;
            }
        }
    }
}
