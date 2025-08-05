using Florin_API.Models;

namespace Florin_API.Interfaces;

public interface IRefreshTokenService
{
    Task<RefreshToken> CreateRefreshTokenAsync(User user);
    Task<RefreshToken?> GetRefreshTokenAsync(string token, User user);
    Task RevokeRefreshTokenAsync(string token, User user);
    string GenerateToken();
}
