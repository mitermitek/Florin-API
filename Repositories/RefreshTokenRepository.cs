using Florin_API.Data;
using Florin_API.Interfaces;
using Florin_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class RefreshTokenRepository(FlorinDbContext context) : IRefreshTokenRepository
{
    public async Task<RefreshToken> CreateRefreshTokenAsync(RefreshToken refreshToken)
    {
        context.RefreshTokens.Add(refreshToken);
        await context.SaveChangesAsync();
        return refreshToken;
    }

    public async Task<RefreshToken?> GetRefreshTokenAsync(string token, User user)
    {
        return await context.RefreshTokens
            .FirstOrDefaultAsync(rt => rt.Token == token && rt.RevokedAt == null && rt.UserId == user.Id);
    }

    public async Task UpdateRefreshTokenAsync(RefreshToken refreshToken)
    {
        context.RefreshTokens.Update(refreshToken);
        await context.SaveChangesAsync();
    }
}
