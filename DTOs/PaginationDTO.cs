using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs;

/// <summary>
/// Represents a data transfer object for pagination
/// </summary>
public class PaginationDTO
{
    /// <summary>
    /// The current page number
    /// </summary>
    /// <remarks>
    /// Default is 1, must be greater than 0
    /// </remarks>
    [Range(1, int.MaxValue, ErrorMessage = "Page must be greater than 0")]
    public int Page { get; set; } = 1;

    /// <summary>
    /// The number of items per page
    /// </summary>
    /// <remarks>
    /// Default is 10, must be between 1 and 100
    /// </remarks>
    [Range(1, 100, ErrorMessage = "PageSize must be between 1 and 100")]
    public int PageSize { get; set; } = 10;
}
