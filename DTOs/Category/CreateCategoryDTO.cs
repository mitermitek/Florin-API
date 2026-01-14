using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs.Category;

public record CreateCategoryDto
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; }
}
