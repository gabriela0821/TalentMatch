namespace TalentMatch.BlazorApp.Providers
{
    using Blazored.LocalStorage;
    using Microsoft.AspNetCore.Components.Authorization;
    using System.Security.Claims;

    namespace TalentMatch.BlazorApp.Providers
    {
        public class CustomAuthStateProvider : AuthenticationStateProvider
        {
            private readonly ILocalStorageService _localStorage;

            public CustomAuthStateProvider(ILocalStorageService localStorage)
            {
                _localStorage = localStorage;
            }

            public override async Task<AuthenticationState> GetAuthenticationStateAsync()
            {
                var token = await _localStorage.GetItemAsync<string>("authToken");

                if (string.IsNullOrEmpty(token))
                {
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }

                var userType = await _localStorage.GetItemAsync<string>("userType");
                var userId = await _localStorage.GetItemAsync<int>("userId");

                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userId.ToString()),
                new Claim(ClaimTypes.Role, userType ?? "JobSeeker")
            };

                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                return new AuthenticationState(user);
            }

            public void NotifyUserAuthentication(string userType)
            {
                var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, userType)
            };

                var identity = new ClaimsIdentity(claims, "jwt");
                var user = new ClaimsPrincipal(identity);

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            }

            public void NotifyUserLogout()
            {
                var identity = new ClaimsIdentity();
                var user = new ClaimsPrincipal(identity);

                NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
            }
        }
    }
}
