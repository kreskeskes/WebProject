using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;



namespace WebProjectUniversity.Core.DTO
{
    public class ProductCategoryAddRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public ICollection<ProductSubcategory> ProductSubcategories { get; set; } = new List<ProductSubcategory>();


        public ProductCategory ToProductCategory()
        {
            return new ProductCategory()
            {
                Name = Name,
                ProductSubcategories = ProductSubcategories,
            };
        }
    }


}
