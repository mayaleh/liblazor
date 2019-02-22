using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.Server.Models;
using PersonalLibrary.Shared;


namespace PersonalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        BookModel _book = new BookModel();


        [HttpGet("[action]")]
        public List<Book> GetAll()
        {
            var data = _book.GetAllBooks();
            return data;
        }

    }
}