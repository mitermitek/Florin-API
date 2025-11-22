using Florin_API.Data;
using Florin_API.Entities;
using Florin_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class CategoryRepository(FlorinDbContext ctx) : ICategoryRepository
{
    public async Task<ICollection<Category>> GetCategoriesByUserIdAsync(int userId)
    {
        return await ctx.Categories
            .Where(c => c.UserId == userId)
            .ToListAsync();
    }

    public async Task<Category?> GetCategoryByIdAndUserIdAsync(int id, int userId)
    {
        return await ctx.Categories.FirstOrDefaultAsync(c => c.Id == id && c.UserId == userId);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        ctx.Categories.Add(category);
        await ctx.SaveChangesAsync();

        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        ctx.Categories.Update(category);
        await ctx.SaveChangesAsync();

        return category;
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        ctx.Categories.Remove(category);
        await ctx.SaveChangesAsync();
    }

    public async Task<bool> IsCategoryExistsByNameAndUserIdAsync(string name, int userId)
    {
        return await ctx.Categories.AnyAsync(c => c.Name == name && c.UserId == userId);
    }
}
