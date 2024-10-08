using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Entities;

namespace ProductService.DTO
{
	public class ProductSubcatergoryUpdateRequest
	{
		[Key]
		public Guid SubcategoryId { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		public Guid CategoryId { get; set; }

		public ProductSubcategory ToProductSubcategory()
		{
			return new ProductSubcategory()
			{
				Id = SubcategoryId,
				CategoryId = CategoryId,
				Name = Name,

			};
		}
	}
}
