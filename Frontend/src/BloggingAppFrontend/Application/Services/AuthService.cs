using Blazored.LocalStorage;
using BloggingAppFrontend.Application.Dtos.AuthDtos;
using Microsoft.AspNetCore.Components;

namespace BloggingAppFrontend.Application.Services
{
    public class AuthService
    {
        public interface IAuthenticationService
        {
            TokenDto? TokenDto { get; }
            Task Initialize();
            Task<bool> Register(RegisterDto registerDto);
            Task<bool> Login(LoginDto loginDto);
            Task Logout();
        }

        public class AuthenticationService : IAuthenticationService
        {
            private IHttpService _httpService;
            private NavigationManager _navigationManager;
            private ILocalStorageService _localStorageService;
            public TokenDto? TokenDto { get; private set; }

            public AuthenticationService(
                IHttpService httpService,
                NavigationManager navigationManager,
                ILocalStorageService localStorageService
            )
            {
                _httpService = httpService;
                _navigationManager = navigationManager;
                _localStorageService = localStorageService;
            }

            public async Task Initialize()
            {
                TokenDto = await _localStorageService.GetItemAsync<TokenDto>("tokenDto");
            }

            public async Task<bool> Register(RegisterDto registerDto)
            {
                var registerDtoCopy = new RegisterDto
                {
                    Email = registerDto.Email,
                    Password = registerDto.Password,
                    Username = registerDto.Username,
                };
                TokenDto = await _httpService.Post<TokenDto>("/api/Auth/Register", registerDtoCopy);
                if (TokenDto is null)
                {
                    return false;
                }
                await _localStorageService.SetItemAsync("tokenDto", TokenDto);
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
                await _localStorageService.SetItemAsync("tokenDto", TokenDto);
                return true;
            }

            public async Task Logout()
            {
                TokenDto = null;
                await _localStorageService.RemoveItemAsync("tokenDto");
                _navigationManager.NavigateTo("login");
            }
        }
    }
}
