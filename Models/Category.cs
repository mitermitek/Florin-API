using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Florin_API.Models;

public class Category
{
    [Key]
    public required int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 3)]
    public required string Name { get; set; }

    [Required]
    public required int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }

    public ICollection<Transaction> Transactions { get; set; } = [];
}
