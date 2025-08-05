using Florin_API.Models;

namespace Florin_API.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken);
    Task<RefreshToken?> GetRefreshTokenAsync(string token, User user);
    Task UpdateRefreshTokenAsync(RefreshToken refreshToken);
}
