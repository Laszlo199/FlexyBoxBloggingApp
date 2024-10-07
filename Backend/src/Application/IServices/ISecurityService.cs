using Application.Models;

namespace Application.IServices
{
    public interface ISecurityService
    {
        Task<TokenModel?> GenerateJwtToken(string email, string password);
        Task<bool> Create(string email, string password, string username);
        Task<bool> EmailExists(string email);
    }
}
