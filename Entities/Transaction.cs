using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Florin_API.Enums;

namespace Florin_API.Entities;

public class Transaction
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int AccountId { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [Required]
    public TransactionType Type { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Required]
    public DateTime Date { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }


    [ForeignKey(nameof(AccountId))]
    public virtual Account Account { get; set; } = null!;

    [ForeignKey(nameof(CategoryId))]
    public virtual Category Category { get; set; } = null!;
}
