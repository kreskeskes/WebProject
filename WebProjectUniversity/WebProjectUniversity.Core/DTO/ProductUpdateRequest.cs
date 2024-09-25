using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Enums;
using WebProjectUniversity.Core.Domain.Entities;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;

namespace WebProjectUniversity.Core.DTO
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
		public string Color { get; set; }

		[Required]
		[StringLength(50)]
		public string Brand { get; set; }

		[Required]
		public List<ProductProductCategory> ProductCategories { get; set; }

		[Required]
		public Guid? ProductSubcategoryId { get; set; }


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
				Color = Color,
				Brand = Brand,
				ProductCategories = ProductCategories,
				ProductSubcategoryId = ProductSubcategoryId,
				AgeGenderGroup = AgeGenderGroup,
				Id = Id,
			
			};
		}
	}


}
