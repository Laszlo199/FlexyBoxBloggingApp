using Blazored.LocalStorage;
using BloggingAppFrontend.Application.Dtos.AuthDtos;
using Microsoft.AspNetCore.Components;

namespace BloggingAppFrontend.Application.Services
{
    public interface IAuthService
    {
        TokenDto? TokenDto { get; }
        Task Initialize();
        Task<bool> Register(RegisterDto registerDto);
        Task<bool> Login(LoginDto loginDto);
        Task Logout();
    }

    public class AuthService : IAuthService
    {
        private IHttpService _httpService;
        private NavigationManager _navigationManager;
        private ILocalStorageService _localStorageService;
        public TokenDto? TokenDto { get; private set; }

        public AuthService(
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
            if (TokenDto != null)
                return;

            TokenDto = await _localStorageService.GetItemAsync<TokenDto>("tokenDto");
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