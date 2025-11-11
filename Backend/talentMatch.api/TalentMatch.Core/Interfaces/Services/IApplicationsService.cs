using TalentMatch.Core.DTOs.Application.Request;
using TalentMatch.Core.DTOs.Application.Response;
using TalentMatch.Core.Wrappers;

namespace TalentMatch.Core.Interfaces.Services
{
    public interface IApplicationsService
    {
        Task<Response<GetApplicationDtoResponse?>> CreateApplication(CreateApplicationDtoRequest create);

        Task<Response<GetApplicationDtoResponse?>> GetApplicationByJobId(int jobId);

        Task<Response<GetApplicationDtoResponse?>> GetApplicationByUserId(int userId);
    }
}