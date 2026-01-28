using Florin_API.Common;
using Florin_API.Entities;

namespace Florin_API.Repositories.Interfaces;

public interface ICategoryRepository
{
    Task<ICollection<Category>> GetCategoriesByUserIdAsync(int userId, CancellationToken cancellationToken);
    Task<Pagination<Category>> GetCategoriesByUserIdAsync(int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken);
    Task<Category?> GetCategoryByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken);
    Task<Category> CreateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<Category> UpdateCategoryAsync(Category category, CancellationToken cancellationToken);
    Task DeleteCategoryAsync(Category category, CancellationToken cancellationToken);
    Task<bool> IsCategoryExistsByNameAndUserIdAsync(string name, int userId, CancellationToken cancellationToken);
}
