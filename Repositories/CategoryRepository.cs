using Florin_API.Common;
using Florin_API.Data;
using Florin_API.Entities;
using Florin_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class CategoryRepository(FlorinDbContext ctx) : ICategoryRepository
{
    public async Task<ICollection<Category>> GetCategoriesByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await ctx.Categories
            .Where(c => c.UserId == userId)
            .ToListAsync(cancellationToken);
    }

    public async Task<Pagination<Category>> GetCategoriesByUserIdAsync(int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken)
    {
        var query = ctx.Categories.Where(c => c.UserId == userId);

        var total = await query.CountAsync();
        var items = await query
            .OrderBy(c => c.Name)
            .Skip((paginationFilter.Page - 1) * paginationFilter.Size)
            .Take(paginationFilter.Size)
            .ToListAsync(cancellationToken);

        return new Pagination<Category>
        {
            Items = items,
            Total = total
        };
    }

    public async Task<Category?> GetCategoryByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken)
    {
        return await ctx.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId, cancellationToken);
    }

    public async Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        ctx.Categories.Add(category);
        await ctx.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        ctx.Categories.Update(category);
        await ctx.SaveChangesAsync(cancellationToken);

        return category;
    }

    public async Task DeleteCategoryAsync(Category category, CancellationToken cancellationToken)
    {
        ctx.Categories.Remove(category);
        await ctx.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsCategoryExistsByNameAndUserIdAsync(string name, int userId, CancellationToken cancellationToken)
    {
        return await ctx.Categories.AnyAsync(c => c.Name == name && c.UserId == userId, cancellationToken);
    }
}
