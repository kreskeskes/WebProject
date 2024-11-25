using ProductService.Enums;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductService.Entities;
using ProductService.Helpers;
using ProductService.CustomValidations;

public class Product
{
    #region Fields

    private string _name;
    private string _description;
    private List<string> _colors = new List<string>();
    private string _brand;
    private List<string> _styles = new List<string>();
    private string _length;
    private Dictionary<string, float> _materials = new Dictionary<string, float>();
    #endregion

    #region Properties
    [Key]
    public Guid Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name
    {
        get
        {
            return _name;
        }
        set
        {
            _name = StringHelper.ToSentenceCase(value);
        }
    }

    [Required]
    [StringLength(500)]
    public string Description
    {
        get
        {
            return _description;
        }
        set
        {
            _description = StringHelper.ToSentenceCase(value);
        }
    }


    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    [Range(0, double.MaxValue, ErrorMessage = "The price must be a decimal.")]
    public decimal Price { get; set; }

    [Required]
    public List<SizeOptions> Sizes { get; set; } = new List<SizeOptions>();

    [Required]
    [StringLength(50)]
    public string Brand
    {
        get
        {
            return _brand;
        }
        set
        {
            _brand = StringHelper.ToSentenceCase(value);
        }
    }

    // Single reference to ProductType
    [Required]
    public Guid ProductTypeId { get; set; }

    [ForeignKey("ProductTypeId")]
    public ProductType ProductType { get; set; }

    // Many-to-many relationship with ProductCategory
    [Required]
    public List<ProductProductCategory> Categories { get; set; } = new List<ProductProductCategory>();

    [Required]
    public AgeGenderGroup AgeGenderGroup { get; set; }

    [Required]
    public List<string> Colors
    {
        get
        {
            return _colors;
        }
        set
        {
            _colors = value.Select(x=>StringHelper.ToSentenceCase(x)).ToList();
        }
    }

    [Required]
    [MaterialsValidation]
    public Dictionary<string, float> Materials
    {
        get => _materials;
        set
        {
            _materials = value.ToDictionary(
                kvp => StringHelper.ToSentenceCase(kvp.Key),
                kvp => kvp.Value == 0 ? 100 : kvp.Value
            );
        }
    }

    public List<string> Styles
    {
        get
        {
            return _styles;
        }
        set
        {
            _styles = value.Select(x => StringHelper.ToSentenceCase(x)).ToList();
        }
    }

    public string? Length
    {
        get
        {
            return _length;
        }
        set
        {
            _length = StringHelper.ToSentenceCase(_length);
        }
    }
    #endregion


}