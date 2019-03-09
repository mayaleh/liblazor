﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PersonalLibrary.Server.Models;
using PersonalLibrary.Shared;


namespace PersonalLibrary.Server.Controllers
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