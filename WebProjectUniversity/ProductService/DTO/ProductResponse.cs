using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductService.Enums;
using ProductService.Entities;


namespace ProductService.DTO
{
    public class ProductResponse
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public List<SizeOptions> Sizes { get; set; }

        public string Color { get; set; }

        public string Brand { get; set; }

        public List<ProductProductCategory> ProductCategories { get; set; }

        public Guid? ProductSubcategoryId { get; set; }

        public ProductSubcategory ProductSubcategory { get; set; }

        public AgeGenderGroup AgeGenderGroup { get; set; }



        public override bool Equals(object? obj)
        {
            if (obj is ProductResponse other)
            {
                return Id == other.Id &&
                       Name == other.Name &&
                       Description == other.Description &&
                       Price == other.Price &&
                       Sizes == other.Sizes &&
                       Color == other.Color &&
                       Brand == other.Brand &&
                       Equals(ProductCategories, other.ProductCategories) &&
                       ProductSubcategoryId == other.ProductSubcategoryId &&
                       Equals(ProductSubcategory, other.ProductSubcategory) &&
                       AgeGenderGroup == other.AgeGenderGroup;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public ProductUpdateRequest ToProductUpdateRequest()
        {
            return new ProductUpdateRequest()
            {
                Id = Id,
                Name = Name,
                Description = Description,
                Price = Price,
                Sizes = Sizes,
                Color = Color,
                Brand = Brand,
                ProductSubcategoryId = ProductSubcategoryId,
                ProductCategories = ProductCategories,
                AgeGenderGroup = AgeGenderGroup,

            };
        }
    }

    public static class ProductExtensions
    {
        /// <summary>
        /// Extension method that converts an instance of product class to an instance of ProductResponse class
        /// </summary>
        /// <param name="product">an instance of product class to be converted to ProductResponse</param>
        /// <returns>an instance of ProductResponse</returns>
        public static ProductResponse ToProductResponse(this Product product)
        {
            return new ProductResponse()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Sizes = product.Sizes,
                Color = product.Color,
                Brand = product.Brand,
                ProductCategories = product.ProductCategories,
                ProductSubcategoryId = product.ProductSubcategoryId,
                ProductSubcategory = product.ProductSubcategory,
                AgeGenderGroup = product.AgeGenderGroup,
            };
        }


    }
}
