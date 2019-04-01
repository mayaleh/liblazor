using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using MyLibraryOverview.Shared;
using Microsoft.JSInterop;
using Microsoft.AspNetCore.Components.Layouts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;

namespace MyLibraryOverview.Client.Shared
{
    public class NavMenuLogic : LayoutComponentBase
    {
        #region Injected properties
        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }
        
        [Inject]
        protected IJSRuntime JSRuntime { get; set; }

        [Inject]
        private AppState State { get; set; }
        #endregion


        #region Parameters
        [Parameter]
        protected bool UserChanged { get; set; }

        [Parameter]
        private Action<bool> UserChangedChanged { get; set; }
        #endregion


        #region Binding variables



        protected bool ShowFillMessage { get; private set; } = false;
        protected bool IsWrongLoggin { get; private set; } = false;
        protected bool IsAvailableLogin { get; private set; } = true;

#if DEBUG
        protected string UserName { get; set; } = "salim.mayaleh";
        protected string Password { get; set; } = "0000RootAdmin12345";
#else
        protected string UserName { get; set; } = "";
        protected string Password { get; set; } = "";
#endif

        #endregion


        protected override void OnParametersSet()
        {
            base.OnParametersSet();
            StateHasChanged();
        }


        protected override async Task OnInitAsync()
        {
            await State.CheckIsLoggedIn();
            if(State.IsLoggedIn)
            {
                IsAvailableLogin = false;
            }
        }

       
        protected async Task SignIn()
        {
            
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
                ShowFillMessage = false;
                UserLogin userLogin = new UserLogin
                {
                    UserName = UserName,
                    Password = Password
                };
                await State.Login(userLogin);
                // is realy signed in?
                if (State.IsLoggedIn)
                {
                    this.CallJSHideModal();
                    IsWrongLoggin = false;
                }
                else
                {
                    //no, he has wrong access data
                    IsWrongLoggin = true;
                }
            }
            else
            {
                ShowFillMessage = true;
            }
            StateHasChanged();
        }

        protected async Task SignOut()
        {
            await State.Logout();
            IsAvailableLogin = true;
            StateHasChanged();
        }

        // I hope, one day this javascript will be pleasantly forgotten and never used...
        private async void CallJSHideModal()
        {

            // If modal hidden, then remove the code
            if(await JSRuntime.InvokeAsync<bool>("DestroyModal"))
            {
                IsAvailableLogin = false;
                StateHasChanged();
            }

            // Call our function with an object. It will be serialized (JSON),
            // passed to JS-part of Blazor and deserialized into a JavaScript
            // object again.
            //await JSRuntime.Current.InvokeAsync<bool>("say", new { greeting = "Hello" });

            // Get some demo data from a web service and pass it to our function.
            // Again, it will be turned to JSON and back during the function call.
            //var customers = await Http.GetJsonAsync<List<Customer>>("/api/Customer");
            //wait JSRuntime.Current.InvokeAsync<bool>("say", customers);
        }

    }
}
