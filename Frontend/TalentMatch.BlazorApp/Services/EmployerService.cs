using Blazored.LocalStorage;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net.Http.Headers;
using TalentMatch.BlazorApp.Models;
using TalentMatch.BlazorApp.Models.DTOs.EmployerProfile.Request;
using TalentMatch.BlazorApp.Models.DTOs.EmployerProfile.Response;
using TalentMatch.BlazorApp.Models.DTOs.JobSeekerProfile.Response;
using TalentMatch.BlazorApp.Models.Interfaces.Services;
using TalentMatch.BlazorApp.Pages.JobSeeker;

namespace TalentMatch.BlazorApp.Services
{
    public class EmployerService: IEmployerService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public EmployerService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

        private async Task SetAuthHeaderAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        public async Task<Response<GetEmployerProfileDtoResponse?>> CreateProfile(CreateEmployerProfileDtoRequest create)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("EmployerService/CreateProfile", create);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetEmployerProfileDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<Response<GetEmployerProfileDtoResponse?>> GetProfileById(int userId)
        {
            await SetAuthHeaderAsync();
            try
            {
                var response = await _http.GetFromJsonAsync<Response<GetEmployerProfileDtoResponse?>>($"EmployerService/Profile?userId={userId}");
                return response ?? new Response<GetEmployerProfileDtoResponse?> { Succeeded = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new Response<GetEmployerProfileDtoResponse?> { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<Response<GetEmployerProfileDtoResponse?>> UpdateProfile(UpdateEmployerProfileDtoRequest update)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("EmployerService/UpdateProfile", update);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetEmployerProfileDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

    }
}
