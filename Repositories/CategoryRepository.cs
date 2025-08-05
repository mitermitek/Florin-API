using Florin_API.Data;
using Florin_API.Interfaces;
using Florin_API.Models;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Repositories;

public class CategoryRepository(FlorinDbContext context) : ICategoryRepository
{
    public async Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId)
    {
        return await context.Categories
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<(IEnumerable<Category> categories, int totalCount)> GetCategoriesByUserIdAsync(int userId, int skip, int take)
    {
        var query = context.Categories
            .Where(c => c.UserId == userId)
            .OrderBy(c => c.Name);

        var totalCount = await query.CountAsync();
        var categories = await query
            .Skip(skip)
            .Take(take)
            .ToListAsync();

        return (categories, totalCount);
    }

    public async Task<Category?> GetCategoryByIdAndUserIdAsync(int categoryId, int userId)
    {
        return await context.Categories
            .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        context.Categories.Add(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task<Category> UpdateCategoryAsync(Category category)
    {
        context.Categories.Update(category);
        await context.SaveChangesAsync();
        return category;
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        context.Categories.Remove(category);
        await context.SaveChangesAsync();
    }

    public async Task<bool> CategoryExistsAsync(int categoryId, int userId)
    {
        return await context.Categories
            .AnyAsync(c => c.Id == categoryId && c.UserId == userId);
    }

    public async Task<bool> CategoryNameExistsAsync(string name, int userId)
    {
        var categories = await context.Categories
            .Where(c => c.UserId == userId)
            .Select(c => c.Name)
            .ToListAsync();

        return categories.Any(c => string.Equals(c, name, StringComparison.OrdinalIgnoreCase));
    }

    public async Task<bool> CategoryNameExistsAsync(string name, int userId, int excludeCategoryId)
    {
        var categories = await context.Categories
            .Where(c => c.UserId == userId && c.Id != excludeCategoryId)
            .Select(c => c.Name)
            .ToListAsync();

        return categories.Any(c => string.Equals(c, name, StringComparison.OrdinalIgnoreCase));
    }
}
