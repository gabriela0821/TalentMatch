using TalentMatch.BlazorApp.Models.DTOs.JobMatch.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobMatch.Response;

namespace TalentMatch.BlazorApp.Models.Interfaces.Services
{
    public interface IMatchingService
    {
        Task<Response<GetJobMatchDtoResponse?>> CreateMatch(CreateJobMatchDtoRequest create);

        Task<Response<PaginationResponse<GetJobMatchDtoResponse?>>> GetMatches(MatchesQueryFilter filter);

        Task<Response<bool>> UpdateMatchStatus(UpdateJobMatchDtoRequest update);
    }
}
