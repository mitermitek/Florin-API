using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Florin_API.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Florin_API.Services;

public class JwtService(IConfiguration configuration) : IJwtService
{
    public string GenerateToken(int userId)
    {
        var jwtSecret = configuration["JWT:Secret"] ?? throw new InvalidOperationException("JWT:Secret is not configured.");
        var jwtValidIssuer = configuration["JWT:ValidIssuer"] ?? throw new InvalidOperationException("JWT:ValidIssuer is not configured.");
        var jwtValidAudience = configuration["JWT:ValidAudience"] ?? throw new InvalidOperationException("JWT:ValidAudience is not configured.");

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64)
        };

        var token = new JwtSecurityToken(
            issuer: jwtValidIssuer,
            audience: jwtValidAudience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(10)),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public int GetUserIdFromToken(string token)
    {
        var jwtHandler = new JwtSecurityTokenHandler();
        var jwtToken = jwtHandler.ReadJwtToken(token);

        var userIdClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
        {
            throw new InvalidOperationException("Invalid token or user ID claim not found.");
        }

        return userId;
    }
}
