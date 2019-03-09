using Microsoft.AspNetCore.Identity;
using PersonalLibrary.Server.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalLibrary.Server.Models.Seed
{
    // Create startup data
    public class SeedData
    {
        public static void Initialize(
            ApplicationDBContext dBContext,
            UserManager<UserAppIdentity> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            InitIdentity(userManager, roleManager);
        }

        private static void InitIdentity(UserManager<UserAppIdentity> userManager, RoleManager<IdentityRole> roleManager)
        {
            Task.Run(async () =>
            {
                var roleAdmin = new IdentityRole("Admin");
                var roleUser = new IdentityRole("User");

                var admin = new UserAppIdentity
                {
                    Email = "salim.mayaleh@gmail.com",
                    RealName = "Salim Mayaleh",
                    UserName = "salim.mayaleh"
                }
                ; //could be extended to my User

                await roleManager.CreateAsync(roleAdmin); 
                await roleManager.CreateAsync(roleUser);

                var roles = new[] { roleAdmin.Name, roleUser.Name };

                await userManager.CreateAsync(admin, "0000RootAdmin12345");

                await userManager.AddToRolesAsync(admin, roles);
            })
            .GetAwaiter()
            .GetResult();
        }

    }
}
