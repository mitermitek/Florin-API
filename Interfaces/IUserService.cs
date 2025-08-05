using Florin_API.Models;

namespace Florin_API.Interfaces;

public interface IUserService
{
    Task<User> GetUserByIdAsync(int id);
}
