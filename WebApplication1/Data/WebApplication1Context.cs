using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class WebApplication1Context : DbContext
    {

        public WebApplication1Context(DbContextOptions<WebApplication1Context> options)
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
            base.OnModelCreating(modelBuilder); // Important for Identity

            // Configure enum conversions
            modelBuilder.Entity<Product>()
                .Property(p => p.Category)
                .HasConversion<int>();

            modelBuilder.Entity<Product>()
                .Property(p => p.Volume)
                .HasConversion<int>();

            // Configure computed column for RetailPrice
            modelBuilder.Entity<Product>()
                .Property(p => p.RetailPrice)
                .HasComputedColumnSql(
                    "ROUND(WholesalePrice + (WholesalePrice * 0.07) + ((WholesalePrice + (WholesalePrice * 0.07)) * 0.13), 2)");

            // Configure the view
            modelBuilder.Entity<ProductDisplayView>(entity =>
            {
                entity.ToView("ProductDisplay");
                entity.HasNoKey();
                entity.Property(v => v.ProductId).HasColumnName("ProductId");
                // Map other view properties as needed
            });

            // Configure relationships
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

            modelBuilder.Entity<Product>()
                .HasMany(p => p.InventoryStocks)
                .WithOne(i => i.Product)
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Product>()
                .HasMany(p => p.OrderItems)
                .WithOne(oi => oi.Product)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)
                .WithOne(o => o.Customer)
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);
            // Configure BS date conversion
            modelBuilder.Entity<InventoryStock>()
                .Property(i => i.PurchaseDateBs)
                .HasColumnType("TEXT");

            // Configure indexes
            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ShadeSlug);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Company);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.Category);

            modelBuilder.Entity<InventoryStock>()
                .HasIndex(i => i.ProductId);

            modelBuilder.Entity<InventoryStock>()
                .HasIndex(i => i.PurchaseDateAd);
            modelBuilder.Entity<InventoryStock>()
                .HasIndex(i => i.PurchaseDateBs);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Phone);

            modelBuilder.Entity<Customer>()
                .HasIndex(c => c.Name);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.CustomerId);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.DateAd);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.DateBs);

            modelBuilder.Entity<OrderItem>()
                .HasIndex(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasIndex(oi => oi.ProductId);

            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.OrderId);

            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.PaymentDateAd);
            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.PaymentDateBs);

            // Seed initial data if needed
            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            // Example seed data - adjust as needed
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Premium Paint",
                    Category = ProductCategory.Paint,
                    Volume = ProductVolume.Liter4,
                    Company = "Asian Paints",
                    ShadeSlug = "royal-blue",
                    WholesalePrice = 800.00m
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Wall Putty",
                    Category = ProductCategory.Putty,
                    Volume = ProductVolume.Kg5,
                    Company = "Berger",
                    ShadeSlug = "white",
                    WholesalePrice = 500.00m
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Smart Care Polish",
                    Category = ProductCategory.SmartCare,
                    Volume = ProductVolume.Kg1,
                    Company = "Nerolac",
                    ShadeSlug = "clear",
                    WholesalePrice = 300.00m
                }
            );

            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    Name = "John Doe",
                    Phone = "9841000001",
                    Address = "Kathmandu"
                },
                new Customer { CustomerId = 2, Name = "Anita Sharma", Phone = "9841000002", Address = "Lalitpur" }
            );

            modelBuilder.Entity<InventoryStock>().HasData(
                new InventoryStock
                {
                    BatchId = 1,
                    ProductId = 1,
                    Quantity = 100,
                    PurchaseDateAd = new DateTime(2025, 4, 1),
                    PurchaseDateBs = "2081-01-18"
                },
                new InventoryStock { BatchId = 2, ProductId = 2, Quantity = 50, PurchaseDateAd = new DateTime(2025, 4, 5), PurchaseDateBs = "2081-01-22", CostPrice = 480.00m }
            );
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    CustomerId = 1,
                    DateAd = new DateTime(2025, 4, 10),
                    DateBs = "2081-01-27",
                    TotalAmount= 1000.00m
                },
                new Order { OrderId = 2, CustomerId = 2, DateAd = new DateTime(2025, 4, 12), DateBs = "2081-01-29", TotalAmount = 500.00m, PendingAmount = 0.00m }
            );
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { ItemId = 1, OrderId = 1, ProductId = 1, Quantity = 1, UnitPrice = 800.00m, ExciseDuty = 56.00m, VAT = 117.04m },
                new OrderItem { ItemId = 2, OrderId = 1, ProductId = 2, Quantity = 1, UnitPrice = 500.00m, ExciseDuty = 35.00m, VAT = 73.15m },
                new OrderItem { ItemId = 3, OrderId = 2, ProductId = 2, Quantity = 1, UnitPrice = 500.00m, ExciseDuty = 35.00m, VAT = 73.15m }
            );
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    PaymentId = 1,
                    OrderId = 1,
                    PaymentDateAd = new DateTime(2025, 4, 11),
                    PaymentDateBs = "2081-01-28",
                    IsAdvance = false,
                    Amount = 1000.00m
                },new Payment { PaymentId = 2, OrderId = 2, Amount = 500.00m, PaymentDateAd = new DateTime(2025, 4, 13), PaymentDateBs = "2081-02-01", IsAdvance = true }
            );
        }
    }
}
