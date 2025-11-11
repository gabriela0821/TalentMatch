using TalentMatch.Core.DTOs.User.Request;
using TalentMatch.Core.DTOs.User.Response;
using TalentMatch.Core.Wrappers;

namespace TalentMatch.Core.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Response<GetUserDtoResponse?>> LoginUserAsync(LoginUserDtoRequest createImport);

        Task<Response<GetUserDtoResponse?>> RegisterUserAsync(CreateUserDtoRequest createImport);
    }
}