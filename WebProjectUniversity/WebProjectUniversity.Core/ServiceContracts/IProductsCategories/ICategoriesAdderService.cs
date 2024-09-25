﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.DTO;

namespace WebProjectUniversity.Core.ServiceContracts.IProductsCategories
{
	public interface ICategoriesAdderService
	{
		/// <summary>
		/// Adds a new category to Category list
		/// </summary>
		/// <param name="productCategoryAddRequest">Category to be added</param>
		/// <returns>The just added category</returns>
		Task<ProductCategoryResponse> AddProductCategory(ProductCategoryAddRequest productCategoryAddRequest);
	}
}
