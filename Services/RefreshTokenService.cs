using System.Security.Cryptography;
using Florin_API.Exceptions;
using Florin_API.Interfaces;
using Florin_API.Models;

namespace Florin_API.Services;

public class RefreshTokenService(IRefreshTokenRepository refreshTokenRepository) : IRefreshTokenService
{
    public async Task<RefreshToken> CreateRefreshTokenAsync(User user)
    {
        var refreshToken = new RefreshToken
        {
            Id = default,
            Token = GenerateToken(),
            ExpiresAt = DateTime.UtcNow.AddDays(7),
            CreatedAt = DateTime.UtcNow,
            UserId = user.Id,
            User = user
        };

        return await refreshTokenRepository.CreateRefreshTokenAsync(refreshToken);
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token, User user)
    {
        return await refreshTokenRepository.GetRefreshTokenAsync(token, user);
    }

    public async Task RevokeRefreshTokenAsync(string token, User user)
    {
        var refreshToken = await GetRefreshTokenAsync(token, user);
        if (refreshToken == null)
        {
            throw new InvalidRefreshTokenException();
        }

        refreshToken.RevokedAt = DateTime.UtcNow;
        await refreshTokenRepository.UpdateRefreshTokenAsync(refreshToken);
    }

    public string GenerateToken()
    {
        var randomBytes = new byte[32];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);

        return Convert.ToBase64String(randomBytes);
    }
}
