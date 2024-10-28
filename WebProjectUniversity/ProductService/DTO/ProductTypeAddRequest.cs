using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.DTO
{
	public class ProductTypeAddRequest
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();



        public ProductType ToProductType()
		{
			return new ProductType()
			{
				Name= Name,
				Products= Products
			};
		}
	}

}
