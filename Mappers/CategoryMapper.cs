using Florin_API.Common;
using Florin_API.DTOs.Requests;
using Florin_API.DTOs.Responses;
using Florin_API.Entities;

namespace Florin_API.Mappers;

public static class CategoryMapper
{
    public static Category ToEntity(this CategoryRequest request)
    {
        return new Category
        {
            Name = request.Name
        };
    }

    public static CategoryResponse ToResponse(this Category entity)
    {
        return new CategoryResponse
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public static Pagination<CategoryResponse> ToResponse(this Pagination<Category> pagination)
    {
        return new Pagination<CategoryResponse>
        {
            Items = pagination.Items.ToResponses(),
            Total = pagination.Total
        };
    }

    public static ICollection<CategoryResponse> ToResponses(this ICollection<Category> entities)
    {
        return [.. entities.Select(e => e.ToResponse())];
    }
}
