using ProductService.Entities;
using System.ComponentModel.DataAnnotations;

public class ProductCategory
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    // Many-to-many relationship with Product
    public List<ProductProductCategory> Products { get; set; } = new List<ProductProductCategory>();
}