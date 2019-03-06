using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
namespace PersonalLibrary.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostBuilder = BuildWebHost(args);

            // Initialize the database
            var scopeFactory = hostBuilder.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Models.ApplicationDBContext>();

                //Todo upravit pro možnost volání
                if (dbContext.Database.EnsureCreated())
                {
                    var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<IdentityUser>>();

                    var roleManager = scope.ServiceProvider
                        .GetRequiredService<RoleManager<IdentityRole>>();

                    Models.Seed.SeedData.Initialize(dbContext, userManager, roleManager);
                }
            }
            
            hostBuilder.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                .UseStartup<Startup>()
                .Build();
    }
}
