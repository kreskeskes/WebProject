using ProductService.DTO;
using ProductService.ServiceContracts.IProductsSubcategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.Services.ProductsCategories.ProductSubcategories
{
	public class SubcategoriesGetterService : ISubcategoriesGetterService
	{
		public Task<List<ProductSubcategoryResponse>> GetAlProductlSubcategories()
		{
			throw new NotImplementedException();
		}

		public Task<ProductSubcategoryResponse> GetProductSubcategoryResponseById(Guid id)
		{
			throw new NotImplementedException();
		}
	}
}
