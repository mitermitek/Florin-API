using Florin_API.Common;
using Florin_API.Entities;
using Florin_API.Exceptions.Category;
using Florin_API.Repositories.Interfaces;
using Florin_API.Services.Interfaces;

namespace Florin_API.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<ICollection<Category>> GetCategoriesByUserIdAsync(int userId, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoriesByUserIdAsync(userId, cancellationToken);
    }

    public async Task<Pagination<Category>> GetCategoriesByUserIdAsync(int userId, PaginationFilter paginationFilter, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoriesByUserIdAsync(userId, paginationFilter, cancellationToken);
    }

    public async Task<Category> GetCategoryByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetCategoryByIdAndUserIdAsync(id, userId, cancellationToken) ?? throw new CategoryNotFoundException();
    }

    public async Task<Category> CreateCategoryByUserIdAsync(int userId, Category category, CancellationToken cancellationToken)
    {
        await IsCategoryExistsByNameAndUserIdAsync(category.Name, userId, cancellationToken);

        category.UserId = userId;

        return await categoryRepository.CreateCategoryAsync(category, cancellationToken);
    }

    public async Task<Category> UpdateCategoryByIdAndUserIdAsync(int id, int userId, Category category, CancellationToken cancellationToken)
    {
        Category existingCategory = await GetCategoryByIdAndUserIdAsync(id, userId, cancellationToken);

        if (existingCategory.Name != category.Name)
        {
            await IsCategoryExistsByNameAndUserIdAsync(category.Name, userId, cancellationToken);
        }

        existingCategory.Name = category.Name;

        return await categoryRepository.UpdateCategoryAsync(existingCategory, cancellationToken);
    }

    public async Task DeleteCategoryByIdAndUserIdAsync(int id, int userId, CancellationToken cancellationToken)
    {
        Category existingCategory = await GetCategoryByIdAndUserIdAsync(id, userId, cancellationToken);

        await categoryRepository.DeleteCategoryAsync(existingCategory, cancellationToken);
    }

    public async Task IsCategoryExistsByNameAndUserIdAsync(string name, int userId, CancellationToken cancellationToken)
    {
        if (await categoryRepository.IsCategoryExistsByNameAndUserIdAsync(name, userId, cancellationToken))
        {
            throw new CategoryAlreadyExistsException();
        }
    }
}
