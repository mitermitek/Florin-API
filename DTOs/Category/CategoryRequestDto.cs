using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs.Category;

public record CategoryRequestDto
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; }
}
