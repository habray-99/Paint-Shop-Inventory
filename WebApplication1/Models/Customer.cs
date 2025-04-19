using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

[Index(nameof(Phone), IsUnique = true)]
[Index(nameof(Name), IsUnique = false)]
public class Customer
{
    [Key]
    public int CustomerId { get; set; }

    [Required, MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(15)]
    public string Phone { get; set; }

    [MaxLength(50)]
    public string Address { get; set; }

    // Navigation
    public ICollection<Order> Orders { get; set; }
}
