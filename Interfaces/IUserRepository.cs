using Florin_API.Models;

namespace Florin_API.Interfaces;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task<User?> GetUserByIdAsync(int id);
    Task<User> CreateUserAsync(User user);
}
