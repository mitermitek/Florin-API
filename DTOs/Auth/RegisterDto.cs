using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs.Auth;

public record RegisterDto
{
    [Required]
    [MinLength(3), MaxLength(100)]
    public required string FirstName { get; init; }

    [Required]
    [MinLength(3), MaxLength(100)]
    public required string LastName { get; init; }

    [Required]
    [EmailAddress]
    [MinLength(5), MaxLength(255)]
    public required string Email { get; init; }

    [Required]
    [MinLength(8)]
    public required string Password { get; init; }

    [Required]
    [Compare("Password", ErrorMessage = "Passwords do not match.")]
    public required string PasswordConfirmation { get; init; }
}
