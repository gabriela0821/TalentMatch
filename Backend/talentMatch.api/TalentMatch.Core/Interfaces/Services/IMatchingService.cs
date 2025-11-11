using TalentMatch.Core.DTOs.JobMatch.Request;
using TalentMatch.Core.DTOs.JobMatch.Response;
using TalentMatch.Core.Wrappers;
using TalentMatch.Domain.QueryFilters;

namespace TalentMatch.Core.Interfaces.Services
{
    public interface IMatchingService
    {
        Task<Response<GetJobMatchDtoResponse?>> CreateMatch(CreateJobMatchDtoRequest create);

        Task<Response<PaginationResponse<GetJobMatchDtoResponse?>>> GetMatches(MatchesQueryFilter filter);

        Task<Response<bool>> UpdateMatchStatus(UpdateJobMatchDtoRequest update);
    }
}