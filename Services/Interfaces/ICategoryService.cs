using Florin_API.Entities;

namespace Florin_API.Services.Interfaces;

public interface ICategoryService
{
    Task<ICollection<Category>> GetCategoriesByUserIdAsync(int userId);
    Task<Category> GetCategoryByIdAndUserIdAsync(int id, int userId);
    Task<Category> CreateCategoryByUserIdAsync(int userId, Category category);
    Task<Category> UpdateCategoryByIdAndUserIdAsync(int id, int userId, Category category);
    Task DeleteCategoryByIdAndUserIdAsync(int id, int userId);
    Task IsCategoryExistsByNameAndUserIdAsync(string name, int userId);
}
