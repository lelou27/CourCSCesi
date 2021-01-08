using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using MyHN.Application;
using MyHN.Domain;
using MyHN.Infrastructure;
using MyHN.Infrastructure.Repositories;

namespace Api
{
    public class TokenOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }

        public SecurityKey Key => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
    }

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMyHNServices(Configuration).AddUserProvider<HttpUserProvider>();

            /*services.AddDbContext<MyHNDbContext>(options => options.UseSqlite(Configuration.GetConnectionString("Default")));
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IContext, MyHNDbContext>();
            services.AddMediatR(typeof(CreateLinkCommand));*/
            //services.AddScoped<IUserProvider, HttpUserProvider>();

            var tokenOptions = Configuration.GetSection("Token").Get<TokenOptions>();
            services.AddSingleton<TokenOptions>(tokenOptions);

            services.AddIdentityCore<IdentityUser>(options =>
            {
                options.Password.RequiredLength = options.Password.RequiredUniqueChars = 0;
                options.Password.RequireDigit = options.Password.RequireLowercase =
                    options.Password.RequireNonAlphanumeric = options.Password.RequireUppercase = false;
            })
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<MyHNDbContext>()
                .AddSignInManager();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        IssuerSigningKey = tokenOptions.Key,
                    };
                });

            services.AddControllers(options =>
            {
                options.Filters.Add(new CustomExceptionFilter());
            });

            services.AddOpenApiDocument(doc => {
                doc.AddSecurity("JWT", Enumerable.Empty<string>(), new NSwag.OpenApiSecurityScheme() { 
                    Type = NSwag.OpenApiSecuritySchemeType.ApiKey,
                    Name = "Authorization",
                    In = NSwag.OpenApiSecurityApiKeyLocation.Header,
                    Description = "Renseigner votre jeton : Bearer {jeton}"
                });

                doc.PostProcess = od =>
                {
                    od.Info.Title = "MyHN API Documentation";
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseOpenApi();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwaggerUi3();
            }

            app.UseCors(o => {
                o.AllowAnyOrigin();
                o.AllowAnyMethod();
                o.AllowAnyHeader();
            });

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World!");
                });
            });
        }
    }
}
