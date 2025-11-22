using Florin_API.Entities;
using Florin_API.Exceptions.User;
using Florin_API.Repositories.Interfaces;
using Florin_API.Services.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Florin_API.Services;

public class UserService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository) : IUserService
{
    public async Task<User> CreateUserAsync(User user)
    {
        await IsUserExistsByEmailAsync(user.Email);

        user.Password = passwordHasher.HashPassword(user, user.Password);

        return await userRepository.CreateUserAsync(user);
    }

    public async Task IsUserExistsByEmailAsync(string email)
    {
        if (await userRepository.IsUserExistsByEmailAsync(email))
        {
            throw new UserAlreadyExistsException();
        }
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await userRepository.GetUserByEmailAsync(email);
    }

    public bool VerifyUserPassword(User user, string password)
    {
        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(user, user.Password, password);
        return passwordVerificationResult == PasswordVerificationResult.Success;
    }

    public async Task<User> GetUserByIdAsync(int id)
    {
        return await userRepository.GetUserByIdAsync(id) ?? throw new UserNotFoundException();
    }
}
