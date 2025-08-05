using System.ComponentModel.DataAnnotations;

namespace Florin_API.DTOs;

/// <summary>
/// Represents a data transfer object for refresh tokens
/// </summary>
public class RefreshTokenDTO
{
    /// <summary>
    /// User's refresh token
    /// </summary>
    [Required]
    public required string Token { get; set; }
}
