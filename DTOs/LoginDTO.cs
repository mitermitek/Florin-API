using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs;

/// <summary>
/// Represents a data transfer object for user login
/// </summary>
public class LoginDTO
{
    /// <summary>
    /// User's email address
    /// </summary>
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    /// <summary>
    /// User's password
    /// </summary>
    [Required]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
