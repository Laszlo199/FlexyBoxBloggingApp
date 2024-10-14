using Blazored.LocalStorage;
using Blazored.Toast;
using BloggingAppFrontend.Application.AuthGuard;
using BloggingAppFrontend.Application.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

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
            services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
            #endregion

            services.AddBlazoredLocalStorage();
            services.AddBlazoredToast();
            services.AddAuthentication();
            services.AddScoped<ProtectedLocalStorage>();

            // Register CustomAuthStateProvider before AuthService
            services.AddScoped<CustomAuthStateProvider>();

            // Register AuthenticationStateProvider as CustomAuthStateProvider
            services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<CustomAuthStateProvider>());
            services.AddCascadingAuthenticationState();

            return services;
        }
    }
}
