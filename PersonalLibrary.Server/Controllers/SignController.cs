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
using Microsoft.AspNetCore.Authorization;
using PersonalLibrary.Server.Services;

namespace PersonalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignController : ControllerBase
    {
        private readonly IJwtTokenService _tokenService;
        private readonly UserModel _user = new UserModel();

        public SignController(IJwtTokenService tokenService)
        {
            _tokenService = tokenService;
        }

        [HttpPost("[action]")]
        public IActionResult In([FromBody] UserAccess user)
        {
            var userChecked = _user.GetUserAccessLogin(user);
            if (userChecked != null)
            {
                var token = _tokenService.BuildToken(userChecked.Email, userChecked.Userid.ToString());

                // Set Cookie
                /* In client - no access to cookies
                CookieOptions option = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30)
                };

                Response.Cookies.Append("userParamAccess", token, option);
                */


                return Ok(new { token, userChecked.Name }); // TODO add to browser storage user name and display it
            }
            else
            {
                return Forbid();
            }
        }

        //Todo Sign out

        [Authorize]
        [HttpPost("[action]")]
        public IActionResult UserCheck(string token) => Ok();
    }
}