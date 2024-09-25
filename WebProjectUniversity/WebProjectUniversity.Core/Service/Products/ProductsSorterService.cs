using ServiceContracts.Enums;
using System;
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
	public class ProductsSorterService : IProductsSorterService
	{

		private readonly IProductsRepository _productsRepository;

		public ProductsSorterService(IProductsRepository productsRepository)
		{
			_productsRepository = productsRepository;
		}

		public async Task<List<ProductResponse>>? GetSortedProducts(List<ProductResponse> allProducts, string sortBy, SortOrderOptions sortOrder)
		{
			if (sortBy == null)
			{
				return allProducts;
			}
			else
			{
				List<ProductResponse>? sortedList = (sortBy, sortOrder) switch
				{
					((nameof(Product.Price)), SortOrderOptions.Ascending) =>
					allProducts.OrderBy(temp => temp.Price).ToList(),

					((nameof(Product.Price)), SortOrderOptions.Descending) =>
					allProducts.OrderByDescending(temp => temp.Price).ToList(),

					((nameof(Product.Name)), SortOrderOptions.Ascending) =>
					allProducts.OrderBy(temp => temp.Name).ToList(),

					((nameof(Product.Name)), SortOrderOptions.Descending) =>
					allProducts.OrderBy(temp => temp.Name).ToList(),

					((nameof(Product.Brand)), SortOrderOptions.Descending) =>
					allProducts.OrderBy(temp => temp.Brand).ToList(),

					((nameof(Product.Brand)), SortOrderOptions.Ascending) =>
					allProducts.OrderBy(temp => temp.Brand).ToList(),

					_ => allProducts

				};

				return allProducts;
			}
		}
	}
}
