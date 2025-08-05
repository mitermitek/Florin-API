using System.ComponentModel.DataAnnotations;

namespace Florin_API.Models;

public class User
{
    [Key]
    public required int Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 3)]
    public required string LastName { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(254)]
    public required string Email { get; set; }

    [Required]
    [StringLength(255, MinimumLength = 8)]
    public required string Password { get; set; }

    public ICollection<Category> Categories { get; set; } = [];
    public ICollection<Transaction> Transactions { get; set; } = [];
}
