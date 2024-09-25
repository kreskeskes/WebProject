﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities;
using WebProjectUniversity.Core.Domain.RepositoryContracts;
using WebProjectUniversity.Core.DTO;
using WebProjectUniversity.Core.ServiceContracts.IProductsCategories;
using WebProjectUniversity.Core.ServiceContracts.IProductsSubcategories;

namespace WebProjectUniversity.Core.Service.ProductsCategories
{
    public class CategoriesAdderService : ICategoriesAdderService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesAdderService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public async Task<ProductCategoryResponse> AddProductCategory(ProductCategoryAddRequest productCategoryAddRequest)
        {
            if (productCategoryAddRequest != null)
            {
                if (productCategoryAddRequest.Name!=null)
                {
                    ProductCategory productCategory = productCategoryAddRequest.ToProductCategory();

                    ProductCategory categoryAdded = await _categoriesRepository.AddProductCategory(productCategory);
                    categoryAdded.Id = Guid.NewGuid();

                    if (categoryAdded.Id != Guid.Empty)
                    {
                        return categoryAdded.ToProductCategoryResponse();

                    }
                    else
                    {
                        throw new ArgumentException();
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