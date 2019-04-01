using Microsoft.AspNetCore.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using MyLibraryOverview.Shared;
using Microsoft.AspNetCore.Blazor;
using System.Threading.Tasks;
using MyLibraryOverview.Client.Components;
using Microsoft.AspNetCore.Components.Layouts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;

namespace MyLibraryOverview.Client.Pages
{
    public class AuthorsPublicLogic : LayoutComponentBase
    {
        #region Injected properties
        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }


        [Inject]
        private AppState State { get; set; }
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
