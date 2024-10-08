using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using ProductService.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;


namespace ProductService.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            // Configure many-to-many relationship
            modelBuilder.Entity<ProductProductCategory>()
                .HasKey(pp => new { pp.ProductId, pp.CategoryId });


            modelBuilder.Entity<ProductProductCategory>()
           .HasOne(pp => pp.Product)
           .WithMany(p => p.ProductCategories)
           .HasForeignKey(pp => pp.ProductId);

            modelBuilder.Entity<ProductProductCategory>()
           .HasOne(pp => pp.ProductCategory)
           .WithMany(c => c.Products)
           .HasForeignKey(pp => pp.CategoryId);

            // Configure ProductCategory entity
            modelBuilder.Entity<ProductCategory>()
                .ToTable("ProductCategories");

            // Configure ProductSubcategory entity
            modelBuilder.Entity<ProductSubcategory>()
                .ToTable("ProductSubcategories")
                .HasOne(p => p.Category)
                .WithMany(c => c.ProductSubcategories)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior

            // Configure Product entity
            modelBuilder.Entity<Product>()
                .ToTable("Products");

            // Configure relationship for ProductSubcategory (if needed)
            modelBuilder.Entity<Product>()
                .HasOne(p => p.ProductSubcategory)
                .WithMany() // No navigation property on ProductSubcategory side
                .HasForeignKey(p => p.ProductSubcategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior

            // Seed data configuration
            var productCategoriesJson = File.ReadAllText("productCategories.json");
            var productCategories = JsonConvert.DeserializeObject<List<ProductCategory>>(productCategoriesJson);
            if (productCategories != null)
            {
                modelBuilder.Entity<ProductCategory>().HasData(productCategories.Select(c => new
                {
                    c.Id,
                    c.Name,
                }).ToArray());
            }

            var productSubcategoriesJson = File.ReadAllText("productSubcategories.json");

            var productSubcategories = JsonConvert.DeserializeObject<List<ProductSubcategory>>(productSubcategoriesJson);
            if (productSubcategories != null)
            {
                modelBuilder.Entity<ProductSubcategory>().HasData(productSubcategories.Select(c => new
                {
                    c.Id,
                    c.Name,
                    c.CategoryId,
                }).ToArray());
            }

            var productsJson = File.ReadAllText("products.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(productsJson);

            if (products != null)
            {
                modelBuilder.Entity<ProductCategory>().HasData(products.Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.Color,
                    p.Brand,
                    p.ProductSubcategoryId,
                    p.AgeGenderGroup,
                    p.Sizes,
                    p
                }).ToArray());
                foreach (var product in products)
                {
                    modelBuilder.Entity<Product>().HasData(product);
                    foreach (var categoryId in product.ProductCategoryIds)
                    {
                        modelBuilder.Entity<ProductProductCategory>().HasData(new ProductProductCategory
                        {
                            ProductId = product.Id,
                            CategoryId = categoryId,
                        });
                    }
                }

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

