using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal AmountPaid { get; set; }

    [Required]
    public DateTime PaymentDate { get; set; }

    // Navigation property
    public Order Order { get; set; }
}