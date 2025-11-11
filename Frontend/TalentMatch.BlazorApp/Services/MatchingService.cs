using Blazored.LocalStorage;
using System.Net.Http.Headers;
using TalentMatch.BlazorApp.Models;
using TalentMatch.BlazorApp.Models.DTOs.JobMatch.Request;
using TalentMatch.BlazorApp.Models.DTOs.JobMatch.Response;
using TalentMatch.BlazorApp.Models.DTOs.JobSeekerProfile.Response;
using TalentMatch.BlazorApp.Models.Interfaces.Services;
using TalentMatch.BlazorApp.Pages.JobSeeker;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TalentMatch.BlazorApp.Services
{
    public class MatchingService:IMatchingService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public MatchingService(HttpClient http, ILocalStorageService localStorage)
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

        public async Task<Response<GetJobMatchDtoResponse?>> CreateMatch(CreateJobMatchDtoRequest create)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("MatchingService/CreateMatch", create);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<GetJobMatchDtoResponse>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<Response<PaginationResponse<GetJobMatchDtoResponse?>>> GetMatches(MatchesQueryFilter filter)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("MatchingService/GetMatches", filter);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<PaginationResponse<GetJobMatchDtoResponse?>>> ();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }

        public async Task<Response<bool>> UpdateMatchStatus(UpdateJobMatchDtoRequest update)
        {
            await SetAuthHeaderAsync();
            var response = await _http.PostAsJsonAsync("MatchingService/UpdateMatchStatus", update);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<bool>>();
                if (result?.Succeeded == true && result.Data != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}
