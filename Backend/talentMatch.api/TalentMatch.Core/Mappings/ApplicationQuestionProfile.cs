using AutoMapper;
using TalentMatch.Core.DTOs.ApplicationQuestion.Request;
using TalentMatch.Core.DTOs.ApplicationQuestion.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class ApplicationQuestionProfile : Profile
    {
        public ApplicationQuestionProfile()
        {
            #region RequestApplicationQuestion

            CreateMap<CreateApplicationQuestionDtoRequest, ApplicationQuestion>();
            CreateMap<UpdateApplicationQuestionDtoRequest, ApplicationQuestion>();

            #endregion RequestApplicationQuestion

            CreateMap<ApplicationQuestion, GetApplicationQuestionDtoResponse>();
        }
    }
}