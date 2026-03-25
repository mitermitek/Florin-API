using Florin_API.Common;
using Florin_API.DTOs.Requests;
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
                var pagedCategoriesResponse = CategoryMapper.ToResponse(pagedCategories);

                return Ok(pagedCategoriesResponse);
            }

            var categories = await categoryService.GetCategoriesByUserIdAsync(userId, cancellationToken);
            var categoriesResponse = CategoryMapper.ToResponses(categories);

            return Ok(categoriesResponse);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(int id, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var category = await categoryService.GetCategoryByIdAndUserIdAsync(id, userId, cancellationToken);
            var categoryResponse = CategoryMapper.ToResponse(category);

            return Ok(categoryResponse);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryRequest categoryRequest, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var categoryToCreate = categoryRequest.ToEntity();
            var createdCategory = await categoryService.CreateCategoryByUserIdAsync(userId, categoryToCreate, cancellationToken);
            var categoryResponse = CategoryMapper.ToResponse(createdCategory);

            return CreatedAtAction(
                nameof(GetCategoryById),
                new { id = categoryResponse.Id },
                categoryResponse
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, [FromBody] CategoryRequest categoryRequest, CancellationToken cancellationToken)
        {
            var userId = userContextService.GetCurrentUserId();
            var categoryToUpdate = categoryRequest.ToEntity();
            var updatedCategory = await categoryService.UpdateCategoryByIdAndUserIdAsync(id, userId, categoryToUpdate, cancellationToken);
            var categoryResponse = CategoryMapper.ToResponse(updatedCategory);

            return Ok(categoryResponse);
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
