using Florin_API.DTOs;

namespace Florin_API.Helpers;

public static class PaginationHelper
{
    public static PagedResultDTO<T> CreatePagedResult<T>(IEnumerable<T> items, int totalCount, int page, int pageSize)
    {
        return new PagedResultDTO<T>
        {
            Items = items,
            TotalCount = totalCount,
            Page = page,
            PageSize = pageSize
        };
    }

    public static (int skip, int take) CalculateSkipTake(int page, int pageSize)
    {
        var skip = (page - 1) * pageSize;
        return (skip, pageSize);
    }
}
