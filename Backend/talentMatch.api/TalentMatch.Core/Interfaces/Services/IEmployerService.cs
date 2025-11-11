using TalentMatch.Core.DTOs.EmployerProfile.Request;
using TalentMatch.Core.DTOs.EmployerProfile.Response;
using TalentMatch.Core.Wrappers;

namespace TalentMatch.Core.Interfaces.Services
{
    public interface IEmployerService
    {
        Task<Response<GetEmployerProfileDtoResponse?>> CreateProfile(CreateEmployerProfileDtoRequest create);

        Task<Response<GetEmployerProfileDtoResponse?>> UpdateProfile(UpdateEmployerProfileDtoRequest update);

        Task<Response<GetEmployerProfileDtoResponse?>> GetProfileById(int userId);
    }
}