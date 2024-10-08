using Application.Dtos.Security;
using Application.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly ISecurityService _securityService;

        public AuthController(ISecurityService securityService)
        {
            _securityService = securityService;
        }

        [HttpPost(nameof(Login))]
        public async Task<ActionResult<TokenDto>> Login([FromBody] LoginDto loginDto)
        {
            var token = await _securityService.GenerateJwtToken(loginDto.Email, loginDto.Password);
            if (token is null)
            {
                return BadRequest("Invalid username or password!");
            }
            return new TokenDto
            {
                Jwt = token.Jwt,
                Message = token.Message,
                UserId = token.UserId,
            };
        }

        [HttpPost(nameof(Register))]
        public async Task<ActionResult<TokenDto>> Register([FromBody] RegisterDto registerDto)
        {
            var exists = await _securityService.EmailExists(registerDto.Email);
            if (exists)
                return BadRequest("Email is already in use!");
            if (await _securityService.Create(registerDto.Email, registerDto.Password, registerDto.Username))
            {
                var token = await _securityService.GenerateJwtToken(registerDto.Email, registerDto.Password);
                if (token is null)
                {
                    return BadRequest("Something went wrong during the registration process. Please contact an admin");
                }
                return new TokenDto
                {
                    Jwt = token.Jwt,
                    Message = token.Message,
                    UserId = token.UserId,
                };
            }
            return BadRequest("Something went wrong during the registration process. Please contact an admin");
        }
    }
}
