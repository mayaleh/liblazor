using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.Shared;

namespace PersonalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MyLibraryController : ControllerBase
    {
        [HttpGet("[action]")]
        public IEnumerable<Book> Books()
        {
            var rng = new Random();
            return Enumerable.Range(1, 8).Select(index => new Book
            {
                Name = "Name of book " + rng.ToString(),
                Author = new Author
                {
                    Name = " Author Name " + rng.ToString(),
                    About = ""
                },
                About = "",
                Place = "Place " + rng.ToString()
                //Summary = Summaries[rng.Next(Summaries.Length)]
            });
        }
    }
}