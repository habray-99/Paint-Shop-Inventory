using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Index(nameof(CustomerId), IsUnique = false)]
[Index(nameof(DateAd), IsUnique = false)]
[Index(nameof(DateBs), IsUnique = false)]
public class Order
{
    [Key]
    public int OrderId { get; set; }

    [ForeignKey("Customer")]
    public int CustomerId { get; set; }

    [Column(TypeName = "date")]
    public DateTime DateAd { get; set; }

    [BsDateFormat]
    [Column(TypeName = "Text")]
    [MaxLength(20)]
    public string? DateBs { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal PendingAmount { get; set; }

    // Navigation
    public Customer? Customer { get; set; }
    public ICollection<OrderItem>? OrderItems { get; set; }
    public ICollection<Payment>? Payments { get; set; }
}
