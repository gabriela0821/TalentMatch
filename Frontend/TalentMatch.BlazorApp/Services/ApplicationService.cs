using Blazored.LocalStorage;
using System.Net.Http.Headers;
using TalentMatch.BlazorApp.Models;
using TalentMatch.BlazorApp.Models.DTOs.Application.Request;
using TalentMatch.BlazorApp.Models.DTOs.Application.Response;
using TalentMatch.BlazorApp.Models.DTOs.EmployerProfile.Response;
using TalentMatch.BlazorApp.Models.DTOs.JobPosting.Response;
using TalentMatch.BlazorApp.Models.Interfaces.Services;

namespace TalentMatch.BlazorApp.Services
{
    public class ApplicationService: IApplicationService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public ApplicationService(HttpClient http, ILocalStorageService localStorage)
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

        public async Task<Response<GetApplicationDtoResponse?>> CreateApplication(CreateApplicationDtoRequest create)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("ApplicationsService/CreateApplication", create);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetApplicationDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<Response<GetApplicationDtoResponse?>> GetApplicationByJobId(int jobId)
        {
            await SetAuthHeaderAsync();
            try
            {
                var response = await _http.GetFromJsonAsync<Response<GetApplicationDtoResponse?>>($"ApplicationsService/GetApplicationByJobId?jobId={jobId}");
                return response ?? new Response<GetApplicationDtoResponse?> { Succeeded = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new Response<GetApplicationDtoResponse?> { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<Response<GetApplicationDtoResponse?>> GetApplicationByUserId(int userId)
        {
            await SetAuthHeaderAsync();
            try
            {
                var response = await _http.GetFromJsonAsync<Response<GetApplicationDtoResponse?>>($"ApplicationsService/GetApplicationByUserId?userId={userId}");
                return response ?? new Response<GetApplicationDtoResponse?> { Succeeded = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new Response<GetApplicationDtoResponse?> { Succeeded = false, Message = ex.Message };
            }
        }

    }
}
