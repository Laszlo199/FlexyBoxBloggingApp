using Application.IServices;
using Domain.IRepositories;
using Domain.Services;
using Infrastructure.Repositories;

namespace WebApi.Extensions
{
    public static class ServiceAndRepositoriesExtensions
    {
        public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IBlogPostService, BlogPostService>();
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            #endregion

            return services;
        }

    }
}
