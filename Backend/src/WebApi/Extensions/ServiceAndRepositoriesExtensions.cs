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
            services.AddScoped<ICategoryService, CategoryService>();
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IBlogPostRepository, BlogPostRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            #endregion

            return services;
        }

    }
}
