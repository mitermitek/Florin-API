namespace Florin_API.Interfaces;

public interface IJwtService
{
    public string GenerateToken(int userId);
    public int GetUserIdFromToken(string token);
}
