using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {

        public WebApplication1Context (DbContextOptions<WebApplication1Context> options)
            : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<InventoryStock> InventoryStocks { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure enums to be stored as strings in the database
            modelBuilder.Entity<Product>()
                .Property(p => p.Category)
                .HasConversion<string>();
                
            modelBuilder.Entity<Product>()
                .Property(p => p.Volume)
                .HasConversion<string>();

            // Configure indexes
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Name)
                .HasDatabaseName("IX_Product_Name");
                
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Company)
                .HasDatabaseName("IX_Product_Company");
                
            modelBuilder.Entity<Product>()
                .HasIndex(p => new { p.Category, p.Volume })
                .HasDatabaseName("IX_Product_Category_Volume");

            // Configure relationships
            modelBuilder.Entity<Product>()
                .HasMany(p => p.InventoryStocks)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Phone)
                .IsUnique()
                .HasDatabaseName("IX_Customer_Phone_Unique");

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderDateAd)
                .HasDatabaseName("IX_Order_Date");

            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(oi => oi.Order)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasMany(o => o.Payments)
                .WithOne(p => p.Order)
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<InventoryStock>()
                .HasIndex(i => i.PurchaseDate)
                .HasDatabaseName("IX_Inventory_PurchaseDate");

            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.PaymentDate)
                .HasDatabaseName("IX_Payment_Date");
        }
    }
}
