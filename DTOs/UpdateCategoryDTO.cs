using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs;

/// <summary>
/// Represents a data transfer object for updating an existing category
/// </summary>
public class UpdateCategoryDTO
{
    /// <summary>
    /// Name of the category
    /// </summary>
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }
}
