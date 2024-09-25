using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.DTO;
using WebProjectUniversity.Core.ServiceContracts.IProductsSubcategories;

namespace WebProjectUniversity.Core.Service.ProductsCategories
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
