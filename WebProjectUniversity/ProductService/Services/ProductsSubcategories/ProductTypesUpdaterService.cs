using ProductService.DTO;
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

        public ProductTypesUpdaterService(IProductTypesRepository productTypesRepository)
        {
            _productTypesRepository = productTypesRepository;
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
                    matchingProductType.Products = productUpdateRequest.Products;
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
