using Florin_API.Exceptions;
using Florin_API.Interfaces;
using Florin_API.Models;

namespace Florin_API.Services;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<User> GetUserByIdAsync(int id)
    {
        var user = await userRepository.GetUserByIdAsync(id);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        return user;
    }
}
