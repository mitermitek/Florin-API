using Florin_API.Entities;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Data;

public class FlorinDbContext(DbContextOptions<FlorinDbContext> opt) : DbContext(opt)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
}
