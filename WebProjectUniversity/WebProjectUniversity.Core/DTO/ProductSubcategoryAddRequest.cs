using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;

namespace WebProjectUniversity.Core.DTO
{
	public class ProductSubcategoryAddRequest
	{
		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		[Required]
		public Guid CategoryId { get; set; }
		

		public ProductSubcategory ToProductSubcategory()
		{
			return new ProductSubcategory()
			{
				CategoryId= CategoryId,
				Name= Name
			};
		}
	}

}
