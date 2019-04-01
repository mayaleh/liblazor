using System;
using System.Collections.Generic;
using System.Net.Http;
using MyLibraryOverview.Shared;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Layouts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using Kendo.Blazor.Components.Grid;

namespace MyLibraryOverview.Client.Pages
{
    public class MyBooksLogic : LayoutComponentBase
    {
        #region Injected properties
        [Inject]
        protected HttpClient Http { get; set; }

        [Inject]
        protected IUriHelper UriHelper { get; set; }

        [Inject]
        protected AppState State { get; set; }
        #endregion

        #region Template State

        protected bool IsDataLoaded { get; set; } = false;
        protected bool IsError { get; set; } = false;

        #endregion

        #region Data properties

        protected IEnumerable<Book> Books { get; set; }

        protected string ErrorMessage { get; set; }
        #endregion


        protected override async Task OnInitAsync()
        {
            IsDataLoaded = false;
            await State.CheckIsLoggedIn();
            try
            {
                Books = await Http.GetJsonAsync<IEnumerable<Book>>("api/book/getUserBooks");
                IsDataLoaded = true;
                Console.WriteLine("Data is loaded");
            }
            catch (Exception e)
            {
                this._showErrorMessage(e.Message);
                throw;
            }
            finally
            {
                this.StateHasChanged();
            }
        }


        protected async Task RefreshBookList()
        {
            await this.OnInitAsync();
        }

        private void _showErrorMessage(string errMessage = "Ups! Something went wrong...")
        {
            ErrorMessage = errMessage;
            IsError = true;
            StateHasChanged();
        }

        private void _hiddeErroreMessage()
        {
            ErrorMessage = "";
            IsError = false;
        }

        protected void HandleEditBtn(int bookId)
        {
            Console.WriteLine("Edit clicked. Bookid: " + bookId.ToString());
        }


        protected void HandleDeleteBtn(int bookId)
        {
            Console.WriteLine("Delete clicked. Bookid: " + bookId.ToString());
        }


        protected void UpdateItem(GridCommandEventArgs args)
        {
            Book book = args.Item as Book;
            if (book.Bookid == 0)//one way to tell inserted item in this scenario
            {
                var result = string.Format("You added the book {0} {1}.", book.Name, book.Author.Name);
            }
            else
            {
                var result = string.Format("Book now has name {0} and author {1}", book.Name, book.Author.Name);
            }
            StateHasChanged();
        }
    }
}
