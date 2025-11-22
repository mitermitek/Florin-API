using Florin_API.Data;
using Florin_API.Entities;
using Florin_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class UserRepository(FlorinDbContext ctx) : IUserRepository
{
    public async Task<User> CreateUserAsync(User user)
    {
        ctx.Users.Add(user);
        await ctx.SaveChangesAsync();

        return user;
    }

    public async Task<bool> IsUserExistsByEmailAsync(string email)
    {
        return await ctx.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await ctx.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await ctx.Users.FindAsync(id);
    }
}
