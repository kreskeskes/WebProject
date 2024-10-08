using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace ProductService.Entities
{
	public class ProductCategory
	{
		[Key]
		public Guid? Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Name { get; set; }

		public ICollection<ProductSubcategory>? ProductSubcategories { get; set; } = new List<ProductSubcategory>();

        public List<ProductProductCategory> Products { get; set; } = new List<ProductProductCategory>();


        public override bool Equals(object? obj)
        {
			if (obj is ProductCategory other)
			{
				return other.Id== this.Id&&other.Name==this.Name&&Equals(this.ProductSubcategories, other.ProductSubcategories);
			}
			return false;

        }
    }
}
