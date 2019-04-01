using Microsoft.AspNetCore.Blazor.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using MyLibraryOverview.Shared;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Components.Layouts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;

namespace MyLibraryOverview.Client.Pages
{
    public class BooksPublicLogic : LayoutComponentBase
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
        public List<Book> books = new List<Book>();

        public List<Author> authorsBook = new List<Author>();
        
        #endregion


        protected override async Task OnInitAsync()
        {

            try
            {
                books = await Http.GetJsonAsync<List<Book>>("api/book/getAll");
            }
            catch (Exception)
            {

                throw;
            }


        }
       

    }
}
