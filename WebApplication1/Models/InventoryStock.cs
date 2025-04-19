using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using NepaliDateConverter.Net;

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
    
    [Column(TypeName = "date")]
    public DateTime PurchaseDateAd { get; set; }
    [Column(TypeName = "TEXT")]
    [MaxLength(20)]
    [BsDateFormat]
    public string PurchaseDateBs { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal CostPrice { get; set; }
    
    // Navigation
    public Product? Product { get; set; }
}