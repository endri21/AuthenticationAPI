using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eProtokoll.Core.Authentication
{
    public class AuthStateProvider : AuthenticationStateProvider
    {
        private readonly HttpClient httpClient;
        private readonly ILocalStorageService localStorage;
        private readonly AuthenticationState anonymous;

        public AuthStateProvider(HttpClient httpClient , ILocalStorageService localStorage )
        {
            this.httpClient = httpClient;
            this.localStorage = localStorage;
            anonymous = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }
        public override async Task<AuthenticationState > GetAuthenticationStateAsync()
        {
            var token = await localStorage.GetItemAsync<string>("authToke");
            if (string.IsNullOrWhiteSpace(token))
            {
                return anonymous;
            }
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token),"jwtAuthType")));
        } 
        public void  NotifyUserAuthentication(string token)
        {
            var authUser = new ClaimsPrincipal((new ClaimsIdentity(JwtParser.ParseClaimsFromJwt(token), "jwtAuthType")));
            var authState = Task.FromResult(new AuthenticationState(authUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void NotifyUserLogout()
        {
            var authState = Task.FromResult(anonymous);
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
