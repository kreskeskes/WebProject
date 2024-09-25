using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;
using WebProjectUniversity.Core.Domain.RepositoryContracts;

namespace WebProjectUniversity.Infrastructure.Repositories
{
	public class SubcategoriesRepository : ISubcategoriesRepository
	{
		public Task<ProductSubcategory> AddProductSubcategory(ProductSubcategory productSubcategory)
		{
			throw new NotImplementedException();
		}

		public Task<bool> DeleteProductSubcategoryBytId(Guid? productSubcategoryId)
		{
			throw new NotImplementedException();
		}

		public Task<List<ProductSubcategory>?> GetAllProductSubcategories()
		{
			throw new NotImplementedException();
		}

		public Task<ProductSubcategory> GetProductSubcategoryById(Guid subcategoryId)
		{
			throw new NotImplementedException();
		}

		public Task<ProductSubcategory> UpdateProductCategory(ProductSubcategory productSubcategory)
		{
			throw new NotImplementedException();
		}
	}
}
