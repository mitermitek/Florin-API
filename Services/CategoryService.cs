using Florin_API.DTOs;
using Florin_API.Exceptions;
using Florin_API.Helpers;
using Florin_API.Interfaces;
using Florin_API.Models;

namespace Florin_API.Services;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<IEnumerable<Category>> GetUserCategoriesAsync(int userId)
    {
        return await categoryRepository.GetCategoriesByUserIdAsync(userId);
    }

    public async Task<PagedResultDTO<Category>> GetUserCategoriesAsync(int userId, int page, int pageSize)
    {
        var (skip, take) = PaginationHelper.CalculateSkipTake(page, pageSize);
        var (categories, totalCount) = await categoryRepository.GetCategoriesByUserIdAsync(userId, skip, take);
        
        return PaginationHelper.CreatePagedResult(categories, totalCount, page, pageSize);
    }

    public async Task<Category> GetCategoryByIdAndUserIdAsync(int categoryId, int userId)
    {
        var category = await categoryRepository.GetCategoryByIdAndUserIdAsync(categoryId, userId);
        if (category == null)
        {
            throw new CategoryNotFoundException($"Category with ID {categoryId} not found for user {userId}");
        }

        return category;
    }

    public async Task<Category> CreateCategoryAsync(Category category)
    {
        // Check if category name already exists for this user
        var nameExists = await categoryRepository.CategoryNameExistsAsync(category.Name, category.UserId);
        if (nameExists)
        {
            throw new DuplicateCategoryNameException($"A category with the name '{category.Name}' already exists for this user");
        }

        return await categoryRepository.CreateCategoryAsync(category);
    }

    public async Task<Category> UpdateCategoryAsync(Category existingCategory, Category categoryUpdate)
    {
        // Check if the new name already exists for this user (excluding the current category)
        var nameExists = await categoryRepository.CategoryNameExistsAsync(categoryUpdate.Name, existingCategory.UserId, existingCategory.Id);
        if (nameExists)
        {
            throw new DuplicateCategoryNameException($"A category with the name '{categoryUpdate.Name}' already exists for this user");
        }

        existingCategory.Name = categoryUpdate.Name;
        
        return await categoryRepository.UpdateCategoryAsync(existingCategory);
    }

    public async Task DeleteCategoryAsync(Category category)
    {
        await categoryRepository.DeleteCategoryAsync(category);
    }
}
