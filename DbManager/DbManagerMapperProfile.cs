using AutoMapper;
using DB.Models;
using DbManager.DTO;

namespace DbManager
{
    internal class DbManagerMapperProfile : Profile
    {
        public DbManagerMapperProfile()
        {
            CreateMap<Questionnaire, QuestionnaireDTO>();
            CreateMap<Section, SectionDTO>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<Question, QuestionDTO>();
            CreateMap<Answer, AnswerDTO>();

            CreateMap<QuestionnaireDTO, Questionnaire>();
            CreateMap<SectionDTO, Section>();
            CreateMap<CategoryDTO, Category>();
            CreateMap<QuestionDTO, Question>();
            CreateMap<AnswerDTO, Answer>();
        }
    }
}
