using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tailor_shop.Models;
using TailorShop.Models;

namespace Tailor_shop.Data
{
    public class WebDbContext : IdentityDbContext
    {
        public WebDbContext(DbContextOptions<WebDbContext> options) : base(options)
        {
        }

        // DbSets for each model (tables in the database)
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Measurement> Measurements { get; set; }

        // Configure relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Customer ↔ Order (One-to-Many)
            modelBuilder.Entity<Customer>()
                .HasMany(c => c.Orders)  // One customer has many orders
                .WithOne(o => o.Customer)  // Each order belongs to one customer
                .HasForeignKey(o => o.CustomerId)  // Foreign key: CustomerId in Order
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete

            // 2. Order ↔ Payment (One-to-Many)
            modelBuilder.Entity<Order>()
                .HasMany(o => o.Payments)  // One order has many payments
                .WithOne(p => p.Order)  // Each payment belongs to one order
                .HasForeignKey(p => p.OrderId)  // Foreign key: OrderId in Payment
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete

            // 3. Customer ↔ Measurement (One-to-Many)
            modelBuilder.Entity<Measurement>()
                .HasOne(m => m.Customer)  // Each measurement belongs to one customer
                .WithMany(c => c.Measurements)  // One customer can have many measurements
                .HasForeignKey(m => m.CustomerId)  // Foreign key: CustomerId in Measurement
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete

            // 4. Product ↔ Measurement (Many-to-One)
            modelBuilder.Entity<Measurement>()
                .HasOne(m => m.Product)  // Each measurement belongs to one product
                .WithMany(p => p.Measurements)  // One product can have many measurements
                .HasForeignKey(m => m.ProductId)  // Foreign key: ProductId in Measurement
                .OnDelete(DeleteBehavior.Restrict);  // Prevent deletion of product if in use by measurements

            // 5. Order ↔ Customer (Many-to-One)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)  // Each order belongs to one customer
                .WithMany(c => c.Orders)  // One customer can have many orders
                .HasForeignKey(o => o.CustomerId)
                .OnDelete(DeleteBehavior.Cascade);  // Cascade delete

            // Optional: Add indexes for performance on frequently used foreign keys
            modelBuilder.Entity<Measurement>()
                .HasIndex(m => m.CustomerId)
                .HasDatabaseName("IX_Measurement_CustomerId");

            modelBuilder.Entity<Measurement>()
                .HasIndex(m => m.ProductId)
                .HasDatabaseName("IX_Measurement_ProductId");

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.CustomerId)
                .HasDatabaseName("IX_Order_CustomerId");

            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.OrderId)
                .HasDatabaseName("IX_Payment_OrderId");

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.ProductName)
                .HasDatabaseName("IX_Product_ProductName");
        }
    }
}
