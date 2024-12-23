﻿using ProductService.RepositoryContracts;
using ProductService.ServiceContracts.IProducts;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.Services.Products
{
	public class ProductsDeleterService : IProductsDeleterService
	{
		private readonly IProductsRepository _productsRepository;

		public ProductsDeleterService(IProductsRepository productsRepository)
		{
			_productsRepository = productsRepository;
		}


		public async Task<bool> DeleteProduct(Guid? productId)
		{
			if (productId == null)
			{
				throw new ArgumentNullException();
			}
			else
			{
				Product? foundProduct = await _productsRepository.GetProductByProductId(productId.Value);

				if (foundProduct == null)
				{
					return false;
				}

				return await _productsRepository.DeleteProductByProductId(foundProduct.Id);
			}
		}
	}
}
