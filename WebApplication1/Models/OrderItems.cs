using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class OrderItem
{
    [Key]
    public int ItemId { get; set; }

    [ForeignKey("Order")]
    public int OrderId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [StringLength(50)]
    public string PaintShade { get; set; } // Slug format

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal ExciseDuty { get; set; } // 7% of batch cost

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Vat { get; set; } // 13% of (cost + excise)

    // Navigation properties
    public Order Order { get; set; }
    public Product Product { get; set; }
}