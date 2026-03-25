using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs.Requests;

public record LoginRequest
{
    [Required]
    public required string Email { get; init; }

    [Required]
    public required string Password { get; init; }

    public bool RememberMe { get; init; } = false;
}
