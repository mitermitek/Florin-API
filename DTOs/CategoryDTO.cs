using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs;

/// <summary>
/// Represents a category in the application
/// </summary>
public class CategoryDTO
{
    /// <summary>
    /// Unique identifier for the category
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Name of the category
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    /// <summary>
    /// User ID associated with the category
    /// </summary>
    public int UserId { get; set; }
}
