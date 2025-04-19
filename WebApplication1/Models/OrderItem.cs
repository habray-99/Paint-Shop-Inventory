using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Index(nameof(OrderId), IsUnique = false)]
[Index(nameof(ProductId), IsUnique = false)]
public class OrderItem
{
    [Key]
    public int ItemId { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ExciseDuty { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal VAT { get; set; }

    // Navigation
    public Order Order { get; set; }
    public Product Product { get; set; }
}
