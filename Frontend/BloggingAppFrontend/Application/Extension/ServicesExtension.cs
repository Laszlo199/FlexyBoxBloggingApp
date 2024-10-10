using Blazored.LocalStorage;
using Blazored.Toast;
using BloggingAppFrontend.Application.Services;

namespace BloggingAppFrontend.Application.Extension
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region Service

            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IHttpService, HttpService>();
            services.AddTransient<IBlogPostService, BlogPostService>();
            services.AddTransient<ICategoryService, CategoryService>();
            #endregion

            services.AddBlazoredLocalStorage();
            services.AddBlazoredToast();

            return services;
        }
    }
}
