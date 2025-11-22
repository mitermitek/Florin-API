using Florin_API.DTOs.Category;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class CategoryMapper
{
    public static Category ToEntity(this CreateCategoryDTO dto)
    {
        return new Category
        {
            Name = dto.Name
        };
    }

    public static Category ToEntity(this UpdateCategoryDTO dto)
    {
        return new Category
        {
            Name = dto.Name
        };
    }

    public static CategoryDTO ToDTO(this Category entity)
    {
        return new CategoryDTO
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static ICollection<CategoryDTO> ToDTOs(this ICollection<Category> entities)
    {
        return [.. entities.Select(e => e.ToDTO())];
    }
}
