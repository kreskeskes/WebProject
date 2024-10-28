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
    public class ProductTypesGetterService : IProductTypesGetterService
    {
        private readonly IProductTypesRepository _ProductTypesRepository;
        private readonly ICategoriesRepository _categoriesRepository;

        public ProductTypesGetterService(ICategoriesRepository categoriesRepository,
            IProductTypesRepository ProductTypesRepository)
        {
            _ProductTypesRepository = ProductTypesRepository;
            _categoriesRepository = categoriesRepository;
        }
        public async Task<List<ProductTypeResponse>> GetAllProductTypes()
        {
            List<ProductType> productTypes = await _ProductTypesRepository.GetAllProductTypes();
            return productTypes.Select(x => x.ToProductTypeResponse()).ToList();
        }

        public async Task<ProductTypeResponse> GetProductTypeResponseById(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentNullException();
            }
            ProductType foundProductType = await _ProductTypesRepository.GetProductTypeById(id);

            if (foundProductType == null)
            {
                return null;
            }
            else
            {
                return foundProductType.ToProductTypeResponse();
            }
        }
    }
}
