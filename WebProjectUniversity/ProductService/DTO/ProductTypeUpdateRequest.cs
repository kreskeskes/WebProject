using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProductService.DTO
{
	public class ProductTypeUpdateRequest
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();


        public ProductType ToProductType()
		{
			return new ProductType()
			{
				Id = Id,
				Name = Name,
				Products =	Products
			};
		}
	}
}
