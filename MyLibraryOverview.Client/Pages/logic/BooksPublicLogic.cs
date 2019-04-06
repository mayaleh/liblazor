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

        protected bool IsDataLoaded { get; set; } = false;

        protected bool IsError { get; set; } = false;

        protected string ErrMessage { get; set; }
        #endregion


        protected override async Task OnInitAsync()
        {

            try
            {
                books = await Http.GetJsonAsync<List<Book>>("api/book/getAll");
                IsDataLoaded = true;
                Console.WriteLine("Data loaded");
            }
            catch (Exception e)
            {
                ErrMessage = "Ups! Something went wrong. Error: " + e.Message; 
                //throw;
            }
            finally
            {
                StateHasChanged();
            }


        }
       

    }
}
