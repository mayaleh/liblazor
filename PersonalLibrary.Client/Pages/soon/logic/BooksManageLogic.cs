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
    public class BooksManageLogic : BlazorComponent
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


        #region Status properties used to enable/disable/hide/show UI elements
        public string FiltrType = "listBooks";

        public bool isSearch = false;

        public int? editBookId = null;

        public string removedBookName;

        public string EditBtnTxt = "Edit";

        #endregion


        #region Data properties
        public List<Book> books = new List<Book>();

        public List<Author> authorsBook = new List<Author>();

        public List<Book> searchResult = new List<Book>();

        public string searchFor;
        #endregion


        protected override async Task OnInitAsync()
        {

            State.CheckIsLoggedIn().Wait(30);
            FiltrType = "listBooks";

            //Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Storage["useraccessparam"]);

            // pomoci GetJsonAsync<T> nelze přistupovat k hlavičce. Lze poslat bez gener. typu a pak deserealizovat do json s gen. typ
            /*
               HttpResponseMessage response = await url.GetAsync();
               HttpResponseHeaders headers = response.Headers;
               FooPayload payload = await response.ReadFromJsonAsync<FooPayload>();
             */


            try
            {

                books = await Http.GetJsonAsync<List<Book>>("api/mylibrary/getBooks");
            }
            catch (Exception)
            {

                throw;
            }


        }

        protected async Task GetBooksByAuthor()
        {

            await State.CheckIsLoggedIn();
            //books = await Http.GetJsonAsync<Book[]>("sample-data/books.json");
            FiltrType = "getByAuthors";
            //authorsBook = await Http.GetJsonAsync<Author[]>("api/mylibrary/getBooksByAuthor");
            //Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Storage["useraccessparam"]);
            authorsBook = await Http.GetJsonAsync<List<Author>>("api/mylibrary/getBooksByAuthor");
        }

        protected async void DeleteBook(Book deleteBook)
        {

            await State.CheckIsLoggedIn();
            await Http.SendJsonAsync(HttpMethod.Post, "/api/mylibrary/deleteBook", deleteBook);
            var item = books.SingleOrDefault(x => x.Bookid == deleteBook.Bookid);
            if (item != null)
                books.Remove(item);
            removedBookName = item.Name;
            //books.RemoveAll(x => x.Bookid == deleteBook);
        }

        protected async void SearchFor()
        {

            await State.CheckIsLoggedIn();
            if (!string.IsNullOrEmpty(searchFor))
            {
                isSearch = true;
                searchResult = books
                    .Where(bk =>
                        (bk.Name.ToUpper().Contains(searchFor.ToUpper())
                        //|| bk.Place.ToUpper().Contains(searchFor.ToUpper())
                        //|| bk.About.ToUpper().Contains(searchFor.ToUpper()) //nefunguje hledani v tomto
                        || bk.Author.Name.ToUpper().Contains(searchFor.ToUpper())
                    ))
                    .ToList<Book>();
                StateHasChanged();
            }
            else
            {

                isSearch = false;
            }
        }

        protected void HandleEditBtn(int rowId)
        {
            editBookId = rowId;
            StateHasChanged();
        }

    }
}
