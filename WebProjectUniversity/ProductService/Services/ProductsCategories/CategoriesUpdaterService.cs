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
    public class CategoriesUpdaterService : ICategoriesUpdaterService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesUpdaterService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<ProductCategoryResponse> UpdateProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest)
        {
            if (productCategoryUpdateRequest != null)
            {
                if (productCategoryUpdateRequest.Id != Guid.Empty)
                {
                    ProductCategory foundCategory = await _categoriesRepository.GetProductCategoryById(productCategoryUpdateRequest.Id);

                    if (foundCategory != null)
                    {
                        foundCategory.ProductSubcategories = productCategoryUpdateRequest.ProductSubcategories;
                        foundCategory.Name = productCategoryUpdateRequest.Name;
                        ProductCategory productCategoryAfterUpdation = await _categoriesRepository.UpdateProductCategory(foundCategory);

                        return productCategoryAfterUpdation.ToProductCategoryResponse();

                    }
                    else
                    {
                        return null;
                    }

                }
                else
                {
                    throw new ArgumentException();
                }

            }
            else
            {
                throw new ArgumentNullException();
            }
        }
    }
}
