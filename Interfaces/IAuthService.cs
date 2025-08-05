using Florin_API.Models;

namespace Florin_API.Interfaces;

public interface IAuthService
{
    public Task<User> RegisterAsync(User user);
    public Task<User> LoginAsync(User user);
    public string GenerateAccessToken(User user);
}
