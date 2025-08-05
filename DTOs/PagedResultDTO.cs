namespace Florin_API.DTOs;

// <summary>
// Represents a paginated result set for data transfer objects
// </summary>
public class PagedResultDTO<T>
{
    /// <summary>
    /// List of items in the current page
    /// </summary>
    public IEnumerable<T> Items { get; set; } = [];

    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Current page number
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Size of each page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// Indicates if there is a next page
    /// </summary>
    public bool HasNextPage => Page < TotalPages;

    /// <summary>
    /// Indicates if there is a previous page
    /// </summary>
    public bool HasPreviousPage => Page > 1;
}
