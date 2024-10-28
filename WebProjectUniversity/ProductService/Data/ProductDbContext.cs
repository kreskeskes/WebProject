using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProductService.Entities;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<ProductProductCategory> ProductProductCategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            modelBuilder.Entity<ProductProductCategory>().HasKey(pc => new { pc.ProductCategoryId, pc.ProductId });

            modelBuilder.Entity<ProductProductCategory>()
                .HasOne(x => x.Product)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.ProductId);

            modelBuilder.Entity<ProductProductCategory>()
            .HasOne(x => x.ProductCategory)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.ProductCategoryId); 

            // Configure ProductCategory table
            modelBuilder.Entity<ProductCategory>()
                .ToTable("ProductCategories");

            // Configure ProductType table
            modelBuilder.Entity<ProductType>()
                .ToTable("ProductTypes");


            // Seed data configuration
            var productCategoriesJson = File.ReadAllText("productCategories.json");
            var productCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(productCategoriesJson);
            if (productCategories != null)
            {
                modelBuilder.Entity<ProductCategory>().HasData(productCategories.Select(c => new
                {
                    c.Id,
                    c.Name
                }).ToArray());
            }

            modelBuilder.Entity<Product>()
     .Property(p => p.Materials)
     .HasConversion(
         v => JsonConvert.SerializeObject(v), // Convert to JSON string for storage
         v => string.IsNullOrEmpty(v) ? new Dictionary<string, float>() : JsonConvert.DeserializeObject<Dictionary<string, float>>(v) // Deserialize JSON string back to dictionary
     );

            var productTypesJson = File.ReadAllText("productTypes.json");
            var productTypes = JsonConvert.DeserializeObject<List<ProductType>>(productTypesJson);
            if (productTypes != null)
            {
                modelBuilder.Entity<ProductType>().HasData(productTypes.Select(c => new
                {
                    c.Id,
                    c.Name
                }).ToArray());
            }

            var productsJson = File.ReadAllText("products.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);
            if (products != null)
            {
                modelBuilder.Entity<Product>().HasData(products);
            }
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebProjectUniversityDatabase;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
            }
        }
    }
}