using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyLibraryOverview.Server.Models.Entities;

namespace MyLibraryOverview.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //BuildWebHost(args).Run();
            var hostBuilder = BuildWebHost(args);
            var scopeFactory = hostBuilder.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<Models.ApplicationDBContext>();

                //Todo upravit pro možnost volání
                if (dbContext.Database.EnsureCreated())
                {
                    var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<UserAppIdentity>>();

                    var roleManager = scope.ServiceProvider
                        .GetRequiredService<RoleManager<IdentityRole>>();

                    Models.Seed.SeedData.Initialize(dbContext, userManager, roleManager);
                }
            }
            hostBuilder.Run();

        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder(args)
                    .UseConfiguration(new ConfigurationBuilder()
                    .AddCommandLine(args)
                    .Build())
                    .UseStartup<Startup>()
                    .Build();
        }
    }
}
