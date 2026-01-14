using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs.Auth;

public record LoginDto
{
    [Required]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }

    public bool RememberMe { get; init; } = false;
}
