using InventoryManagementSystemApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml;

namespace InventoryManagementSystemApi.Context
{
    public class InventoryDbContext:DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Category>()
                .HasIndex(e => e.CategoryName)
                .IsUnique();

            // Other configurations
            modelBuilder.Entity<Company>()
               .HasKey(e => e.Id);

            modelBuilder.Entity<Company>()
                .HasIndex(e => e.CompanyName)
                .IsUnique();
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<StockOut> StockOuts { get; set; }

    }
}
