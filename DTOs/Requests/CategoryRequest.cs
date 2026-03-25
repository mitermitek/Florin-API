using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs.Requests;

public record CategoryRequest
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; }
}
