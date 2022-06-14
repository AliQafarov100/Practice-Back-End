using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _6hours_Back_End.DAL;
using _6hours_Back_End.Models;
using _6hours_Back_End.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace _6hours_Back_End
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<LayoutService>();
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(opt =>
            {
                opt.UseSqlServer(_configuration.GetConnectionString("Default"));
            });
            services.AddIdentity<AppUser, IdentityRole>(option =>
             {
                 option.Password.RequireDigit = true;
                 option.Password.RequireLowercase = true;
                 option.Password.RequireUppercase = false;
                 option.Password.RequiredLength = 8;
                 option.Password.RequireNonAlphanumeric = false;

                 option.Lockout.MaxFailedAccessAttempts = 3;
                 option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                 option.SignIn.RequireConfirmedEmail = false;
                 option.User.AllowedUserNameCharacters = "qwertyuiopasdfghjklzxcvbnm_1234567890";

             }).AddDefaultTokenProviders().AddEntityFrameworkStores<AppDbContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                 name: "areas",
                 pattern: "{area:exists}/{controller=dashboard}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "Default",
                    pattern: "{controller=home}/{action=index}/{id?}");
            });
        }
    }
}
