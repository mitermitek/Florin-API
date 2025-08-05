using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Florin_API.Models;

public class RefreshToken
{
    [Key]
    public required int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public required string Token { get; set; }

    [Required]
    public DateTime ExpiresAt { get; set; } = DateTime.UtcNow.AddDays(7);

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? RevokedAt { get; set; }

    [Required]
    public required int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }

    public bool IsActive => ExpiresAt > DateTime.UtcNow && (RevokedAt == null || RevokedAt > DateTime.UtcNow);
}
