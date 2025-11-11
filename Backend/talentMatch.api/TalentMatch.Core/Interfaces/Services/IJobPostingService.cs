using TalentMatch.Core.DTOs.ApplicationQuestion.Request;
using TalentMatch.Core.DTOs.JobPosting.Request;
using TalentMatch.Core.DTOs.JobPosting.Response;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.QueryFilters;

namespace TalentMatch.Core.Interfaces.Services
{
    public interface IJobPostingService
    {
        Task<Response<GetJobPostingDtoResponse?>> CreateJobPosting(CreateJobPostingDtoRequest create);

        Task<Response<GetJobPostingDtoResponse?>> CreateQuestions(List<CreateApplicationQuestionDtoRequest> questions);

        Task<Response<GetJobPostingDtoResponse?>> GetJobPostingById(int jobId);

        Task<Response<PaginationResponse<GetJobPostingDtoResponse?>>> GetJobPostings(JobPostingQueryFilter filter);
    }
}