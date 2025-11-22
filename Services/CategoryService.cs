using Florin_API.Common;
using Florin_API.Entities;
using Florin_API.Exceptions.Category;
using Florin_API.Repositories.Interfaces;
using Florin_API.Services.Interfaces;

namespace Florin_API.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<ICollection<Category>> GetCategoriesByUserIdAsync(int userId)
    {
        return await categoryRepository.GetCategoriesByUserIdAsync(userId);
    }

    public async Task<Pagination<Category>> GetCategoriesByUserIdAsync(int userId, PaginationFilter paginationFilter)
    {
        return await categoryRepository.GetCategoriesByUserIdAsync(userId, paginationFilter);
    }

    public async Task<Category> GetCategoryByIdAndUserIdAsync(int id, int userId)
    {
        return await categoryRepository.GetCategoryByIdAndUserIdAsync(id, userId) ?? throw new CategoryNotFoundException();
    }

    public async Task<Category> CreateCategoryByUserIdAsync(int userId, Category category)
    {
        await IsCategoryExistsByNameAndUserIdAsync(category.Name, userId);

        category.UserId = userId;

        return await categoryRepository.CreateCategoryAsync(category);
    }

    public async Task<Category> UpdateCategoryByIdAndUserIdAsync(int id, int userId, Category category)
    {
        Category existingCategory = await GetCategoryByIdAndUserIdAsync(id, userId);

        if (existingCategory.Name != category.Name)
        {
            await IsCategoryExistsByNameAndUserIdAsync(category.Name, userId);
        }

        existingCategory.Name = category.Name;

        return await categoryRepository.UpdateCategoryAsync(existingCategory);
    }

    public async Task DeleteCategoryByIdAndUserIdAsync(int id, int userId)
    {
        Category existingCategory = await GetCategoryByIdAndUserIdAsync(id, userId);

        await categoryRepository.DeleteCategoryAsync(existingCategory);
    }

    public async Task IsCategoryExistsByNameAndUserIdAsync(string name, int userId)
    {
        if (await categoryRepository.IsCategoryExistsByNameAndUserIdAsync(name, userId))
        {
            throw new CategoryAlreadyExistsException();
        }
    }
}
