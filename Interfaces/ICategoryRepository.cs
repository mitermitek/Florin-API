using Florin_API.Models;

namespace Florin_API.Interfaces;

public interface ICategoryRepository
{
    Task<IEnumerable<Category>> GetCategoriesByUserIdAsync(int userId);
    Task<(IEnumerable<Category> categories, int totalCount)> GetCategoriesByUserIdAsync(int userId, int skip, int take);
    Task<Category?> GetCategoryByIdAndUserIdAsync(int categoryId, int userId);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
    Task<bool> CategoryExistsAsync(int categoryId, int userId);
    Task<bool> CategoryNameExistsAsync(string name, int userId);
    Task<bool> CategoryNameExistsAsync(string name, int userId, int excludeCategoryId);
}
