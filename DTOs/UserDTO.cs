using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs;

/// <summary>
/// Represents user data
/// </summary>
public class UserDTO
{
    /// <summary>
    /// Unique identifier of the user
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    /// User's first name
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string FirstName { get; set; }

    /// <summary>
    /// User's last name
    /// </summary>
    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string LastName { get; set; }

    /// <summary>
    /// User's email address
    /// </summary>
    [Required]
    [EmailAddress]
    [StringLength(254)]
    public required string Email { get; set; }
}
