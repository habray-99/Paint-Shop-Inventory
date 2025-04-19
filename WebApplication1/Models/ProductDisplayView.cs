using Microsoft.EntityFrameworkCore;

[Keyless]
public class ProductDisplayView
{
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string CategoryName { get; set; }
    public string VolumeDisplay { get; set; }
    public string Company { get; set; }
    public string ShadeSlug { get; set; }
    public decimal WholesalePrice { get; set; }
    public decimal RetailPrice { get; set; }
}
