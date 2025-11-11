using TalentMatch.BlazorApp.Models.DTOs.Application.Request;
using TalentMatch.BlazorApp.Models.DTOs.Application.Response;

namespace TalentMatch.BlazorApp.Models.Interfaces.Services
{
    public interface IApplicationService
    {
        Task<Response<GetApplicationDtoResponse?>> CreateApplication(CreateApplicationDtoRequest create);
        Task<Response<GetApplicationDtoResponse?>> GetApplicationByJobId(int jobId);
        Task<Response<GetApplicationDtoResponse?>> GetApplicationByUserId(int userId);
        
    }
}
