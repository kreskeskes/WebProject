using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.DTO
{
    public class ProductTypeResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();



        public override bool Equals(object? obj)
        {
            if (obj is ProductTypeResponse otherProductType)
            {
                return this.Id == otherProductType.Id &&
                    this.Name == otherProductType.Name &&
                    this.Products == otherProductType.Products;
            }
            return false;
        }
    }

    public static class ProductTypeExtensions
    {
        public static ProductTypeResponse ToProductTypeResponse(this ProductType ProductType)
        {
            return new ProductTypeResponse()
            {
                Id = ProductType.Id,
                Name = ProductType.Name,
                Products = ProductType.Products,
            };
        }
    }
}

