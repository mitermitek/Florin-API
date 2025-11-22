using Florin_API.Entities;

namespace Florin_API.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user);
    Task IsUserExistsByEmailAsync(string email);
    Task<User?> GetUserByEmailAsync(string email);
    bool VerifyUserPassword(User user, string password);
    Task<User> GetUserByIdAsync(int id);
}
