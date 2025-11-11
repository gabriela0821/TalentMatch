using TalentMatch.BlazorApp.Models.DTOs.ApplicationQuestion.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobPosting.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobPosting.Response;

namespace TalentMatch.BlazorApp.Models.Interfaces.Services
{
    public interface IJobPostingService
    {
        Task<Response<GetJobPostingDtoResponse?>> CreateJobPosting(CreateJobPostingDtoRequest create);

        Task<Response<GetJobPostingDtoResponse?>> CreateQuestions(List<CreateApplicationQuestionDtoRequest> questions);

        Task<Response<GetJobPostingDtoResponse?>> GetJobPostingById(int jobId);

        Task<Response<PaginationResponse<GetJobPostingDtoResponse?>>> GetJobPostings(JobPostingQueryFilter filter);
    }
}
