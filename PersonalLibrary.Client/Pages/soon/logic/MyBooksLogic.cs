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
using Kendo.Blazor;

namespace PersonalLibrary.Client.Pages
{
    public class MyBooksLogic : BlazorComponent
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

        protected List<Book> Books { get; set; }

        protected string ErrorMessage { get; set; }
        #endregion


        protected override async Task OnInitAsync()
        {
            IsDataLoaded = false;
            await State.CheckIsLoggedIn();
            try
            {
                Books = await Http.GetJsonAsync<List<Book>>("api/book/getUserBooks");
                IsDataLoaded = true;
                Console.WriteLine("Data is loaded");
            }
            catch (Exception e)
            {
                this._showErrorMessage(e.Message);
                throw;
            }
            StateHasChanged();
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


        /*
        protected void UpdateItem(GridCommandEventArgs args)
        {
            Book book = args.Item as Book;
            if (book.Bookid == 0)//one way to tell inserted item in this scenario
            {
                var result = string.Format("On {2} you added the employee {0} who was hired on {1}.", alteredItem.Name, alteredItem.HireDate, DateTime.Now);
            }
            else
            {
                var result = string.Format("Employee with ID {0} now has name {1} and hire date {2}", alteredItem.ID, alteredItem.Name, alteredItem.HireDate);
            }
            StateHasChanged();
        }
        */
    }
}
