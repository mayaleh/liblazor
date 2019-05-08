using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyLibraryOverview.Server.Models;
using MyLibraryOverview.Shared;
using System.Collections.Generic;

namespace MyLibraryOverview.Server.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MyLibraryController : ControllerBase
    {
        //todo Edit to DI
        BookModel _book = new BookModel();
        AuthorModel _author = new AuthorModel();

        #region Authors

        [HttpGet("[action]")]
        public IEnumerable<Author> GetAuthors()
        {
            return _author.GetAllAuthors();
        }


        [HttpGet("[action]/{id}")]
        public Author AuthorDetail(int id)
        {
            return _author.GetAuthor(id);
        }

        [HttpPost("[action]")]
        public void AddAuthor([FromBody] Author author)
        {
            if (ModelState.IsValid)
                _author.SaveAuthor(author);
        }

        [HttpPost("[action]")]
        public void EditAuthor([FromBody] Author author)
        {
            if (ModelState.IsValid)
                _author.SaveAuthor(author);
        }
        #endregion

        #region Books

        /*
        [HttpGet]
        [Route("api/MyLibrary/GetBooks")]
        */
        [HttpGet("[action]")]
        public List<Book> GetBooks()
        {
            var data = _book.GetAllBooks(); //new HttpResponseException
            return data;
        }

        [HttpGet("[action]")]
        public List<Author> GetBooksByAuthor()
        {
            return _book.GetAllBooksByAuthor();
        }


        [HttpGet("[action]/{id}")]
        public Book BookDetail(int id)
        {
            return _book.GetBook(id);
        }


        [HttpPost("[action]")]
        public void AddBook([FromBody] Book book)
        {
            if (ModelState.IsValid)
                _book.SaveBook(book);
        }



        [HttpPost("[action]")]
        public void EditBook([FromBody] Book book)
        {
            if (ModelState.IsValid)
                _book.SaveBook(book);
        }

        //[HttpDelete]
        [HttpPost("[action]")]
        public void DeleteBook(Book book)
        {
            if (ModelState.IsValid)
                _book.DeleteBook(book.Bookid);
        }

        #endregion
    }
}