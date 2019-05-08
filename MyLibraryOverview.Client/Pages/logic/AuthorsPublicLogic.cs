using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using MyLibraryOverview.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

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
