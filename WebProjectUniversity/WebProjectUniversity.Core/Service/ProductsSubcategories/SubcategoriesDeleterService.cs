using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.ServiceContracts.IProductsSubcategories;

namespace WebProjectUniversity.Core.Service.ProductsSubcategories
{
	public class SubcategoriesDeleterService : ISubcategoriesDeleterService
	{
		public Task<bool> DeleteProductSubcategory(Guid? subcategoryId)
		{
			throw new NotImplementedException();
		}
	}
}
