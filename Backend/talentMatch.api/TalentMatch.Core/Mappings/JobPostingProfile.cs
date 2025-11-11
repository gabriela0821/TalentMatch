using AutoMapper;
using TalentMatch.Core.DTOs.JobPosting.Request;
using TalentMatch.Core.DTOs.JobPosting.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class JobPostingProfile : Profile
    {
        public JobPostingProfile()
        {
            #region RequestJobPosting

            CreateMap<CreateJobPostingDtoRequest, JobPosting>();
            CreateMap<UpdateJobPostingDtoRequest, JobPosting>();

            #endregion RequestJobPosting

            CreateMap<JobPosting, GetJobPostingDtoResponse>();
        }
    }
}