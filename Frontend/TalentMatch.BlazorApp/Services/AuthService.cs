using Blazored.LocalStorage;
using System.Net.Http.Json;
using TalentMatch.BlazorApp.Models;
using TalentMatch.BlazorApp.Models.DTOs;
using TalentMatch.BlazorApp.Models.DTOs.User.Request;
using TalentMatch.BlazorApp.Models.DTOs.User.Response;
using TalentMatch.BlazorApp.Models.Interfaces.Services;

namespace TalentMatch.BlazorApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        public async Task<Response<GetUserDtoResponse?>> LoginUserAsync(LoginUserDtoRequest request)
        {
            var response = await _http.PostAsJsonAsync("Auth/LoginUser", request);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetUserDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    await _localStorage.SetItemAsync("authToken", result.Data.Token);
                    await _localStorage.SetItemAsync("userType", result.Data.UserType);
                    await _localStorage.SetItemAsync("userId", result.Data.UserId);
                    return result;
                }
            }
            return null;
        }

        public async Task<Response<GetUserDtoResponse?>> RegisterUserAsync(CreateUserDtoRequest request)
        {
            try
            {
                var response = await _http.PostAsJsonAsync("Auth/CreateUser", request);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<Response<GetUserDtoResponse>>();
                    if (result?.Succeeded == true && result.Data != null)
                    {
                        return result;
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public async Task LogoutAsync()
        {
            await _localStorage.RemoveItemAsync("authToken");
            await _localStorage.RemoveItemAsync("userType");
            await _localStorage.RemoveItemAsync("userId");
        }

        public async Task<string?> GetTokenAsync()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}