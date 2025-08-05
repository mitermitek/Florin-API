using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs;

/// <summary>
/// Represents a data transfer object for user registration
/// </summary>
public class RegisterDTO
{
    /// <summary>
    /// User's first name
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string FirstName { get; set; }

    /// <summary>
    /// User's last name
    /// </summary>
    /// <remarks>
    /// Must be at least 3 characters long
    /// </remarks>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string LastName { get; set; }

    /// <summary>
    /// User's email address
    /// </summary>
    /// <remarks>
    /// Must be a valid email format and not exceed 254 characters
    /// </remarks>
    [Required]
    [EmailAddress]
    [StringLength(254)]
    public required string Email { get; set; }

    /// <summary>
    /// User's password
    /// </summary>
    /// <remarks>
    /// Must be at least 6 characters long
    /// </remarks>
    [Required]
    [StringLength(255, MinimumLength = 6)]
    [DataType(DataType.Password)]
    public required string Password { get; set; }

    /// <summary>
    /// Confirmation of the user's password
    /// </summary>
    /// <remarks>
    /// Must match the Password field
    /// </remarks>
    [Required]
    [StringLength(255, MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public required string ConfirmPassword { get; set; }
}
