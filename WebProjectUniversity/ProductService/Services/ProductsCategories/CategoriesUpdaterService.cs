using ProductService.DTO;
using ProductService.Repositories;
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
        private readonly IProductsRepository _productsRepository;

        public CategoriesUpdaterService(ICategoriesRepository categoriesRepository, IProductsRepository productsRepository)
        {
            _categoriesRepository = categoriesRepository;
            _productsRepository = productsRepository;
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
                        List<Product> foundProducts = new List<Product>();
                        foreach (var productId in productCategoryUpdateRequest.ProductIds)
                        {
                            var product = await _productsRepository.GetProductByProductId(productId);
                            foundProducts.Add(product);
                        }

                        foreach(var product in foundCategory.Products)
                        {
                            foundCategory.Products.Add(product);
                        }
                        ;
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
