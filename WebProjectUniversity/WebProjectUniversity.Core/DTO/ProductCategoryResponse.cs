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
    public class ProductCategoryResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<ProductSubcategory> ProductSubcategories { get; set; } = new List<ProductSubcategory>();



        public override bool Equals(object? obj)
        {
            if (obj is ProductCategoryResponse other)
            {
                return other.Id == this.Id &&
                    other.Name == this.Name
                    && this.ProductSubcategories.SequenceEqual(other.ProductSubcategories);
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
                Id = productCategory.Id ?? Guid.Empty,
                Name = productCategory.Name,
                ProductSubcategories = productCategory.ProductSubcategories?.Select(ps => new ProductSubcategory
                {
                    Id = ps.Id,
                    Name = ps.Name,
                    CategoryId = ps.CategoryId
                }).ToList()
            };
        }
    }
}

