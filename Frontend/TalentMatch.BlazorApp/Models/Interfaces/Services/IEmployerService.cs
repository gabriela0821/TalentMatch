using TalentMatch.BlazorApp.Models.DTOs.EmployerProfile.Request;
using TalentMatch.BlazorApp.Models.DTOs.EmployerProfile.Response;

namespace TalentMatch.BlazorApp.Models.Interfaces.Services
{
    public interface IEmployerService
    {
        Task<Response<GetEmployerProfileDtoResponse?>> CreateProfile(CreateEmployerProfileDtoRequest create);

        Task<Response<GetEmployerProfileDtoResponse?>> UpdateProfile(UpdateEmployerProfileDtoRequest update);

        Task<Response<GetEmployerProfileDtoResponse?>> GetProfileById(int userId);
    }
}
