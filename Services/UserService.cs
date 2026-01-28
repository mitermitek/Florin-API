using Florin_API.Entities;
using Florin_API.Exceptions.User;
using Florin_API.Repositories.Interfaces;
using Florin_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Florin_API.Services;

public class UserService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository) : IUserService
{
    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        await IsUserExistsByEmailAsync(user.Email, cancellationToken);

        user.Password = passwordHasher.HashPassword(user, user.Password);

        return await userRepository.CreateUserAsync(user, cancellationToken);
    }

    public async Task IsUserExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        if (await userRepository.IsUserExistsByEmailAsync(email, cancellationToken))
        {
            throw new UserAlreadyExistsException();
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await userRepository.GetUserByEmailAsync(email, cancellationToken);
    }

    public bool VerifyUserPassword(User user, string password, CancellationToken cancellationToken)
    {
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);
        return passwordVerificationResult == PasswordVerificationResult.Success;
    }

    public async Task<User> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await userRepository.GetUserByIdAsync(id, cancellationToken) ?? throw new UserNotFoundException();
    }
}
