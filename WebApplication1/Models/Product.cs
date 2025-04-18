using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Index(nameof(ShadeSlug)), isUnique=false]
[Index(nameof(Company)), isUnique=false]
[Index(nameof(Category)), isUnique=false]
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

public enum ProductCategory
{
    Paint = 1,
    Putty = 2,
    SmartCare = 3
}

public enum ProductVolume
{
    [Display(Name = "500ML")] MilliLiter500 = 500,
    [Display(Name = "200ML")] MilliLiter200 = 200,
    [Display(Name = "100ML")] MilliLiter100 = 100,
    [Display(Name = "50ML")] MilliLiter50 = 50,
    [Display(Name = "1L")] Liter1 = 1,
    [Display(Name = "4L")] Liter4 = 4,
    [Display(Name = "10L")] Liter10 = 10,
    [Display(Name = "20L")] Liter20 = 20,
    [Display(Name = "40L")] Liter40 = 40,
    //[Display(Name = "1Kg")] Kg1 = 1,
    //[Display(Name = "5Kg")] Kg5 = 5,
    //[Display(Name = "10Kg")] Kg10 = 10,
    //[Display(Name = "20Kg")] Kg20 = 20,
    //[Display(Name = "40Kg")] Kg40 = 40
    [Display(Name = "1Kg")] Kg1 = 1001,  // Different numbering scheme
    [Display(Name = "5Kg")] Kg5 = 1005,
    [Display(Name = "10Kg")] Kg10 = 1010,
    [Display(Name = "20Kg")] Kg20 = 1020,
    [Display(Name = "40Kg")] Kg40 = 1040
}
