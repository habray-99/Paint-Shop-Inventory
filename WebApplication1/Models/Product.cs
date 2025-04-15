using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models;

[Index(nameof(Name), IsUnique = false)] // Index for faster searching
[Index(nameof(Company), IsUnique = false)] // Index for faster searching
public class Product
{
    [Required]
    [StringLength(100)]
    public required string Name { get; set; }

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProductId { get; set; }

    [Required]
    [EnumDataType(typeof(ProductCategory))]
    public ProductCategory Category { get; set; } // Paint/Putty/Smart Care

    [Required]
    [EnumDataType(typeof(ProductVolume))]
    public ProductVolume Volume { get; set; } // Using enum for standardized volumes

    [Required]
    [StringLength(100)]
    // [Index(IsUnique = false)] // Index for faster filtering by company
    public required string Company { get; set; }

    // Navigation properties
    public virtual ICollection<InventoryStock>? InventoryStocks { get; set; }
    public virtual ICollection<OrderItem>? OrderItems { get; set; }
}

public enum ProductCategory
{
    Paint = 1,
    Putty = 2,
    SmartCare = 3
}

public enum ProductVolume
{
    [Display(Name = "1L")] Liter1 = 1,
    [Display(Name = "4L")] Liter4 = 4,
    [Display(Name = "10L")] Liter10 = 10,
    [Display(Name = "20L")] Liter20 = 20,
    [Display(Name = "40L")] Liter40 = 40,
    [Display(Name = "500ML")] MilliLiter500 = 500,
    [Display(Name = "200ML")] MilliLiter200 = 200,
    [Display(Name = "100ML")] MilliLiter100 = 100,
    [Display(Name = "50ML")] MilliLiter50 = 50,
    [Display(Name = "1Kg")] Kg1 = 1,
    [Display(Name = "5Kg")] Kg5 = 5,
    [Display(Name = "10Kg")] Kg10 = 10,
    [Display(Name = "20Kg")] Kg20 = 20,
    [Display(Name = "40Kg")] Kg40 = 40
}
