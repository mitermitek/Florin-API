using Florin_API.Data;
using Florin_API.Interfaces;
using Florin_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class UserRepository(FlorinDbContext context) : IUserRepository
{
    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await context.Users.FirstOrDefaultAsync(user => user.Id == id);
    }

    public async Task<User> CreateUserAsync(User user)
    {
        context.Users.Add(user);
        await context.SaveChangesAsync();

        return user;
    }
}
