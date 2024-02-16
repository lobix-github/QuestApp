using AutoMapper;
using DB.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using QuestApp.DbContexts;
using QuestApp.DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace QuestApp
{
    public class MyAutoMapperProfile : Profile
    {
        public MyAutoMapperProfile()
        {
            CreateMap<UserDTO, User>();
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Country, s => s.MapFrom(x => new EnumIdText<CountryId>() { Id = (CountryId)x.CountryId }))
                .ForMember(x => x.CitySize, s => s.MapFrom(x => new EnumIdText<CitySizeId>() { Id = (CitySizeId)x.CitySizeId }))
                .ForMember(x => x.NumberOfEmployees, s => s.MapFrom(x => new EnumIdText<NumberOfEmployeesId>() { Id = (NumberOfEmployeesId)x.NumberOfEmployeesId }))
                .ForMember(x => x.Sector, s => s.MapFrom(x => new EnumIdText<SectorId>() { Id = (SectorId)x.SectorId }))
                .ForMember(x => x.ServiceArea, s => s.MapFrom(x => new EnumIdText<ServiceAreaId>() { Id = (ServiceAreaId)x.ServiceAreaId }))
                .ForMember(x => x.OperationTime, s => s.MapFrom(x => new EnumIdText<OperationTimeId>() { Id = (OperationTimeId)x.OperationTimeId }))
                .ForMember(x => x.Turnover, s => s.MapFrom(x => new EnumIdText<TurnoverId>() { Id = (TurnoverId)x.TurnoverId }))
                .ForMember(x => x.EnterpriseRole, s => s.MapFrom(x => new EnumIdText<EnterpriseRoleId>() { Id = (EnterpriseRoleId)x.EnterpriseRoleId }));
            CreateMap<User, Submit>();

            CreateMap<Submit, SubmitDTO>()
                .ForMember(x => x.Country, s => s.MapFrom(x => new EnumIdText<CountryId>() { Id = (CountryId)x.CountryId }))
                .ForMember(x => x.CitySize, s => s.MapFrom(x => new EnumIdText<CitySizeId>() { Id = (CitySizeId)x.CitySizeId }))
                .ForMember(x => x.NumberOfEmployees, s => s.MapFrom(x => new EnumIdText<NumberOfEmployeesId>() { Id = (NumberOfEmployeesId)x.NumberOfEmployeesId }))
                .ForMember(x => x.Sector, s => s.MapFrom(x => new EnumIdText<SectorId>() { Id = (SectorId)x.SectorId }))
                .ForMember(x => x.ServiceArea, s => s.MapFrom(x => new EnumIdText<ServiceAreaId>() { Id = (ServiceAreaId)x.ServiceAreaId }))
                .ForMember(x => x.OperationTime, s => s.MapFrom(x => new EnumIdText<OperationTimeId>() { Id = (OperationTimeId)x.OperationTimeId }))
                .ForMember(x => x.Turnover, s => s.MapFrom(x => new EnumIdText<TurnoverId>() { Id = (TurnoverId)x.TurnoverId }))
                .ForMember(x => x.EnterpriseRole, s => s.MapFrom(x => new EnumIdText<EnterpriseRoleId>() { Id = (EnterpriseRoleId)x.EnterpriseRoleId }));

            CreateMap<Questionnaire, QuestionnaireDTO>()
                .ForMember(x => x.Description, s => s.MapFrom($"Description_{Extensions.ShortUICultureNameFromCurrent}"));
            CreateMap<Section, SectionDTO>()
                .ForMember(x => x.Title, s => s.MapFrom($"Title_{Extensions.ShortUICultureNameFromCurrent}"))
                .ForMember(x => x.Text, s => s.MapFrom($"Text_{Extensions.ShortUICultureNameFromCurrent}"))
                .ForMember(x => x.Comment, s => s.MapFrom($"Comment_{Extensions.ShortUICultureNameFromCurrent}"));
            CreateMap<Category, CategoryDTO>()
                .ForMember(x => x.Title, s => s.MapFrom($"Title_{Extensions.ShortUICultureNameFromCurrent}"))
                .ForMember(x => x.Text, s => s.MapFrom($"Text_{Extensions.ShortUICultureNameFromCurrent}"))
                .ForMember(x => x.Comment, s => s.MapFrom($"Comment_{Extensions.ShortUICultureNameFromCurrent}"))
                .ForMember(x => x.Prequestion, s => s.MapFrom($"Prequestion_{Extensions.ShortUICultureNameFromCurrent}"))
                .ForMember(x => x.PrequestionComment, s => s.MapFrom($"PrequestionComment_{Extensions.ShortUICultureNameFromCurrent}"));
            CreateMap<Question, QuestionDTO>()
                .ForMember(x => x.Text, s => s.MapFrom($"Text_{Extensions.ShortUICultureNameFromCurrent}"))
                .ForMember(x => x.Comment, s => s.MapFrom($"Comment_{Extensions.ShortUICultureNameFromCurrent}"));
            CreateMap<Answer, AnswerDTO>()
                .ForMember(x => x.Text, s => s.MapFrom($"Text_{Extensions.ShortUICultureNameFromCurrent}"))
                .ForMember(x => x.Comment, s => s.MapFrom($"Comment_{Extensions.ShortUICultureNameFromCurrent}"));

            CreateMap<Stats, StatsDTO>();
        }
    }

    public static class Extensions
    {
        private const string DEFAULT_CULTURE = "pl";

        public static Dictionary<string, string> CULTURES = null;

        public static string ShortUICultureNameFromCurrent
        { 
            get
            {
                var name = System.Threading.Thread.CurrentThread.CurrentUICulture.Name;
                var shortName = name.Substring(0, name.IndexOf('-') == -1 ? name.Length : name.IndexOf('-'));
                return CULTURES.Keys.Contains(shortName.ToLower()) ? shortName : DEFAULT_CULTURE;
            }
        }

        public static async Task<User> GetUserAsync(this AuthenticationStateProvider authProvider, QuestDbContext db)
        {
            var auth = await authProvider.GetAuthenticationStateAsync();
            return db.Users.FirstOrDefault(x => x.Email == auth.User.Identity.Name);
        }

        public static TDest Map<TDest>(this object source)
        {
            return mapper.Map<TDest>(source);
        }

        public static TDest Map<TDest>(this object source, TDest destination)
        {
            return mapper.Map(source, destination);
        }

        private static IMapper mapper => new MapperConfiguration(cfg => cfg.AddProfile<MyAutoMapperProfile>()).CreateMapper();

        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            email = Regex.Replace(email, @"(@)(.+)$", DomainMapper, RegexOptions.None, TimeSpan.FromMilliseconds(200));

            string DomainMapper(Match match)
            {
                var idn = new IdnMapping();
                string domainName = idn.GetAscii(match.Groups[2].Value);

                return match.Groups[1].Value + domainName;
            }

            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
        }

        public static double AverageOrZero(this IEnumerable<double> source) => source.Any() ? source.Average() : 0;

        public static string ToListString<T>(this IEnumerable<EnumIdText<T>> val) => string.Join("<br/>", val.Select(x => x.Text));
    }
}
