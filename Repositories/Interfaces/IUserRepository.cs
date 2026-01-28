using Florin_API.Entities;

namespace Florin_API.Repositories.Interfaces;

public interface IUserRepository
{
    Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task<bool> IsUserExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken);
}
