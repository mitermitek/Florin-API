using Florin_API.Entities;

namespace Florin_API.Services.Interfaces;

public interface IAuthService
{
    Task<User> RegisterAsync(User user);
    Task<User> LoginAsync(User user);
    Task LogoutAsync();
}
