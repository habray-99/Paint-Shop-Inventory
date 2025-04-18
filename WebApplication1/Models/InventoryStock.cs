using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models;

[Index(nameof(ProductId), IsUnique = false)]
[Index(nameof(PurchaseDateAd), IsUnique = false)]
public class InventoryStock
{
    [Key]
    public int BatchId { get; set; }

    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public int Quantity { get; set; }
    
    [Column(TypeName = "dateAD")]
    public DateTime PurchaseDateAd { get; set; }
    [Column(TypeName = "dateBS")]
    [MaxLength(20)]
    public string PurchaseDateBs { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal CostPrice { get; set; }
    
    // Navigation
    public Product Product { get; set; }
}