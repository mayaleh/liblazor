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
using PersonalLibrary.Shared.Model;
using Microsoft.AspNetCore.Authorization;
using PersonalLibrary.Server.Services;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using PersonalLibrary.Server.Models.Entities;

namespace PersonalLibrary.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignController : ControllerBase
    {

        // udelat svuj objekt pro uzivatele dedici z IdentityUser - pridat email a realname a nahradit IdentityUser
        // pak upravit userState a vracet realname a email
        // UserManager.GetEmailAsync(user);
        private readonly SignInManager<UserAppIdentity> signInManager;
        private readonly UserManager<UserAppIdentity> userManager;

        public SignController(SignInManager<UserAppIdentity> signInManager, UserManager<UserAppIdentity> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpGet("[action]")]
        public UserState GetUser()
        {
            if(User.Identity.IsAuthenticated)
            {

            }

            return User.Identity.IsAuthenticated
               ? new UserState
               {
                   IsLoggedIn = true,
                   FullName = ((ClaimsIdentity)User.Identity).FindFirst("RealName").Value, // TODO refactoring - create private function
                   LoginName = User.Identity.Name,
                   Email = ((ClaimsIdentity)User.Identity).FindFirst(ClaimTypes.Email).Value
               }
               : new UserState { IsLoggedIn = false };
        }

        [HttpPost("[action]")]
        public async Task<JsonResult> In([FromBody] UserLogin userLogin)
        {
            //var u = User.Identity;
            var result = await signInManager.PasswordSignInAsync(userLogin.UserName, userLogin.Password, true, lockoutOnFailure: true);
            //System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            

            if (result.Succeeded)
            {
                var identity = (ClaimsIdentity)User.Identity;

                var userApp = userManager.FindByNameAsync(userLogin.UserName).Result;
                

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userLogin.UserName),
                    new Claim(ClaimTypes.Role, "Administrator"),
                    new Claim(ClaimTypes.Email, userApp.Email),
                    new Claim("RealName", userApp.RealName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                HttpContext.User = new ClaimsPrincipal(claimsIdentity);

                var userState = GetUser();

                return new JsonResult(userState);
            }
            else
            {
                return new JsonResult(new UserState { IsLoggedIn = false });
            }
        }

        [HttpPut("[action]")]
        public async Task<UserState> Out()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            //return LoggedOutState;
            return new UserState { IsLoggedIn = false };
        }

        #region OLD jwtToken
        /*
        [HttpPost("[action]")]
        public IActionResult In([FromBody] UserAccess user)
        {
            var userChecked = userModel.GetUserAccessLogin(user);
            if (userChecked != null)
            {
                var token = _tokenService.BuildToken(userChecked.Email, userChecked.Userid.ToString());

                // Set Cookie

                CookieOptions option = new CookieOptions
                {
                    Expires = DateTime.Now.AddMinutes(30)
                };

                Response.Cookies.Append("token", token, option);
                Response.Cookies.Append("userName", userChecked.Name, option);
                


                return Ok(new { token, userChecked.Name, userChecked.Userid }); // add to browser storage user name and display it
            }
            else
            {
                return Forbid();
            }
        } */

        [Authorize]
        [HttpPost("[action]")]
        public IActionResult UserCheck(string token) => Ok();
        /*public IActionResult UserCheck(HttpRequestMessage request)// => Ok();
         {
            CookieHeaderValue token = request.Headers.GetCookies("token").FirstOrDefault();

            if (token == null)
            {
                return Forbid();
            }


            CookieHeaderValue name = request.Headers.GetCookies("userName").FirstOrDefault();

            string userName = "";
            if (name != null)
            {
                userName = name["userName"].Value;
            }

            return Ok(new { token["token"].Value, userName });
        } */

        #endregion
        //Todo Sign out

    }
}