﻿using ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProducts
{
	public interface IProductsAdderService
	{
		/// <summary>
		/// Adds a products to product list
		/// </summary>
		/// <param name="productAddRequest">a product to be added to product list</param>
		/// <returns>The just added product</returns>
		Task<ProductResponse> AddProduct(ProductAddRequest productAddRequest);
	}
}
