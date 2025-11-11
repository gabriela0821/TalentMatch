using AutoMapper;
using TalentMatch.Core.DTOs.ApplicationAnswer.Request;
using TalentMatch.Core.DTOs.ApplicationAnswer.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class ApplicationAnswerProfile : Profile
    {
        public ApplicationAnswerProfile()
        {
            #region RequestApplicationAnswer

            CreateMap<CreateApplicationAnswerDtoRequest, ApplicationAnswer>();
            CreateMap<UpdateApplicationAnswerDtoRequest, ApplicationAnswer>();

            #endregion RequestApplicationAnswer

            CreateMap<ApplicationAnswer, GetApplicationAnswerDtoResponse>();
        }
    }
}