using System.ComponentModel.DataAnnotations;

namespace QuestApp.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
     
        [Required] public string Email { get; set; }
        public string Role { get; set; }
        [Required] public string Password { get; set; }
        [Required] public string Name { get; set; }
        [Required] public EnumIdText<CountryId> Country { get; set; } 
        [Required] public EnumIdText<CitySizeId> CitySize { get; set; } 
        [Required] public EnumIdText<NumberOfEmployeesId> NumberOfEmployees { get; set; } 
        [Required] public EnumIdText<SectorId> Sector { get; set; } 
        [Required] public EnumIdText<ServiceAreaId> ServiceArea { get; set; } 
        [Required] public EnumIdText<OperationTimeId> OperationTime { get; set; } 
        [Required] public EnumIdText<TurnoverId> Turnover { get; set; } 
        [Required] public EnumIdText<EnterpriseRoleId> EnterpriseRole { get; set; } 

        public string ConfirmPassword { get; set; }
    }
}
