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
        public async Task<IActionResult> GetCategories([FromQuery] PaginationFilter? paginationFilter, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();

            if (paginationFilter is not null)
            {
                var pagedCategories = await categoryService.GetCategoriesByUserIdAsync(userId, paginationFilter, cancellationToken);
                var pagedCategoriesDto = CategoryMapper.ToDto(pagedCategories);

                return Ok(pagedCategoriesDto);
            }

            var categories = await categoryService.GetCategoriesByUserIdAsync(userId, cancellationToken);
            var categoriesDto = CategoryMapper.ToDtos(categories);

            return Ok(categoriesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var category = await categoryService.GetCategoryByIdAndUserIdAsync(id, userId, cancellationToken);
            var categoryDto = CategoryMapper.ToDto(category);

            return Ok(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequestDto categoryRequestDto, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var categoryToCreate = categoryRequestDto.ToEntity();
            var createdCategory = await categoryService.CreateCategoryByUserIdAsync(userId, categoryToCreate, cancellationToken);
            var categoryDto = CategoryMapper.ToDto(createdCategory);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = categoryDto.Id },
                categoryDto
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequestDto categoryRequestDto, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var categoryToUpdate = categoryRequestDto.ToEntity();
            var updatedCategory = await categoryService.UpdateCategoryByIdAndUserIdAsync(id, userId, categoryToUpdate, cancellationToken);
            var categoryDto = CategoryMapper.ToDto(updatedCategory);

            return Ok(categoryDto);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            await categoryService.DeleteCategoryByIdAndUserIdAsync(id, userId, cancellationToken);

            return NoContent();
        }
    }
}
