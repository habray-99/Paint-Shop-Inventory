using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Index(nameof(OrderId))]
[Index(nameof(PaymentDateAd))]
[Index(nameof(PaymentDateBs))]
public class Payment
{
    [Key]
    public int PaymentId { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal Amount { get; set; }

    [Column(TypeName = "date")]
    public DateTime PaymentDateAd { get; set; }
    [Column(TypeName = "TEXT")]
    public string PaymentDateBs { get; set; }

    public bool IsAdvance { get; set; }

    // Navigation
    public Order Order { get; set; }
}
