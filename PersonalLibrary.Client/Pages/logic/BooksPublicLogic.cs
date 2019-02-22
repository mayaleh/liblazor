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
using System.Linq;

namespace PersonalLibrary.Client.Pages
{
    public class BooksPublicLogic : BlazorComponent
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
