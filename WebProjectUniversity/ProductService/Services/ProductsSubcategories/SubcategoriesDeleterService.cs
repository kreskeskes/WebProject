using ProductService.ServiceContracts.IProductsSubcategories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.Services.ProductsCategories.ProductSubcategories
{
	public class SubcategoriesDeleterService : ISubcategoriesDeleterService
	{
		public Task<bool> DeleteProductSubcategory(Guid? subcategoryId)
		{
			throw new NotImplementedException();
		}
	}
}
