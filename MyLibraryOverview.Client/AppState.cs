using System;
using System.Threading.Tasks;
using System.Net.Http;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using MyLibraryOverview.Shared;

namespace MyLibraryOverview.Client
{
    public class AppState
    {
        private HttpClient _http;
        //private readonly LocalStorage _localStorage;

        private IUriHelper _uriHelper;

        public int UserIdentity { get; private set; }

        public bool IsLoggedIn { get; private set; } = false;

        private UserState _userState { get; set; }

        public string UserFullName { get; private set; } = "";

        public AppState
            (
                HttpClient httpClient,
                //LocalStorage localStorage,
                IUriHelper uriHelper
            )
        {
            _http = httpClient;
            //_localStorage = localStorage;
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

            if (response.IsLoggedIn)
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

            if (IsLoggedIn == false)
            {
                //_localStorage.Clear();
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
    }
}
