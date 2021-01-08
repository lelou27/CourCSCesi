using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyHN.Application;
using MyHN.Domain;
using MyHN.Infrastructure.Repositories;

namespace MyHN.Infrastructure
{
    public class MyHNServicesBuilder
    {
        private readonly IServiceCollection _services;

        public MyHNServicesBuilder(IServiceCollection services)
        {
            _services = services;
        }

        public void AddUserProvider<TUserProvider>() where TUserProvider : class, IUserProvider
        {
            _services.AddScoped<IUserProvider, TUserProvider>();
        }
    }

    public static class ServiceCollectionExtension
    {
        public static MyHNServicesBuilder AddMyHNServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<MyHNDbContext>(options => options.UseSqlite(configuration.GetConnectionString("Default")));
            services.AddScoped<ILinkRepository, LinkRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<IContext, MyHNDbContext>();
            // services.AddScoped<IUserProvider, HttpUserProvider>();

            services.AddMediatR(typeof(CreateLinkCommand));

            return new MyHNServicesBuilder(services);
        }
    }
}
