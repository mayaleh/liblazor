using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using PersonalLibrary.Shared;
using Microsoft.AspNetCore.Blazor;
using Cloudcrate.AspNetCore.Blazor.Browser.Storage;
using System.Threading.Tasks;
using PersonalLibrary.Client.Components;

namespace PersonalLibrary.Client.Pages
{
    public class AuthorsPublicLogic : BlazorComponent
    {
        #region Injected properties
        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected LocalStorage Storage { get; set; }

        [Inject]
        protected AppState State { get; set; }
        #endregion


        #region Data properties
        public List<Author> authors = new List<Author>();
    
        #endregion


        protected override async Task OnInitAsync()
        {

            try
            {
                authors = await Http.GetJsonAsync<List<Author>>("api/author/getAll");
                StateHasChanged();
            }
            catch (Exception)
            {
                throw;
            }


        }


    }
}
