using Florin_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Data;

public class FlorinDbContext(DbContextOptions<FlorinDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
