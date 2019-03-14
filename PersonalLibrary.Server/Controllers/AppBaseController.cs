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
    public class AppBaseController : ControllerBase
    {
        public AppBaseController(SignInManager<UserAppIdentity> signInManager, UserManager<UserAppIdentity> userManager)
        {
            SignInManager = signInManager;
            UserManager = userManager;
        }

        protected SignInManager<UserAppIdentity> SignInManager { get; }
        protected UserManager<UserAppIdentity> UserManager { get; }
        

        protected Claim GetClaim(string claimName)
        {
            
            return ((ClaimsIdentity)User.Identity).FindFirst(claimName);
        }

        protected Task<string> GetUserId()
        {
            // var userId = UserManager.GetUserId(User);
            var _user = UserManager.FindByNameAsync(User.Identity.Name).Result;
            var userId = UserManager.GetUserIdAsync(_user);
            return userId;
        }
    }
}
