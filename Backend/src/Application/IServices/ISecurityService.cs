using Application.Models;

namespace Application.IServices
{
    public interface ISecurityService
    {
        Task<TokenModel?> GenerateJwtToken(string email, string password);
        Task<bool> Create(string email, string password, string username, string loginPublicKey);
        Task<bool> EmailExists(string loginDtoEmail);
    }
}
