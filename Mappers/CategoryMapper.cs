using Florin_API.Common;
using Florin_API.DTOs.Category;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class CategoryMapper
{
    public static Category ToEntity(this CreateCategoryDto dto)
    {
        return new Category
        {
            Name = dto.Name
        };
    }

    public static Category ToEntity(this UpdateCategoryDto dto)
    {
        return new Category
        {
            Name = dto.Name
        };
    }

    public static CategoryDto ToDto(this Category entity)
    {
        return new CategoryDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static Pagination<CategoryDto> ToDto(this Pagination<Category> pagination)
    {
        return new Pagination<CategoryDto>
        {
            Items = pagination.Items.ToDtos(),
            Total = pagination.Total
        };
    }

    public static ICollection<CategoryDto> ToDtos(this ICollection<Category> entities)
    {
        return [.. entities.Select(e => e.ToDto())];
    }
}
