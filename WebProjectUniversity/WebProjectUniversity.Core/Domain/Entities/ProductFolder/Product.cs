using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebProjectUniversity.Core.Domain.Enums;

namespace WebProjectUniversity.Core.Domain.Entities.ProductFolder
{
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
		public List<SizeOptions> Sizes { get; set; }

		[Required]
		[StringLength(30)]
		public string Color { get; set; }

		[Required]
		[StringLength(50)]
		public string Brand { get; set; }

        public List<ProductProductCategory> ProductCategories { get; set; } = new List<ProductProductCategory>();

        public List<Guid> ProductCategoryIds { get; set; } = new List<Guid>(); 

        // Foreign Key for ProductSubcategory
        [Required]
		public Guid? ProductSubcategoryId { get; set; }

		[ForeignKey("ProductSubcategoryId")]
		public ProductSubcategory ProductSubcategory { get; set; }

		[Required]
		public AgeGenderGroup AgeGenderGroup { get; set; }


	}
}
