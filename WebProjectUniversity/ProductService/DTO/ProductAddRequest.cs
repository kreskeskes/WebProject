using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using ProductService.Enums;
using ProductService.Entities;
using Azure.Core;
using ProductService.CustomValidations;




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
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public decimal Price { get; set; }

		[Required]
		public List<SizeOptions> Sizes { get; set; }

		[Required]
		public List<string> Colors { get; set; } = new List<string>();

        [Required]
		[MaterialsValidation]
        public Dictionary<string, float> Materials { get; set; } = new Dictionary<string, float>();

        public List<string> Styles { get; set; } = new List<string>();

        public string? Length { get; set; }

        [Required]
		[StringLength(50)]
		public string Brand { get; set; }

        // Single reference to ProductType
        [Required]
        public Guid ProductTypeId { get; set; }

		[Required]
        public List<Guid> CategoryIds { get; set; } = new List<Guid>();

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
				Colors = this.Colors,
				Brand = this.Brand,
				ProductTypeId = this.ProductTypeId,
				Categories = this.CategoryIds.Select(categoryId => new ProductProductCategory
				{
					ProductCategoryId = categoryId
				}).ToList(),
				AgeGenderGroup = this.AgeGenderGroup,
				Length = this.Length,
				Materials = this.Materials,
				Styles = this.Styles,

			};


		}
	}
}