using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;
using WebProjectUniversity.Core.Domain.RepositoryContracts;
using WebProjectUniversity.Core.DTO;
using WebProjectUniversity.Core.ServiceContracts.ICategories;

namespace WebProjectUniversity.Core.Service.Products
{
	public class ProductsAdderService : IProductsAdderService
	{

		private readonly IProductsRepository _productsRepository;

		public ProductsAdderService(IProductsRepository productsRepository)
		{
			_productsRepository = productsRepository;
		}

		public async Task<ProductResponse> AddProduct(ProductAddRequest productAddRequest)
		{
			if (productAddRequest == null)
			{
				throw new ArgumentNullException();
			}
			else
			{
				if (productAddRequest.Name==null)
				{
					throw new ArgumentException();
				}
				Product product = productAddRequest.ToProduct();
				product.Id = Guid.NewGuid();

				await _productsRepository.AddProduct(product);

				return product.ToProductResponse();
			}

		}
	}
}
