using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models;

public class InventoryStock
{
    [Key]
    public int StockId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    [Required]
    public DateTime PurchaseDate { get; set; }

    [Required]
    public int Quantity { get; set; }

    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitCost { get; set; } // Dynamic wholesale price per batch

    // Navigation property
    public Product Product { get; set; }
}