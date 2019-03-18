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
    public class RegistrationController : AppBaseController
    {
        public RegistrationController
            (
                SignInManager<UserAppIdentity> signInManager,
                UserManager<UserAppIdentity> userManager
            ) : base(signInManager, userManager)
        {

        }

        [HttpPost("[action]")]
        public async Task<IActionResult>  NewUser([FromBody] UserRegistration Input)
        {
           // returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new UserAppIdentity
                {
                    RealName = Input.RealName,
                    UserName = Input.UserName,
                    Email = Input.Email
                };
                var result = await UserManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    // _logger.LogInformation("User created a new account with password.");
                    /*
                     var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                     var callbackUrl = Url.Page(
                         "/Account/ConfirmEmail",
                         pageHandler: null,
                         values: new { userId = user.Id, code = code },
                         protocol: Request.Scheme);
                     await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                         $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                         */

                    //await SignInManager.SignInAsync(user, isPersistent: false);
                    //return LocalRedirect(returnUrl);
                    return Ok(new { Code = "Success", Describtion = "New user created successfully!" });
                }
                else
                {
                    IdentityError errReg = result.Errors.FirstOrDefault();

                    /*
                     * 
                    var errList = new List<IdentityError>();
                    foreach (var error in result.Errors)
                    {
                        errList.Add(error);
                        //ModelState.AddModelError(string.Empty, error.Description);
                    }
                    */
                    return Ok(errReg);
                }
            }
            else
            {
                return BadRequest();
            }

            // If we got this far, something failed, redisplay form
            //return Page();
        }
    }
}