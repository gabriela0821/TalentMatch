using AutoMapper;
using TalentMatch.Core.DTOs.Application.Request;
using TalentMatch.Core.DTOs.Application.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class ApplicationProfile : Profile
    {
        public ApplicationProfile()
        {
            #region RequestApplication

            CreateMap<CreateApplicationDtoRequest, Application>();
            CreateMap<UpdateApplicationDtoRequest, Application>();

            #endregion RequestApplication

            CreateMap<Application, GetApplicationDtoResponse>();
        }
    }
}