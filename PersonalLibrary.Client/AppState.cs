using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Services;
using PersonalLibrary.Shared;

namespace PersonalLibrary.Client
{
    public class AppState
    {
        private HttpClient _http;
        private readonly LocalStorage _localStorage;
        private IUriHelper _uriHelper;

        public int UserIdentity { get; private set; }

        public bool IsLoggedIn { get; private set; } = false;


        public AppState
            (
                HttpClient httpClient,
                LocalStorage localStorage,
                IUriHelper uriHelper
            )
        {
            _http = httpClient;
            _localStorage = localStorage;
            _uriHelper = uriHelper;
            
            if(!String.IsNullOrEmpty(_localStorage.GetItem("useraccessparam")))
            {
                sendCheckPost(false).Wait(30); //synch call
            }

        }

        public async Task CheckIsLoggedIn()
        {
            
            //FooPayload payload = await response.ReadFromJsonAsync<FooPayload>();

            if (string.IsNullOrEmpty(_localStorage.GetItem("useraccessparam")))
            {
                _uriHelper.NavigateTo("/sign-in");
            }
            else
            {
                await this.sendCheckPost();
            }
        }


        private async Task sendCheckPost(bool redirect = true)
        {
            HttpResponseMessage response = await _http.PostAsync("/api/sign/userCheck", new StringContent(_localStorage.GetItem("useraccessparam")));
            HttpResponseHeaders headers = response.Headers;
            if (response.IsSuccessStatusCode)
            {
                SetAuthorizationHeader();
            }
            else
            {
                if (redirect)
                {
                    this.Logout();
                }
                else
                {
                    _localStorage.Clear();
                    IsLoggedIn = false;
                }
            }
        }

        public void CheckIsLoggedIn(bool isPublicPage)
        {
           
        }


        public async Task Login(UserAccess loginDetails)
        {
            var response = await _http.PostJsonAsync<ResponseToken>("/api/sign/in", loginDetails);

            if (!string.IsNullOrEmpty(response.Token))
            {
                SaveIdentityToLocal(response);
                SetAuthorizationHeader();
                IsLoggedIn = true;
            }
        }

        public void Logout()
        {
            _localStorage.Clear();
            IsLoggedIn = false;
            _uriHelper.NavigateTo("/");
        }

        private void SaveIdentityToLocal(ResponseToken response)
        {
            _localStorage["useraccessparam"] = response.Token;
            _localStorage["username"] = response.Name;
            _localStorage["userIdentity"] = response.Id.ToString();
            UserIdentity = response.Id;
        }

        private void SetAuthorizationHeader()
        {
            if (!_http.DefaultRequestHeaders.Contains("Authorization"))
            {
                var token = _localStorage.GetItem("useraccessparam");
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
