using ProductService.DTO;
using ProductService.Entities;
using ProductService.RepositoryContracts;
using ProductService.ServiceContracts.IProductsCategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.Services.ProductsCategories
{
    public class CategoriesGetterService : ICategoriesGetterService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesGetterService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<List<ProductCategoryResponse>> GetAllProductCategories()
        {
            List<ProductCategory>? categoryResponses = await _categoriesRepository.GetAllProductCategories();

            return categoryResponses.Select(x => x.ToProductCategoryResponse()).ToList();
        }

        public async Task<ProductCategoryResponse> GetProductCategoryById(Guid id)
        {
            if (id  != Guid.Empty)
            {
                ProductCategory productCategory = await _categoriesRepository.GetProductCategoryById(id);

                if (productCategory != null)
                {
                  return productCategory.ToProductCategoryResponse();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ArgumentNullException();
            }


        }
    }
}
