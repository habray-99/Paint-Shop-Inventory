using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class Order
{
    [Key] public int OrderId { get; set; }

    [ForeignKey("Customer")] public int CustomerId { get; set; }

    [Required] public DateTime OrderDateAd { get; set; }

    [Required] [StringLength(20)] public string OrderDateBs { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal PendingAmount { get; set; }

    // Navigation properties
    public Customer Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<Payment> Payments { get; set; }
}