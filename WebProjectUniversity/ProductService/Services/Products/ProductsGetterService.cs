using ProductService.DTO;
using ProductService.RepositoryContracts;
using ProductService.ServiceContracts.IProducts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace ProductService.Services.Products
{
    public class ProductsGetterService : IProductsGetterService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsGetterService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<List<ProductResponse>?> GetAllProducts()
        {
            List<Product>? products = await _productsRepository.GetAllProducts();
            return products?.Select(x => x.ToProductResponse()).ToList();
        }

        public async Task<List<ProductResponse>> GetFilteredProducts(string? searchBy, string? searchString)
        {
            List<Product>? products;

            products = searchBy switch
            {
                nameof(ProductResponse.Name) =>
                await _productsRepository.GetFilteredProducts
                (temp => temp.Name.Contains(searchString, StringComparison.OrdinalIgnoreCase)),

                nameof(ProductResponse.Brand) =>
                await _productsRepository.GetFilteredProducts
                (temp => temp.Brand.Contains(searchString, StringComparison.OrdinalIgnoreCase)),

                nameof(ProductResponse.Colors) =>
                await _productsRepository.GetFilteredProducts
                (temp => temp.Colors.Contains(searchString)),

                nameof(ProductResponse.ProductType) =>
                await _productsRepository.GetFilteredProducts
                (temp => temp.ProductType.Name.Contains(searchString)),

                nameof(ProductResponse.Categories) =>
                await _productsRepository.GetFilteredProducts
                (temp => temp.Categories.Any(x => x.ProductCategory.Name.Contains(searchString))),

                nameof(ProductResponse.Materials.Keys) =>
                await _productsRepository.GetFilteredProducts
                (temp => temp.Materials.Keys.Contains(searchString)),


                _ => await _productsRepository.GetAllProducts()

            };
            return products.Select(x => x.ToProductResponse()).ToList();
        }

        public async Task<ProductResponse>? GetProductByProductId(Guid productId)
        {
            if (productId != null)
            {
                Product? product = await _productsRepository.GetProductByProductId(productId);
                if (product != null)
                {
                    return product.ToProductResponse();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
