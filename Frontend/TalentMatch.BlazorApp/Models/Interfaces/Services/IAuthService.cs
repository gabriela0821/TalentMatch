using TalentMatch.BlazorApp.Models.DTOs;
using TalentMatch.BlazorApp.Models.DTOs.User.Request;
using TalentMatch.BlazorApp.Models.DTOs.User.Response;

namespace TalentMatch.BlazorApp.Models.Interfaces.Services
{
    public interface IAuthService
    {
        Task<Response<GetUserDtoResponse?>> LoginUserAsync(LoginUserDtoRequest createImport);

        Task<Response<GetUserDtoResponse?>> RegisterUserAsync(CreateUserDtoRequest createImport);
        Task LogoutAsync();
        Task<string?> GetTokenAsync();
    }
}
