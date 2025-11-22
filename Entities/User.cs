using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Florin_API.Entities;

[Index(nameof(Email), IsUnique = true)]
public class User
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(255)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public virtual ICollection<Account> Accounts { get; set; } = [];
    public virtual ICollection<Category> Categories { get; set; } = [];
}
