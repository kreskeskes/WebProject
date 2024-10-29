using ProductService.DTO;
using ProductService.Repositories;
using ProductService.RepositoryContracts;
using ProductService.ServiceContracts.IProductsProductTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.Services.ProductsCategories.ProductTypes
{
    public class ProductTypesUpdaterService : IProductTypesUpdaterService
    {
        private readonly IProductTypesRepository _productTypesRepository;
        private readonly IProductsRepository _productsRepository;


        public ProductTypesUpdaterService(IProductTypesRepository productTypesRepository, IProductsRepository productsRepository)
        {
            _productTypesRepository = productTypesRepository;
            _productsRepository = productsRepository;
        }
        public async Task<ProductTypeResponse> UpdateProductType(ProductTypeUpdateRequest productUpdateRequest)
        {
            if (productUpdateRequest == null)
            {
                throw new ArgumentNullException();
            }
            if (productUpdateRequest.Id != Guid.Empty)
            {
                ProductType matchingProductType = await _productTypesRepository.
               GetProductTypeById(productUpdateRequest.Id);
                if (matchingProductType != null)
                {
                    List<Product> foundProducts = new List<Product>();
                    foreach (var productId in productUpdateRequest.ProductIds)
                    {
                        var product = await _productsRepository.GetProductByProductId(productId);
                        foundProducts.Add(product);
                    }
                    matchingProductType.Products = foundProducts;

                    matchingProductType.Name = productUpdateRequest.Name;
                    ProductType ProductTypeAfterUpdate = await _productTypesRepository.UpdateProductType(matchingProductType);
                    return ProductTypeAfterUpdate.ToProductTypeResponse();
                }
                return null;
            }
            return null;


        }
    }
}
