using AutoMapper;
using TalentMatch.Core.DTOs.JobMatch.Request;
using TalentMatch.Core.DTOs.JobMatch.Response;
using TalentMatch.Domain.Entities;

namespace TalentMatch.Core.Mappings
{
    public class JobMatchProfile : Profile
    {
        public JobMatchProfile()
        {
            #region RequestJobMatch

            CreateMap<CreateJobMatchDtoRequest, JobMatch>();
            CreateMap<UpdateJobMatchDtoRequest, JobMatch>();

            #endregion RequestJobMatch

            CreateMap<JobMatch, GetJobMatchDtoResponse>()
                .ForMember(jobMatch => jobMatch.FullNameJobSeeker, d => d.MapFrom(jobMatch => jobMatch.JobSeekerProfile.FullName))
                .ForMember(jobMatch => jobMatch.TitleJobPosting, d => d.MapFrom(jobMatch => jobMatch.JobPosting.Title));
        }
    }
}