using ProductService.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProductService.DTO
{
    public class ProductCategoryAddRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        // Many-to-many relationship with Product
        public List<ProductProductCategory> Products { get; set; } = new List<ProductProductCategory>();


        public ProductCategory ToProductCategory()
        {
            return new ProductCategory()
            {
                Name = Name,
                Products=Products,
            };
        }
    }


}
