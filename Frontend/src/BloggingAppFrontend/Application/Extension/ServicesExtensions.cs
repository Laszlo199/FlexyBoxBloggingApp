using Blazored.LocalStorage;
using Blazored.Toast;
using BloggingAppFrontend.Application.Services;
using static BloggingAppFrontend.Application.Services.AuthService;

namespace BloggingAppFrontend.Application.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region Service
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IAuthService, AuthService>();
            #endregion

            services.AddBlazoredLocalStorage();
            services.AddBlazoredToast();
            services.AddHttpClient();

            return services;
        }
    }
}
