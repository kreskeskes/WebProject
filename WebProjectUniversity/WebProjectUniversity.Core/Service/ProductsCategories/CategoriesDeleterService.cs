using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities;
using WebProjectUniversity.Core.Domain.RepositoryContracts;
using WebProjectUniversity.Core.ServiceContracts.IProductsCategories;

namespace WebProjectUniversity.Core.Service.ProductsCategories
{
	public class CategoriesDeleterService : ICategoriesDeleterService
	{
		private readonly ICategoriesRepository _categoriesRepository;

		public CategoriesDeleterService(ICategoriesRepository categoriesRepository)
		{
			_categoriesRepository = categoriesRepository;
		}

		public async Task<bool> DeleteProductCategory(Guid? categoryId)
		{
			if (categoryId != null)
			{
				ProductCategory productCategoryRetrieved= await _categoriesRepository.GetProductCategoryById(categoryId.Value);
				if (productCategoryRetrieved != null)
				{
					return await _categoriesRepository.DeleteProductCategoryBytId(productCategoryRetrieved.Id);
				}
				else
				{
					return false;
				}
			}
			else
			{
				throw new ArgumentNullException();
			}
		}
	}
}
