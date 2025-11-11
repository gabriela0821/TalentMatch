using Blazored.LocalStorage;
using System.Net.Http.Headers;
using TalentMatch.BlazorApp.Models;
using TalentMatch.BlazorApp.Models.DTOs.Certification.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobSeekerProfile.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobSeekerProfile.Response;
using TalentMatch.BlazorApp.Models.DTOs.User.Response;
using TalentMatch.BlazorApp.Models.DTOs.WorkExperience.Request;
using TalentMatch.BlazorApp.Models.Interfaces.Services;
using TalentMatch.BlazorApp.Pages.JobSeeker;

namespace TalentMatch.BlazorApp.Services
{
    public class JobSeekerService:IJobSeekerService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public JobSeekerService(HttpClient http, ILocalStorageService localStorage)
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

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> GetProfileById(int userId)
        {
            await SetAuthHeaderAsync();
            try
            {
                var response = await _http.GetFromJsonAsync<Response<GetJobSeekerProfileDtoResponse?>>($"JobSeeker/Profile?userId={userId}");
                return response ?? new Response<GetJobSeekerProfileDtoResponse?> { Succeeded = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new Response<GetJobSeekerProfileDtoResponse?> { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> CreateProfile(CreateJobSeekerProfileDtoRequest profile)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("JobSeeker/CreateProfile", profile);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetJobSeekerProfileDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> UpdateProfile(UpdateJobSeekerProfileDtoRequest profile)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("JobSeeker/UpdateProfile", profile);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetJobSeekerProfileDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> CreateExperience(CreateWorkExperienceDtoRequest create)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("JobSeeker/CreateExperience", create);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetJobSeekerProfileDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<Response<GetJobSeekerProfileDtoResponse?>> CreateCertification(CreateCertificationDtoRequest create)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("JobSeeker/CreateCertification", create);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetJobSeekerProfileDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
