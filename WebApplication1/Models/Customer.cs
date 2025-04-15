using System.ComponentModel.DataAnnotations;
using WebApplication1.Models;

public class Customer
{
    [Key] public int CustomerId { get; set; }

    [Required] [StringLength(100)] public string Name { get; set; }

    [Required] [StringLength(20)] public string Phone { get; set; }

    [Required] [StringLength(200)] public string Address { get; set; }

    // Navigation property
    public ICollection<Order> Orders { get; set; }
}