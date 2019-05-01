using System;
using System.Collections.Generic;
using System.Net.Http;
using MyLibraryOverview.Shared;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Layouts;
using Microsoft.AspNetCore.Components;

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
        //public List<MyLibraryOverview.Shared.Book> books = new List<Book>();
        public IEnumerable<Book> books;

        public List<Author> authorsBook = new List<Author>();

        protected bool IsDataLoaded { get; set; } = false;

        protected bool IsError { get; set; } = false;

        protected string ErrMessage { get; set; }
        #endregion


        protected override async Task OnInitAsync()
        {

            try
            {
                books = await Http.GetJsonAsync<IEnumerable<Book>>("api/book/getAll");
                IsDataLoaded = true;
                Console.WriteLine("Data loaded");
                Console.WriteLine(books);
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
