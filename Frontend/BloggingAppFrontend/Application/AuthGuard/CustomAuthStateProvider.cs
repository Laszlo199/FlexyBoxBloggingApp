using BloggingAppFrontend.Application.Dtos.AuthDtos;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BloggingAppFrontend.Application.AuthGuard
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedLocalStorage _localStorage;

        public CustomAuthStateProvider(ProtectedLocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async override Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var sessionModel = (await _localStorage.GetAsync<TokenDto>("sessionState")).Value;
            var identity = sessionModel == null ? new ClaimsIdentity() : GetClaimsIdentity(sessionModel);
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticated(TokenDto token)
        {
            await _localStorage.SetAsync("sessionState", token);
            var identity = GetClaimsIdentity(token);
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.DeleteAsync("sessionState");
            var identity = new ClaimsIdentity();
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }

        private ClaimsIdentity GetClaimsIdentity(TokenDto token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadJwtToken(token.Jwt);
            var claims = jwtToken.Claims.ToList();
            claims.Add(new Claim("UserId", token.UserId.ToString()));
            claims.Add(new Claim("Message", token.Message));
            return new ClaimsIdentity(claims, "jwt");
        }
        public async Task<int?> GetUserIdFromSessionState()
        {
            var sessionModel = (await _localStorage.GetAsync<TokenDto>("sessionState")).Value;
            return sessionModel?.UserId;
        }
    }
}