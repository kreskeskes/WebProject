using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductService.Enums;
using ProductService.Entities;




namespace ProductService.DTO
{
	public class ProductAddRequest
	{
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

		[Required]
		public List<ProductProductCategory> ProductCategories { get; set; }

		[Required]
		public Guid ProductSubcategoryId { get; set; }

		[Required]
		public AgeGenderGroup AgeGenderGroup { get; set; }

		public Product ToProduct()
		{
			return new Product()
			{
				Name = this.Name,
				Description = this.Description,
				Price = this.Price,
				Sizes = this.Sizes,
				Color = this.Color,
				Brand = this.Brand,
				ProductCategories = this.ProductCategories,
				ProductSubcategoryId = this.ProductSubcategoryId,
				AgeGenderGroup = this.AgeGenderGroup
			};
		}
	}
}