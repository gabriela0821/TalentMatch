using AutoMapper;
using TalentMatch.Core.DTOs.WorkExperience.Request;
using TalentMatch.Core.DTOs.WorkExperience.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class WorkExperienceProfile : Profile
    {
        public WorkExperienceProfile()
        {
            #region RequestWorkExperience

            CreateMap<CreateWorkExperienceDtoRequest, WorkExperience>();
            CreateMap<UpdateWorkExperienceDtoRequest, WorkExperience>();

            #endregion RequestWorkExperience

            CreateMap<WorkExperience, GetWorkExperienceDtoResponse>();
        }
    }
}