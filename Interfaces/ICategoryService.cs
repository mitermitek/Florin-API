using Florin_API.DTOs;
using Florin_API.Models;

namespace Florin_API.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<Category>> GetUserCategoriesAsync(int userId);
    Task<PagedResultDTO<Category>> GetUserCategoriesAsync(int userId, int page, int pageSize);
    Task<Category> GetCategoryByIdAndUserIdAsync(int categoryId, int userId);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category existingCategory, Category categoryUpdate);
    Task DeleteCategoryAsync(Category category);
}
