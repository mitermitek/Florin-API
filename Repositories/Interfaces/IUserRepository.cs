using Florin_API.Entities;

namespace Florin_API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user);
    Task<bool> IsUserExistsByEmailAsync(string email);
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(int id);
}
