using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using PersonalLibrary.Shared;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Blazor;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using System.Threading.Tasks;
//using ViewLogicSeparation.Logic;

namespace PersonalLibrary.Client.Pages
{
    public class AuthorsLogic : BlazorComponent
    {

        #region Injected properties
        // Note that Blazor's dependency injection works even if you use the
        // `InjectAttribute` in a component's base class.

        // Note that we decouple the component's base class from
        // the data access service using an interface.
        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected LocalStorage Storage { get; set; }
        #endregion


        #region Status properties used to enable/disable/hide/show UI elements
        public bool AuthorsLoaded => AuthorsData != null;
        #endregion

        public Author[] AuthorsData;

        public int? EditAuthorId = null;


        //public AuthorsLogic() : base()
        //{

        //}

        protected override async Task OnInitAsync()
        {
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Storage["useraccessparam"]);
            AuthorsData = await Http.GetJsonAsync<Author[]>("api/mylibrary/getAuthors");
            StateHasChanged();
        }
    }
}
