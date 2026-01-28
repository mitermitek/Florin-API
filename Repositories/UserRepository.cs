using Florin_API.Data;
using Florin_API.Entities;
using Florin_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class UserRepository(FlorinDbContext ctx) : IUserRepository
{
    public async Task<User> CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        ctx.Users.Add(user);
        await ctx.SaveChangesAsync(cancellationToken);

        return user;
    }

    public async Task<bool> IsUserExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await ctx.Users.AnyAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
    {
        return await ctx.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
    }

    public async Task<User?> GetUserByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await ctx.Users.FindAsync(id, cancellationToken);
    }
}
