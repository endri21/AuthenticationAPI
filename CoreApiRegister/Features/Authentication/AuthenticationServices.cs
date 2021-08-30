using Blazored.LocalStorage;
using CoreApiRegister.Data.Models;
using CoreApiRegister.Features.Identity;
using eProtokoll.Core.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;

namespace CoreApiRegister.Features.Authentication
{
    public class AuthenticationServices : IAuthenticationServices
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authStateProvider;
        private readonly ILocalStorageService _localStorage;
        private readonly UserManager<User> _userManager;
        private readonly IIdentityService _identityService;

        public AuthenticationServices(HttpClient httpClient,
                                      AuthenticationStateProvider authStateProvider,
                                      ILocalStorageService localStorage,
                                      UserManager<User> userManager,
                                      IIdentityService identityService)
        {
            _httpClient = httpClient;
            _authStateProvider = authStateProvider;
            _localStorage = localStorage;
            _userManager = userManager;
            _identityService = identityService;
        }
        public async Task<AuthenticationResponseModel> Login(AuthenticationRequestModel model)
        {

            var user = await _userManager.FindByNameAsync(model.username);
            if (user == null)
            {
                return null;
            }
            var ispasswordvalid = await _userManager.CheckPasswordAsync(user, model.password);

            if (!ispasswordvalid)
            {
                return null;
            }

            var token = _identityService.GenerateJwtToken(
                user.Id,
                user.UserName,
                "this is a secret key"
                
                );
            //var data = new FormUrlEncodedContent(new[]
            //{
            //    new KeyValuePair <string, string>("grant_type","password"),
            //    new KeyValuePair <string, string>("username",model.username ),
            //    new KeyValuePair <string, string>("password",model.password),
            //});

            //var authResult = await _httpClient.PostAsync("https://localhost:44300/Identity/Login", data); //nese do perdorim nje api te konfiguruar  
            //var authContent = await authResult.Content.ReadAsStringAsync();
            //if (!authResult.IsSuccessStatusCode)
            //{
            //    return null;
            //}
            //nese e kemi me api marim nga api perndryshe do i marim nga nje servis tjt 
            //  var result = JsonSerializer.Deserialize<AuthenticationResponseModel>(authContent);
            var result = new AuthenticationResponseModel()
            {
                accessToken = token,
                success = true,
                username = model.username
            };
            
            await _localStorage.SetItemAsync("authToken", result.accessToken);
            ((AuthStateProvider)_authStateProvider).NotifyUserAuthentication(result.accessToken);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", result.accessToken);
            return result;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((AuthStateProvider)_authStateProvider).NotifyUserLogout();
            _httpClient.DefaultRequestHeaders.Authorization = null;

        }
    }
}
