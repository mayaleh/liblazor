using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Services;
using Newtonsoft.Json;
using PersonalLibrary.Shared;
using PersonalLibrary.Shared.Model;

namespace PersonalLibrary.Client
{
    public class AppState
    {
        private HttpClient _http;
        private readonly LocalStorage _localStorage;
        private IUriHelper _uriHelper;

        public int UserIdentity { get; private set; }

        public bool IsLoggedIn { get; private set; } = false;

        private UserState _userState { get; set; }

        public string UserFullName { get; private set; } = "";

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
        }

        public async Task CheckIsLoggedIn()
        {
            
            var response = await _http.GetAsync("/api/sign/getUser");
            if (response.IsSuccessStatusCode)
            {
                SaveIdentityToLocal(JsonConvert.DeserializeObject<UserState>(await response.Content.ReadAsStringAsync()));
            }
            else
            {
                throw new Exception((int)response.StatusCode + "-" + response.StatusCode.ToString());
            }

        }
        
        public async Task Login(UserLogin loginDetails)
        {
            var response = await _http.PostJsonAsync<UserState>("/api/sign/in", loginDetails);

            if(response.IsLoggedIn)
            {
                SaveIdentityToLocal(response);
                IsLoggedIn = true;
            }
        }

        public async Task Logout()
        {

            var response = await _http.PutJsonAsync<UserState>("/api/sign/out", _userState);
            IsLoggedIn = response.IsLoggedIn;
            _userState = response;

            if(IsLoggedIn == false)
            {
                _localStorage.Clear();
                //_uriHelper.NavigateTo("/");
            }

        }

        private void SaveIdentityToLocal(UserState response)
        {
            //TODO get real name
            IsLoggedIn = response.IsLoggedIn;
            _userState = response;
            UserFullName = response.FullName;
        }
        

        [Obsolete("sendCheckPost is deprecated, now we are using diffrent method to authenticate.")]
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
                    await this.Logout();
                }
                else
                {
                    _localStorage.Clear();
                    IsLoggedIn = false;
                }
            }
        }

        [Obsolete("SetAuthorizationHeader is deprecated, don't edit request header.")]
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
