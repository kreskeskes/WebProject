using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Enums;
using ProductService.Entities;


namespace ProductService.DTO
{
    public class ProductUpdateRequest
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
        public List<SizeOptions> Sizes { get; set; }

        [Required]
        [StringLength(30)]
        public List<string> Colors { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        public Dictionary<string, float> Materials { get; set; } = new Dictionary<string, float>();

        public List<string> Styles { get; set; } = new List<string>();

        public string? Length { get; set; }

        [Required]
        public Guid ProductTypeId { get; set; }

        public List<Guid> CategoryIds { get; set; } = new List<Guid>();

        [Required]
        public AgeGenderGroup AgeGenderGroup { get; set; }


        public Product ToProduct()
        {
            return new Product()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                Sizes = Sizes,
                Colors = Colors,
                Brand = Brand,
                ProductTypeId = ProductTypeId,
                Categories = CategoryIds.Select(categoryId => new ProductProductCategory
                {
                    ProductCategoryId = categoryId,
                }).ToList(),
                AgeGenderGroup = AgeGenderGroup,
                Id = Id,
                Length = Length,
                Materials = Materials,
                Styles = Styles,

            };
        }
    }


}
