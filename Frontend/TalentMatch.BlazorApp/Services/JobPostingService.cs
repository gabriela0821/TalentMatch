using Blazored.LocalStorage;
using System.Net.Http.Headers;
using TalentMatch.BlazorApp.Models;
using TalentMatch.BlazorApp.Models.DTOs.ApplicationQuestion.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobPosting.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobPosting.Response;
using TalentMatch.BlazorApp.Models.DTOs.JobSeekerProfile.Response;
using TalentMatch.BlazorApp.Models.Interfaces.Services;
using TalentMatch.BlazorApp.Pages.JobSeeker;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TalentMatch.BlazorApp.Services
{
    public class JobPostingService: IJobPostingService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public JobPostingService(HttpClient http, ILocalStorageService localStorage)
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

        public async Task<Response<GetJobPostingDtoResponse?>> CreateJobPosting(CreateJobPostingDtoRequest create)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("JobPosting/CreateJobPosting", create);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetJobPostingDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<Response<GetJobPostingDtoResponse?>> CreateQuestions(List<CreateApplicationQuestionDtoRequest> questions)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("JobPosting/CreateQuestions", questions);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetJobPostingDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<Response<GetJobPostingDtoResponse?>> GetJobPostingById(int jobId)
        {
            await SetAuthHeaderAsync();
            try
            {
                var response = await _http.GetFromJsonAsync<Response<GetJobPostingDtoResponse?>>($"JobPosting/GetJobPostingById?jobId={jobId}");
                return response ?? new Response<GetJobPostingDtoResponse?> { Succeeded = false };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return new Response<GetJobPostingDtoResponse?> { Succeeded = false, Message = ex.Message };
            }
        }

        public async Task<Response<PaginationResponse<GetJobPostingDtoResponse?>>> GetJobPostings(JobPostingQueryFilter filter)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("JobPosting/GetJobPostings", filter);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<PaginationResponse<GetJobPostingDtoResponse>>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

    }
}
