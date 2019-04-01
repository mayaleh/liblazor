using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyLibraryOverview.Server.Models;
using MyLibraryOverview.Shared;
using MyLibraryOverview.Shared;
using Microsoft.AspNetCore.Authorization;
using MyLibraryOverview.Server.Services;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using MyLibraryOverview.Server.Models.Entities;

namespace MyLibraryOverview.Server.Controllers
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
