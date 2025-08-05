using Florin_API.Exceptions;
using Florin_API.Interfaces;
using Florin_API.Models;
using Microsoft.AspNetCore.Identity;

namespace Florin_API.Services;

public class AuthService(IPasswordHasher<User> passwordHasher, IUserRepository userRepository, IJwtService jwtService) : IAuthService
{
    public async Task<User> RegisterAsync(User user)
    {
        var existingUser = await userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser != null)
        {
            throw new EmailAlreadyExistsException();
        }

        user.Password = passwordHasher.HashPassword(user, user.Password);

        return await userRepository.CreateUserAsync(user);
    }

    public async Task<User> LoginAsync(User user)
    {
        var existingUser = await userRepository.GetUserByEmailAsync(user.Email);
        if (existingUser == null)
        {
            throw new BadCredentialsException();
        }

        var passwordVerificationResult = passwordHasher.VerifyHashedPassword(existingUser, existingUser.Password, user.Password);
        if (passwordVerificationResult == PasswordVerificationResult.Failed)
        {
            throw new BadCredentialsException();
        }

        return existingUser;
    }

    public string GenerateAccessToken(User user)
    {
        return jwtService.GenerateToken(user.Id);
    }
}
