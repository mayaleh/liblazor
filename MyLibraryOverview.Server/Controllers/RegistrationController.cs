﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using MyLibraryOverview.Server.Models.Entities;
using MyLibraryOverview.Server.Models.New;
using MyLibraryOverview.Shared;
using System.Linq;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace MyLibraryOverview.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : AppBaseController
    {
        private readonly IEmailSender emailSender;

        public RegistrationController
            (
                SignInManager<UserAppIdentity> signInManager,
                UserManager<UserAppIdentity> userManager,
                IEmailSender emailSender
            ) : base(signInManager, userManager)
        {
            this.emailSender = emailSender;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> NewUser([FromBody] UserRegistration Input)
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

                    var code = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { UserId = user.Id, Code = code },
                        protocol: Request.Scheme);
                    await emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"<p>Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.</p>");


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


        [HttpPost("[action]")]
        public async Task<IActionResult> ConfirmAccount([FromBody] ConfirmationUser data)
        {
            if (ModelState.IsValid)
            {
                //var user = UserManager.
                var user = UserManager.FindByIdAsync(data.UserId).Result;
                var identityResult = await UserManager.ConfirmEmailAsync(user, data.Code); // TODO 
                if (identityResult.Succeeded)
                {
                    return Ok("Success");
                }
            }
            return Forbid("Failled");
        }

    }
}