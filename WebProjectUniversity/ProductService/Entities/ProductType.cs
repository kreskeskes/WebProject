using System.ComponentModel.DataAnnotations;

public class ProductType
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(50)]
    public string Name { get; set; }

    // One-to-many relationship with Product
    public List<Product> Products { get; set; } = new List<Product>();
}