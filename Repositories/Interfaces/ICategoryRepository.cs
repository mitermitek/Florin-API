using Florin_API.Common;
using Florin_API.Entities;

namespace Florin_API.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetCategoriesByUserIdAsync(int userId);
    Task<Pagination<Category>> GetCategoriesByUserIdAsync(int userId, PaginationFilter paginationFilter);
    Task<Category?> GetCategoryByIdAndUserIdAsync(int id, int userId);
    Task<Category> CreateCategoryAsync(Category category);
    Task<Category> UpdateCategoryAsync(Category category);
    Task DeleteCategoryAsync(Category category);
    Task<bool> IsCategoryExistsByNameAndUserIdAsync(string name, int userId);
}
