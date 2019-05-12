using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using MyLibraryOverview.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Telerik.Blazor.Components.Grid;

namespace MyLibraryOverview.Client.Pages
{
    using importerBookResult = MyLibraryOverview.Shared.Library.Rop.Result<Book, Exception>;
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

        protected int BookGridPage { get; set; } = 1;

        enum MessageTypes { danger = 0, success, info, warning, primary };
        protected bool IsMessageBoxShown { get; set; } = false;
        protected int MessageBoxType { get; set; }
        protected string MessageBoxMessage { get; set; }

        #endregion

        #region Data properties

        protected List<Book> Books { get; set; }
        protected List<Book> BookSearchR { get; set; } = new List<Book>();

        protected string ErrorMessage { get; set; }

        //protected string SearchTerm { get; set; } = "";
        protected string SearchInput { get; set; } = "";

        protected Book NewBook { get; set; } = new Book();


        protected TelerikGrid<Book> BookGridRef;

        protected Telerik.Blazor.Components.Window.TelerikWindow AddFormWindow;
        #endregion


        protected override async Task OnInitAsync()
        {
            await State.CheckIsLoggedIn();
            await this.ReloadGridData();
        }

        private async Task ReloadGridData()
        {
            IsDataLoaded = false;
            try
            {
                Books = await Http.GetJsonAsync<List<Book>>("api/book/getUserBooks");
                IsDataLoaded = true;
            }
            catch (Exception e)
            {
                this.ShowErrorMessage(e.Message);
                throw;
            }
            finally
            {
                this.StateHasChanged();
            }
        }

        protected void OperationDoneWithMessage(bool isSuccess, string message)
        {
            // Show message
            MessageBoxMessage = message;
            if (isSuccess)
            {
                MessageBoxType = (int)MessageTypes.success;
                this.ReloadGridData().Wait();
            }
            else
            {
                MessageBoxType = (int)MessageTypes.danger;
            }
            IsMessageBoxShown = true;
        }

        public void OnMessageStatusClose()
        {
            IsMessageBoxShown = false;
        }

        #region Fulltext Search

        protected void SearchBook(string SearchTerm)
        {
            if (IsDataLoaded && !String.IsNullOrEmpty(SearchTerm))
            {
                SearchInput = SearchTerm;
                var find = Books.FindAll(bk =>
                            (
                                bk.Name.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.AuthorName.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.About.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.AuthorAbout.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.Note.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.Place.ToUpper().Contains(SearchTerm.ToUpper())
                            ));
                /*var find = Books.Where(bk =>
                            (
                                bk.Name.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.AuthorName.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.About.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.AuthorAbout.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.Note.ToUpper().Contains(SearchTerm.ToUpper())
                                || bk.Place.ToUpper().Contains(SearchTerm.ToUpper())
                            )
                        ); */
                if (find.Any())
                {
                    BookSearchR = find.ToList<Book>();
                }
                else
                {
                    BookSearchR = new List<Book>();
                }
            }
            else
            {
                BookSearchR = new List<Book>();
            }

        }

        protected void SetGridRow(int bookId)
        {
            Books = Books.Where(bk => bk.Bookid == bookId).ToList();
            SearchInput = (Books.Where(bk => bk.Bookid == bookId).Single()).Name;
            BookSearchR = new List<Book>();
        }

        #endregion

        protected async Task RefreshBookList()
        {
            await this.OnInitAsync();
        }

        private void ShowErrorMessage(string errMessage = "Ups! Something went wrong...")
        {
            ErrorMessage = errMessage;
            IsError = true;
            StateHasChanged();
        }

        private void HiddeErroreMessage()
        {
            ErrorMessage = "";
            IsError = false;
        }


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

        public async Task UpdateHandlerAsync(GridCommandEventArgs args)
        {
            Book item = (Book)args.Item;

            bool isInsert = args.IsNew;//insert or update operation

            if (isInsert)
            {
                //var some = Http.PostJsonAsync<List<Book>>("api/book/add", item);
            }
            else
            {
                importerBookResult result;
                try
                {
                    BookGridPage = BookGridRef.Page; // to return to the current page... Filter and sort lost...
                    result = await Http.PostJsonAsync<importerBookResult>("api/book/editBook", item);
                    if (result.IsSuccess)
                    {
                        Console.WriteLine(result.Success.Bookid);

                        this.OperationDoneWithMessage(result.IsSuccess, "Item updated successfully");
                    }
                    else
                    {
                        Console.WriteLine(result.Failure.Message);

                        this.OperationDoneWithMessage(result.IsSuccess, "Failled update item");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    this.ShowErrorMessage("Failed update item, please try it again or contact us...");
                }
            }

            Console.WriteLine("Update event is fired.");
        }

        public async Task DeleteHandlerAsync(GridCommandEventArgs args)
        {
            Book item = (Book)args.Item;

            //perform actual data source operation here

            //if you have a context added through an @inject statement, you could call its SaveChanges() method
            //myContext.SaveChanges();
            importerBookResult result;
            try
            {
                result = await Http.PostJsonAsync<importerBookResult>("api/book/deleteBook", item);
                if (result.IsSuccess)
                {
                    Console.WriteLine(result.Success.Bookid);
                }
                else
                {
                    Console.WriteLine(result.Failure.Message);
                    this.ShowErrorMessage("Failed update item, please try it again or contact us...");
                }
            }
            finally
            {
                //refresh grid
                await this.OnInitAsync();
            }
            Console.WriteLine("Delete event is fired.");
        }


        public void CreateHandler(GridCommandEventArgs args)
        {
            Console.WriteLine("Create event is fired.");

        }

        public void CancelHandler(GridCommandEventArgs args)
        {
            Book item = (Book)args.Item;

            Console.WriteLine("Create event is fired.");
        }
        #endregion


        public string TurnicateString(string str, int maxLenght)
        {
            if (str.Length > maxLenght)
            {
                str = str.Substring(0, maxLenght - 3) + "...";
            }
            return str;
        }


        public void OpenWindow()
        {
            AddFormWindow.Open();
        }

        public async Task SaveWindow()
        {
            var result = await Http.PostJsonAsync<importerBookResult>("api/book/addBook", NewBook);
            if (result.IsSuccess)
            {
                this.OperationDoneWithMessage(result.IsSuccess, "New book added successfully.");
                //AddFormWindow.Close();
            }
            else
            {
                this.OperationDoneWithMessage(result.IsSuccess, "Failled to create new book.");
            }
        }

        public void CloseWindow()
        {
            AddFormWindow.Close();
        }

    }
}
