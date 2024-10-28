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
    public class ProductTypesAdderService : IProductTypesAdderService
    {
        private readonly IProductTypesRepository _ProductTypesRepository;
        private readonly ICategoriesRepository _categoriesRepository;


        public ProductTypesAdderService(IProductTypesRepository ProductTypesRepository, ICategoriesRepository categoriesRepository)
        {
            _ProductTypesRepository = ProductTypesRepository;
            _categoriesRepository = categoriesRepository;
        }
        public async Task<ProductTypeResponse> AddProductType(ProductTypeAddRequest productTypeAddRequest)
        {
            if (productTypeAddRequest == null)
            {
                throw new ArgumentNullException();

            }

            if (productTypeAddRequest.Products == null)
            {
                throw new ArgumentException();
            }

            
            ProductType productType_toBeAdded = productTypeAddRequest.ToProductType();

            ProductType productType_added = await _ProductTypesRepository.AddProductType(productType_toBeAdded);
            productType_added.Id = Guid.NewGuid();

            if (productType_added.Id != Guid.Empty)
            {
                return productType_added.ToProductTypeResponse();
            }
            else
            {
                throw new ArgumentException();
            }


        }
    }
}
