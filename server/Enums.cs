namespace QuestApp
{
    public enum CountryId
    {
        CountryId_PL = 1,
        CountryId_GB = 2,
        CountryId_DE = 3,
        CountryId_NL = 4,
        CountryId_Other = 99,
    }

    public enum CitySizeId
    {
        CitySizeId_Below10k = 1,
        CitySizeId_Between10_50k = 2,
        CitySizeId_Between50_200k = 3,
        CitySizeId_Between200_500k = 4,
        CitySizeId_Between500_1000k = 5,
        CitySizeId_Over1000k = 6,
    }

    public enum NumberOfEmployeesId
    {
        NumberOfEmployeesId_0_1 = 1,
        NumberOfEmployeesId_2_9 = 2,
        NumberOfEmployeesId_10_19 = 3,
        NumberOfEmployeesId_20_49 = 4,
        NumberOfEmployeesId_50_99 = 5,
        NumberOfEmployeesId_100_249 = 6,
        NumberOfEmployeesId_250AndMore = 7,
    }

    public enum SectorId
    {
        //SectorId_AgricultureForestryAndFishing = 1,
        //SectorId_MiningAndQuarrying = 2,
        SectorId_Manufacturing = 3,
        SectorId_ElectricityGasSteamAndAirConditioningSupply = 4,
        //SectorId_WaterSupply = 5,
        SectorId_Construction = 6,
        //SectorId_WholesaleAndRetailTrade = 7,
        SectorId_TransportAndStorage = 8,
        SectorId_AccommodationAndFoodService = 9,
        SectorId_InformationAndCommunication = 10,
        SectorId_FinanceAndInsurance = 11,
        SectorId_RealEstate = 12,
        SectorId_ProfessionalScientificAndTechnical = 13,
        //SectorId_AdministrativeAndSupportService = 14,
        //SectorId_PublicAdministrationAndDefence = 15,
        SectorId_Education = 16,
        //SectorId_HumanHealthAndSocialWork = 17,
        SectorId_ArtsEntertainmentAndRecreational = 18,
        SectorId_Other = 19,
        //SectorId_HouseholdsAsEmployers = 20,
        //SectorId_ExtraterritorialOrganisationsAndBodies = 21,
    }

    public enum ServiceAreaId
    {
        //ServiceAreaId_ElectricityGasSteamAndAirConditioningSupply = 4,
        //ServiceAreaId_WaterSupply = 5,
        ServiceAreaId_Construction = 6,
        ServiceAreaId_TransportAndStorage = 8,
        ServiceAreaId_AccommodationAndFoodService = 9,
        ServiceAreaId_InformationAndCommunication = 10,
        ServiceAreaId_FinanceAndInsurance = 11,
        ServiceAreaId_RealEstate = 12,
        ServiceAreaId_ProfessionalScientificAndTechnical = 13,
        ServiceAreaId_Education = 16,
        //ServiceAreaId_HumanHealthAndSocialWork = 17,
        ServiceAreaId_ArtsEntertainmentAndRecreational = 18,
        ServiceAreaId_Other = 19,
        ServiceAreaId_None = 22,
    }

    public enum OperationTimeId
    {
        OperationTimeId_0_1 = 1,
        OperationTimeId_2_7 = 2,
        OperationTimeId_8_20 = 3,
        OperationTimeId_21_99 = 4,
        OperationTimeId_100AndMore = 5,
    }

    public enum TurnoverId
    {
        TurnoverId_LessThan2 = 1,
        TurnoverId_2_10 = 2,
        TurnoverId_10_50 = 3,
        TurnoverId_Over50 = 4,
    }

    public enum EnterpriseRoleId
    {
        EnterpriseRoleId_OwnerManager = 1,
        EnterpriseRoleId_CoOwnerExecutiveManager = 2,
        EnterpriseRoleId_ExecutiveManager = 3,
        EnterpriseRoleId_DepartmentManager = 4,
        EnterpriseRoleId_Other = 5,
    }

    public enum ReportTypeId
    {
        IPE = 0,
        IPDC = 1,
        IPDI = 2,
        Blank = 3,
        Ratio = 4,
    }
}
