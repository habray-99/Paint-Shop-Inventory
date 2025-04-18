using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;
[Index(nameof(Name), IsUnique = false)]
[Index(nameof(ShadeSlug), IsUnique = false)]
[Index(nameof(Company), IsUnique = false)]
[Index(nameof(Category), IsUnique = false)]
public class Product
{
    [Key]
    public int ProductId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public ProductCategory Category { get; set; }

    [Required]
    public ProductVolume Volume { get; set; }

    [Required, MaxLength(100)]
    public string Company { get; set; }

    [Required, MaxLength(50)]
    public string ShadeSlug { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal WholesalePrice { get; set; }

    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public decimal RetailPrice { get; set; }

    // Navigation
    public ICollection<InventoryStock> InventoryStocks { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
