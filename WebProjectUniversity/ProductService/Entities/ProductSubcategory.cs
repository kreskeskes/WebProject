using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductService.Entities
{
    public class ProductSubcategory
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public Guid CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public ProductCategory Category { get; set; }


        public override bool Equals(object? obj)
        {
            if (obj is ProductSubcategory other)
            {
                return this.Id == other.Id && this.Name == other.Name && Equals(this.Category, other.Category) && this.CategoryId == other.CategoryId;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
