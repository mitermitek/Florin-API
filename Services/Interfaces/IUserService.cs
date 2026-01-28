using Florin_API.Entities;

namespace Florin_API.Services.Interfaces;

public interface IUserService
{
    Task<User> CreateUserAsync(User user, CancellationToken cancellationToken);
    Task IsUserExistsByEmailAsync(string email, CancellationToken cancellationToken);
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    bool VerifyUserPassword(User user, string password, CancellationToken cancellationToken);
    Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken);
}
