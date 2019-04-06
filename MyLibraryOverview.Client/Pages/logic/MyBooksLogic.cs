using System;
using System.Collections.Generic;
using System.Net.Http;
using MyLibraryOverview.Shared;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Layouts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using Kendo.Blazor.Components.Grid;
using System.Linq;

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
        private AppState State { get; set; }
        #endregion

        #region Template State

        protected bool IsDataLoaded { get; set; } = false;
        protected bool IsError { get; set; } = false;
        protected bool IsEdit { get; set; } = false;
        protected Book EditBook { get; set; }

        #endregion

        #region Data properties

        protected List<Book> Books { get; set; }
        protected List<Book> BookSearchR { get; set; } = new List<Book>();

        protected string ErrorMessage { get; set; }

        //protected string SearchTerm { get; set; } = "";
        protected string SearchInput { get; set; } = "";
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
            finally
            {
                this.StateHasChanged();
            }
        }

        protected void SearchBook()
        {
            Console.WriteLine("Search called");
        }
        protected void SearchBook(string SearchTerm)
        {
            //var SearchTerm = e.Value;
            //Console.WriteLine(SearchTerm.ToString());
            if (IsDataLoaded)
            {
                SearchInput = SearchTerm;
                var find = Books.Where(bk =>
                            (
                                bk.Name.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.AuthorName.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.About.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.AuthorAbout.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.Note.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.Place.ToUpper().Contains(SearchTerm.ToUpper())
                            )
                        );
                if(find != null)
                {
                    BookSearchR = find.ToList<Book>();
                }
                else
                {
                    BookSearchR = new List<Book>();
                }
                    //.ToList<Book>();
                //StateHasChanged(); 
            }

        }
    
        protected void SetGridRow(int bookId)
        {
            Books = Books.Where(bk => bk.Bookid == bookId).ToList();
            SearchInput = (Books.Where(bk => bk.Bookid == bookId).Single()).Name;
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
                var result = string.Format("You added the book {0} {1}.", book.Name, book.Author.Name);
            }
            else
            {
                var result = string.Format("Book now has name {0} and author {1}", book.Name, book.Author.Name);
            }
            StateHasChanged();
        }
        */


        #region Grid Actions
        public void EditHandler(GridCommandEventArgs args)
        {
            EditBook = (Book)args.Item;
            IsEdit = true;

            //prevent opening for edit based on condition
            //if (item.ID < 3)
            //{
            //    args.IsCancelled = true;//the general approach for cancelling an event
            //}

            Console.WriteLine("Edit event is fired. Book: " + EditBook.Name);
        }

        public void UpdateHandler(GridCommandEventArgs args)
        {
            Book item = (Book)args.Item;

            //bool isInsert = args.IsNew;//insert or update operation

            //perform actual data source operations here
            //if you have a context added through an @inject statement, you could call its SaveChanges() method
            //myContext.SaveChanges();
            Console.WriteLine("Update event is fired.");
        }

        public void DeleteHandler(GridCommandEventArgs args)
        {
            Book item = (Book)args.Item;

            //perform actual data source operation here

            //if you have a context added through an @inject statement, you could call its SaveChanges() method
            //myContext.SaveChanges();
            Console.WriteLine("Delete event is fired.");
        }


        public void CreateHandler(GridCommandEventArgs args)
        {
            Console.WriteLine("Create event is fired.");
            //there is no Item associated with this event handler
        }

        public void CancelHandler(GridCommandEventArgs args)
        {
            Book item = (Book)args.Item;

            //perform actual data source operation here (like cancel changes on a context)
            //if you have a context added through an @inject statement, you could use something like this to abort changes
            //foreach (var entry in myContext.ChangeTracker.Entries().Where(entry => entry.State == EntityState.Modified))
            //{
            //  entry.State = EntityState.Unchanged;
            //}
            Console.WriteLine("Create event is fired.");
        }
        #endregion


        public string TurnicateString(string str, int maxLenght)
        {
            if (str.Length > maxLenght)
            {
                str = str.Substring(0, maxLenght-3) + "...";
            }
            return str;
        }

    }
}
