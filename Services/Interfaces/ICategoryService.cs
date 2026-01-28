using Florin_API.Common;
using Florin_API.Entities;

namespace Florin_API.Services.Interfaces;

public interface ICategoryService
{
    Task<ICollection<Category>> GetCategoriesByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<Pagination<Category>> GetCategoriesByUserIdAsync(int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken);
    Task<Category> GetCategoryByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken);
    Task<Category> CreateCategoryByUserIdAsync(int userId, Category category, CancellationToken cancellationToken);
    Task<Category> UpdateCategoryByIdAndUserIdAsync(int id, int userId, Category category, CancellationToken cancellationToken);
    Task DeleteCategoryByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken);
    Task IsCategoryExistsByNameAndUserIdAsync(string name, int userId, CancellationToken cancellationToken);
}
