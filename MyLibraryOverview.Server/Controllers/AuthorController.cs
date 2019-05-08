using Microsoft.AspNetCore.Mvc;
using MyLibraryOverview.Server.Models;
using MyLibraryOverview.Shared;
using System.Collections.Generic;


namespace MyLibraryOverview.Server.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private AuthorModel _author = new AuthorModel();

        [HttpGet("[action]")]
        public List<Author> GetAll()
        {
            var data = _author.GetAllAuthors();
            return data;
        }

    }
}