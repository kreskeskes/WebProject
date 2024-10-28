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

        public List<string> Colors { get; set; }

        public string Brand { get; set; }

        public Dictionary<string, float> Materials { get; set; } = new Dictionary<string, float>();

        public List<string> Styles { get; set; } = new List<string>();

        public string? Length { get; set; }

        // Single reference to ProductType
        public Guid ProductTypeId { get; set; }

        public ProductType ProductType { get; set; } // Include ProductType information

        // List of Category IDs
        public List<Guid> CategoryIds { get; set; } = new List<Guid>();

        // Optionally include full ProductCategory details if needed
        public List<ProductProductCategory> Categories { get; set; } = new List<ProductProductCategory>();

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
                       Colors == other.Colors &&
                       Brand == other.Brand &&
                       Categories.SequenceEqual(other.Categories) &&
                       ProductType == other.ProductType &&
                       ProductTypeId == other.ProductTypeId &&
                       CategoryIds == other.CategoryIds &&
                       AgeGenderGroup == other.AgeGenderGroup &&
                       Materials == other.Materials &&
                       Styles == other.Styles &&
                       Length == other.Length;
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
                Colors = Colors,
                Brand = Brand,
                AgeGenderGroup = AgeGenderGroup,
                ProductTypeId = ProductTypeId,
                CategoryIds = CategoryIds,
                Materials = Materials,
                Length = Length,
                Styles = Styles,

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
                Colors = product.Colors,
                Brand = product.Brand,
                ProductTypeId = product.ProductTypeId,
                CategoryIds = product.CategoryIds,
                Length = product.Length,
                Categories = product.Categories,
                Materials = product.Materials,
                Styles = product.Styles,
                ProductType = product.ProductType,
                AgeGenderGroup = product.AgeGenderGroup,
            };
        }


    }
}
