using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs.Account;

public record UpdateAccountDto
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; init; }

    public decimal? StartingBalance { get; init; }
}
