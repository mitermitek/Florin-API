using AutoMapper;
using Florin_API.DTOs;
using Florin_API.Interfaces;
using Florin_API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Florin_API.Controllers;

[Route("Categories")]
[Authorize]
public class CategoryController(IMapper mapper, ICategoryService categoryService, ICurrentUserService currentUserService) : ApiBaseController
{
    /// <summary>
    /// Gets all categories for the authenticated user
    /// </summary>
    /// <param name="page">Page number (default: 1)</param>
    /// <param name="pageSize">Number of items per page (default: 10, max: 100)</param>
    /// <returns>Paginated list of user's categories</returns>
    /// <response code="200">Categories retrieved successfully</response>
    /// <response code="400">Invalid pagination parameters</response>
    /// <response code="401">User not authenticated</response>
    [HttpGet]
    [ProducesResponseType(typeof(PagedResultDTO<CategoryDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetCategories([FromQuery] PaginationDTO pagination)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var pagedCategories = await categoryService.GetUserCategoriesAsync(currentUserService.UserId, pagination.Page, pagination.PageSize);

        var result = new PagedResultDTO<CategoryDTO>
        {
            Items = mapper.Map<IEnumerable<CategoryDTO>>(pagedCategories.Items),
            TotalCount = pagedCategories.TotalCount,
            Page = pagedCategories.Page,
            PageSize = pagedCategories.PageSize
        };

        return Ok(result);
    }

    /// <summary>
    /// Gets a specific category by ID for the authenticated user
    /// </summary>
    /// <param name="id">Category ID</param>
    /// <returns>The requested category</returns>
    /// <response code="200">Category retrieved successfully</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">Category not found</response>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetCategory(int id)
    {
        var category = await categoryService.GetCategoryByIdAndUserIdAsync(id, currentUserService.UserId);
        var categoryDTO = mapper.Map<CategoryDTO>(category);

        return Ok(categoryDTO);
    }

    /// <summary>
    /// Creates a new category for the authenticated user
    /// </summary>
    /// <param name="createCategoryDto">Category creation data</param>
    /// <returns>The created category</returns>
    /// <response code="201">Category created successfully</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="401">User not authenticated</response>
    [HttpPost]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationErrorResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDTO createCategoryDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var category = mapper.Map<Category>(createCategoryDto);
        category.UserId = currentUserService.UserId;
        var createdCategory = await categoryService.CreateCategoryAsync(category);
        var categoryDTO = mapper.Map<CategoryDTO>(createdCategory);

        return CreatedAtAction(nameof(GetCategory), new { id = categoryDTO.Id }, categoryDTO);
    }

    /// <summary>
    /// Updates an existing category for the authenticated user
    /// </summary>
    /// <param name="id">Category ID</param>
    /// <param name="updateCategoryDto">Category update data</param>
    /// <returns>The updated category</returns>
    /// <response code="200">Category updated successfully</response>
    /// <response code="400">Invalid input data</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">Category not found</response>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(CategoryDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationErrorResponseDTO), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateCategory(int id, [FromBody] UpdateCategoryDTO updateCategoryDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var existingCategory = await categoryService.GetCategoryByIdAndUserIdAsync(id, currentUserService.UserId);
        var categoryUpdate = mapper.Map<Category>(updateCategoryDto);
        var updatedCategory = await categoryService.UpdateCategoryAsync(existingCategory, categoryUpdate);
        var categoryDTO = mapper.Map<CategoryDTO>(updatedCategory);

        return Ok(categoryDTO);
    }

    /// <summary>
    /// Deletes a category for the authenticated user
    /// </summary>
    /// <param name="id">Category ID</param>
    /// <returns>No content</returns>
    /// <response code="204">Category deleted successfully</response>
    /// <response code="401">User not authenticated</response>
    /// <response code="404">Category not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorResponseDTO), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCategory(int id)
    {
        var category = await categoryService.GetCategoryByIdAndUserIdAsync(id, currentUserService.UserId);
        await categoryService.DeleteCategoryAsync(category);

        return NoContent();
    }
}
