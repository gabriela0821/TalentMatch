using AutoMapper;
using TalentMatch.Core.DTOs.JobSeekerProfile.Request;
using TalentMatch.Core.DTOs.JobSeekerProfile.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class JobSeekerProfileProfile : Profile
    {
        public JobSeekerProfileProfile()
        {
            #region RequestJobSeekerProfile

            CreateMap<CreateJobSeekerProfileDtoRequest, JobSeekerProfile>();
            CreateMap<UpdateJobSeekerProfileDtoRequest, JobSeekerProfile>();

            #endregion RequestJobSeekerProfile

            CreateMap<JobSeekerProfile, GetJobSeekerProfileDtoResponse>();
        }
    }
}