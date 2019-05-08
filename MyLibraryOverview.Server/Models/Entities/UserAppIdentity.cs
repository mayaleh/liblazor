using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace MyLibraryOverview.Server.Models.Entities
{
    public class UserAppIdentity : IdentityUser /* Has email */
    {
        [PersonalData]
        public string RealName { get; set; }

        public override string Email { get; set; }

        /*
        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
        public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
        public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
        */
        public virtual ICollection<UserBook> Userbook { get; set; }
    }
}
