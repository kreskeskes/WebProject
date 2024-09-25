﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;
using WebProjectUniversity.Core.Domain.RepositoryContracts;
using WebProjectUniversity.Core.DTO;
using WebProjectUniversity.Core.ServiceContracts.IProducts;

namespace WebProjectUniversity.Core.Service
{
	public class ProductsUpdaterService : IProductsUpdaterService
	{
		private readonly IProductsRepository _productsRepository;

		public ProductsUpdaterService(IProductsRepository productsRepository)
		{
			_productsRepository = productsRepository;
		}

		public async Task<ProductResponse> UpdateProduct(ProductUpdateRequest productUpdateRequest)
		{
			if (productUpdateRequest != null)
			{
				if (productUpdateRequest.Id==Guid.Empty)
				{
					throw new ArgumentException();
				}
				else
				{
					Product? foundProduct = await _productsRepository.GetProductByProductId(productUpdateRequest.Id);

					if (foundProduct != null)
					{
						foundProduct.ProductSubcategoryId = productUpdateRequest.ProductSubcategoryId;
						foundProduct.Brand = productUpdateRequest.Brand;
						foundProduct.AgeGenderGroup = productUpdateRequest.AgeGenderGroup;
						foundProduct.Color = productUpdateRequest.Color;
						foundProduct.ProductCategories = productUpdateRequest.ProductCategories;
						foundProduct.Sizes = productUpdateRequest.Sizes;
						foundProduct.Description = productUpdateRequest.Description;
						foundProduct.Price = productUpdateRequest.Price;
						foundProduct.Name = productUpdateRequest.Name;

						Product productAfterUpdation = await _productsRepository.UpdateProduct(foundProduct);
						return productAfterUpdation.ToProductResponse();
					}
					else
					{
						return null;
					}
				}
			}
			else
			{
				throw new ArgumentNullException();
			}
		}
	}
}
