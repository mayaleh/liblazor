using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyLibraryOverview.Server.Models;
using MyLibraryOverview.Server.Models.Entities;
using MyLibraryOverview.Server.Models.New;
using MyLibraryOverview.Server.Services;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;

namespace MyLibraryOverview.Server
{
    public class Startup
    {

        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddEntityFrameworkNpgsql();
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationDBContext>(
                    options =>
                        options.UseNpgsql(
                                connectionString
                            )
                );



            #region Identity Services

            services
               .AddDefaultIdentity<UserAppIdentity>(config =>
               {
                   config.SignIn.RequireConfirmedEmail = true;
               })
               .AddRoles<IdentityRole>()
               .AddDefaultTokenProviders()
               //.AddDefaultUI(UIFramework.Bootstrap4)
               .AddEntityFrameworkStores<ApplicationDBContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Default Lockout settings.
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                //options.Lockout.AllowedForNewUsers = true;

                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 6;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
            });

            services.AddAuthentication(
                    options =>
                    {
                        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    }
                ).AddCookie();

            #endregion

            #region DI Model services
            // Translator of entites between Client and Server
            services.AddScoped<EntitiyTranslator>();

            services.AddScoped<Models.New.BookModel>();
            services.AddScoped<Models.New.AuthorModel>();
            #endregion



            #region Email sender
            // requires
            // using Microsoft.AspNetCore.Identity.UI.Services;
            // using WebPWrecover.Services;
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<AuthMessageSenderOptions>(Configuration);
            #endregion

            //DefaultContractResolver contractResolver = new DefaultContractResolver { NamingStrategy = new PascalCaseNamingStrategy() };
            /*
            var ser = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                ContractResolver = contractResolver,
                Formatting = Formatting.Indented
            }; 
            NewtonsoftJsonOutputFormatter jsonOutputFormatter = new NewtonsoftJsonOutputFormatter(ser, ArrayPool<char>.Shared);
            */


            services.AddMvc().AddNewtonsoftJson(
              /*options =>
              {
                  //options.SerializerSettings.ContractResolver = contractResolver;
                  //options.SerializerSettings.ContractResolver = contractResolver;
                  //options.SerializerSettings.Formatting = Formatting.None;
                  //options.SerializerSettings.;
              }*/
              );

            //services.AddMvc().AddNewtonsoftJson();


            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();
            }


            app.UseCookiePolicy();
            app.UseAuthorization();
            app.UseAuthentication();

            //app.UseMvc(routes =>
            //{
            //    routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}");
            //});

            app.UseRouting();

            app.UseEndpoints(routes =>
            {
                routes.MapDefaultControllerRoute();
            });

            app.UseBlazor<Client.Startup>();

        }

        static string ToPascalCase(string text)
        {
            var chars = text.ToCharArray();

            return String.Concat(Char.ToUpperInvariant(chars[0]), text.Substring(1));
        }

        // Not used
        public class PascalCaseNamingStrategy : NamingStrategy
        {
            protected override string ResolvePropertyName(string name)
            {
                return ToPascalCase(name);
            }
        }
    }
}
