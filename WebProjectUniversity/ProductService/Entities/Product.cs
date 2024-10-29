using ProductService.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductService.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    [StringLength(500)]
    public string Description { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }

    [Required]
    public List<SizeOptions> Sizes { get; set; } = new List<SizeOptions>();

    [Required]
    [StringLength(50)]
    public string Brand { get; set; }

    // Single reference to ProductType
    [Required]
    public Guid ProductTypeId { get; set; }

    [ForeignKey("ProductTypeId")]
    public ProductType ProductType { get; set; }

    // Many-to-many relationship with ProductCategory
    public List<ProductProductCategory> Categories { get; set; } = new List<ProductProductCategory>();

    [Required]
    public AgeGenderGroup AgeGenderGroup { get; set; }

    [Required]
    public List<string> Colors { get; set; } = new List<string>();

    [Required]
    public Dictionary<string, float> Materials { get; set; } = new Dictionary<string, float>();

    public List<string> Styles { get; set; } = new List<string>();

    public string? Length { get; set; }
}