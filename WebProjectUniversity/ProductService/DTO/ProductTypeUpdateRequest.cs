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

        public List<Guid> ProductIds { get; set; } = new List<Guid>();


        public ProductType ToProductType()
		{
			return new ProductType()
			{
				Id = Id,
				Name = Name,
				Products =	ProductIds.Select(productId=> new Product()
				{
					Id=productId,
				}).ToList(),
			};
		}
	}
}
