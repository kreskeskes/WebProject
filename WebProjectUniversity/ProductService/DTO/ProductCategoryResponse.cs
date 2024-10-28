using ProductService.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.DTO
{
    public class ProductCategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public List<ProductProductCategory> Products { get; set; } = new List<ProductProductCategory>();




        public override bool Equals(object? obj)
        {
            if (obj is ProductCategoryResponse other)
            {
                return other.Id == this.Id &&
                    other.Name == this.Name
                    && this.Products.SequenceEqual(other.Products);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

    }

    public static class ProductCategoryExtension
    {
        public static ProductCategoryResponse ToProductCategoryResponse(this ProductCategory productCategory)
        {
            return new ProductCategoryResponse
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Products =productCategory.Products
            };
        }
    }
}

