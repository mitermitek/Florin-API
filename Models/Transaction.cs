using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Florin_API.Enums;

namespace Florin_API.Models;

public class Transaction
{
    [Key]
    public required int Id { get; set; }

    [Required]
    public required TransactionType Type { get; set; }

    [Required]
    public required DateTime Date { get; set; }

    [Required]
    public required decimal Amount { get; set; }

    [StringLength(255)]
    public string? Description { get; set; }

    [Required]
    public required int CategoryId { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public required Category Category { get; set; }

    [Required]
    public required int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public required User User { get; set; }
}
