using ProductService.DTO;
using ProductService.RepositoryContracts;
using ProductService.ServiceContracts.IProductsProductTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.Services.ProductsCategories.ProductTypes
{
	public class ProductTypesDeleterService : IProductTypesDeleterService
	{
		private readonly IProductTypesRepository _ProductTypesRepository;

		public ProductTypesDeleterService(IProductTypesRepository ProductTypesRepository)
		{
			_ProductTypesRepository = ProductTypesRepository;
		}

		public async Task<bool> DeleteProductType(Guid ProductTypeId)
		{
			if (ProductTypeId == Guid.Empty)
			{
				throw new ArgumentNullException();
			}
			ProductType foundProductType = await _ProductTypesRepository.GetProductTypeById(ProductTypeId);
			if (foundProductType == null)
			{
				return false;
			}
			return await _ProductTypesRepository.DeleteProductTypeById(foundProductType.Id);

		}
	}
}
