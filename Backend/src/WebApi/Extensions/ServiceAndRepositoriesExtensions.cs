using Application.IServices;
using Domain.IRepositories;
using Domain.Security;
using Domain.Services;
using Infrastructure.Repositories;
using WebApi.Middleware;

namespace WebApi.Extensions
{
    public static class ServiceAndRepositoriesExtensions
    {
        public static IServiceCollection AddServicesAndRepositories(this IServiceCollection services)
        {
            #region Services
            services.AddScoped<IUserService, UserService>()
                .AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();

            services.AddScoped<IBlogPostService, BlogPostService>()
                .AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();

            services.AddScoped<ICategoryService, CategoryService>()
                .AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();
            services.AddScoped<ISecurityService, SecurityService>()
                .AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();
            services.AddScoped<IAuthHelper, AuthHelper>()
                .AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();
            //services.AddScoped<IExceptionHandler, GlobalExceptionHandler>();
            #endregion

            #region Repositories
            services.AddScoped<IUserRepository, UserRepository>()
                .AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();

            services.AddScoped<IBlogPostRepository, BlogPostRepository>()
                .AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();

            services.AddScoped<ICategoryRepository, CategoryRepository>()
                .AddProblemDetails()
                .AddExceptionHandler<GlobalExceptionHandler>();
            #endregion

            return services;
        }

    }
}
