using Blazored.LocalStorage;
using BloggingAppFrontend.Application.AuthGuard;
using BloggingAppFrontend.Application.Dtos.AuthDtos;
using Microsoft.AspNetCore.Components;

namespace BloggingAppFrontend.Application.Services
{
    public interface IAuthService
    {
        TokenDto? TokenDto { get; }
        Task<bool> Register(RegisterDto registerDto);
        Task<bool> Login(LoginDto loginDto);
        Task Logout();
    }

    public class AuthService : IAuthService
    {
        private readonly IHttpService _httpService;
        private readonly NavigationManager _navigationManager;
        private readonly ILocalStorageService _localStorageService;
        private readonly CustomAuthStateProvider _authStateProvider;
        public TokenDto? TokenDto { get; private set; }

        public AuthService(
            IHttpService httpService,
            NavigationManager navigationManager,
            ILocalStorageService localStorageService,
            CustomAuthStateProvider authStateProvider
        )
        {
            _httpService = httpService;
            _navigationManager = navigationManager;
            _localStorageService = localStorageService;
            _authStateProvider = authStateProvider;
        }


        public async Task<bool> Register(RegisterDto registerDto)
        {
            var RegisterDtoCopy = new RegisterDto
            {
                Email = registerDto.Email,
                Password = registerDto.Password,
                Username = registerDto.Username,
            };
            TokenDto = await _httpService.Post<TokenDto>("/api/Auth/Register", RegisterDtoCopy);
            if (TokenDto is null)
            {
                return false;
            }

            await _authStateProvider.MarkUserAsAuthenticated(TokenDto);
            return true;
        }

        public async Task<bool> Login(LoginDto loginDto)
        {
            var loginDtoCopy = new LoginDto
            {
                Email = loginDto.Email,
                Password = loginDto.Password,
            };
            TokenDto = await _httpService.Post<TokenDto>("/api/Auth/login", loginDtoCopy);
            if (TokenDto is null)
            {
                return false;
            }
        
            await _authStateProvider.MarkUserAsAuthenticated(TokenDto);
            return true;
        }

        public async Task Logout()
        {
            TokenDto = null;
            await _authStateProvider.MarkUserAsLoggedOut();
            _navigationManager.NavigateTo("login");
        }
    }
}