using System;
using System.Collections.Generic;
using System.Linq;
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

        BookModel book = new BookModel();
        AuthorModel author = new AuthorModel();


        [HttpGet("[action]")]
        public IEnumerable<Author> GetAuthors()
        {
            return author.GetAllAuthors();
        }

        /*
        [HttpGet]
        [Route("api/MyLibrary/GetBooks")]*/
        [HttpGet("[action]")]
        public IEnumerable<Book> GetBooks()
        {
            return book.GetAllBooks();
        }


    }
}