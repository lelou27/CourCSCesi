using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyHN.Application;
using MyHN.Domain;
using MyHN.Infrastructure;
using MyHN.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Website
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyHNServices(Configuration).AddUserProvider<HttpUserProvider>();

            /*services.AddDbContext<MyHNDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("Default")));
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IContext, MyHNDbContext>();
            services.AddScoped<IUserProvider, HttpUserProvider>();*/

            services.AddIdentity<IdentityUser, IdentityRole>(options => {
                options.Password.RequiredLength = options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = options.Password.RequireLowercase = 
                    options.Password.RequireNonAlphanumeric = options.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<MyHNDbContext>();

            services.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme, o =>
            {
                o.LoginPath = "/Accounts/Login";
                o.LogoutPath = "/Acounts/Logout";
            });

            //services.AddMediatR(typeof(CreateLinkCommand));
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
                options.Filters.Add(new AuthorizeFilter());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var logger = app.ApplicationServices.GetRequiredService<ILogger<Startup>>();
            logger.LogInformation("Launching App !");

            MigrateDatabase(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            /*app.Use(async (context, next) =>
            {
                try
                {
                    await next();
                } catch (Exception e)
                {
                    context.Response.Redirect("")
                }
            });*/

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Links}/{action=Index}/{id?}");
            });
        }

        public void MigrateDatabase(IServiceProvider provider)
        {
            using var scope = provider.CreateScope();
            using var ctx = scope.ServiceProvider.GetRequiredService<MyHNDbContext>();
            ctx.Database.Migrate();
        }
    }
}
