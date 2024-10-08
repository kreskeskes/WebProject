using ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProductsCategories
{
	public interface ICategoriesGetterService
	{
		/// <summary>
		/// Returns all Product Categories as a list
		/// </summary>
		/// <returns></returns>
		Task<List<ProductCategoryResponse>> GetAllProductCategories();


		/// <summary>
		/// Returns a category based on given Id
		/// </summary>
		/// <param name="id">Guid based on which to retrieve category</param>
		/// <returns></returns>
		Task<ProductCategoryResponse> GetProductCategoryById(Guid id);
	}
}
