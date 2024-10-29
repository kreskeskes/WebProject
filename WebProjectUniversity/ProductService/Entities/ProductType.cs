using ProductService.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class ProductType
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    // One-to-many relationship with Product
    public List<Product> Products { get; set; } = new List<Product>();

    //Many-to-many relationship with Category
    public List<ProductTypeProductCategory> ProductCategories { get; set; } = new List<ProductTypeProductCategory>();
}