using Florin_API.Common;
using Florin_API.DTOs.Category;
using Florin_API.Mappers;
using Florin_API.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    [Authorize]
    public class CategoryController(IUserContextService userContextService, ICategoryService categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] PaginationFilter? paginationFilter)
        {
            var userId = userContextService.GetCurrentUserId();

            if (paginationFilter is not null)
            {
                var pagedCategories = await categoryService.GetCategoriesByUserIdAsync(userId, paginationFilter);
                var pagedCategoriesDto = CategoryMapper.ToDto(pagedCategories);

                return Ok(pagedCategoriesDto);
            }

            var categories = await categoryService.GetCategoriesByUserIdAsync(userId);
            var categoriesDto = CategoryMapper.ToDtos(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var userId = userContextService.GetCurrentUserId();
            var category = await categoryService.GetCategoryByIdAndUserIdAsync(id, userId);
            var categoryDto = CategoryMapper.ToDto(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDto categoryRequestDto)
        {
            var userId = userContextService.GetCurrentUserId();
            var categoryToCreate = categoryRequestDto.ToEntity();
            var createdCategory = await categoryService.CreateCategoryByUserIdAsync(userId, categoryToCreate);
            var categoryDto = CategoryMapper.ToDto(createdCategory);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = categoryDto.Id },
                categoryDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequestDto categoryRequestDto)
        {
            var userId = userContextService.GetCurrentUserId();
            var categoryToUpdate = categoryRequestDto.ToEntity();
            var updatedCategory = await categoryService.UpdateCategoryByIdAndUserIdAsync(id, userId, categoryToUpdate);
            var categoryDto = CategoryMapper.ToDto(updatedCategory);

            return Ok(categoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var userId = userContextService.GetCurrentUserId();
            await categoryService.DeleteCategoryByIdAndUserIdAsync(id, userId);

            return NoContent();
        }
    }
}
