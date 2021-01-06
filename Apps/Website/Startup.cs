using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyHN.Application;
using MyHN.Domain;
using MyHN.Infrastructure;
using MyHN.Infrastructure.Repositories;

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
            services.AddDbContext<MyHNDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("Default")));
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IContext, MyHNDbContext>();

            services.AddMediatR(typeof(CreateLinkCommand));
            services.AddControllersWithViews(options =>
            {
                options.Filters.Add<CustomExceptionFilter>();
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
