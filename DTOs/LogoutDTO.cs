using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs;

/// <summary>
/// Represents a data transfer object for user logout
/// </summary>
public class LogoutDTO
{
    /// <summary>
    /// User's refresh token
    /// </summary>
    [Required]
    public required string RefreshToken { get; set; }
}
