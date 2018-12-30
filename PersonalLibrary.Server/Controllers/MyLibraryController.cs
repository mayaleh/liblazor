using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.Server.Models;
using PersonalLibrary.Shared;

namespace PersonalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyLibraryController : ControllerBase
    {

        BookModel _book = new BookModel();
        AuthorModel _author = new AuthorModel();


        [HttpGet("[action]")]
        public IEnumerable<Author> GetAuthors()
        {
            return _author.GetAllAuthors();
        }

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
        public void AddAuthor([FromBody] Author author)
        {
            if (ModelState.IsValid)
                _author.AddAuthor(author);
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

    }
}