using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
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
