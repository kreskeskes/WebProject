using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;

namespace WebProjectUniversity.Core.DTO
{
	public class ProductSubcategoryResponse
	{
		public Guid Id { get; set; }

		public string Name { get; set; }

		public Guid CategoryId { get; set; }

		public ProductCategory Category { get; set; }
	}

	public static class ProductSubcategoryExtensions
	{
		public static ProductSubcategoryResponse ToProductSubcategoryResponse(this ProductSubcategory productSubcategory)
		{
			return new ProductSubcategoryResponse()
			{
				Id = productSubcategory.Id,
				CategoryId = productSubcategory.CategoryId,
				Name = productSubcategory.Name,
				Category = productSubcategory.Category,
			};
		}
	}
}

