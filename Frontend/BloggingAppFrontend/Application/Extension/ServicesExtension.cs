using Microsoft.AspNetCore.Authentication;

namespace BloggingAppFrontend.Application.Extension
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            #region Service

            services.AddScoped<IAuthenticationService, AuthenticationService>();
            //services.AddTransient<IHttpService, HttpService>();

            #endregion

            //services.AddBlazoredLocalStorage();
            //services.AddBlazoredToast();

            return services;
        }
    }
}
