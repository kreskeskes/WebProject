using ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProductsSubcategories
{
	public interface ISubcategoriesGetterService
	{

		/// <summary>
		/// Returns a list of subcategories
		/// </summary>
		/// <returns></returns>
		Task<List<ProductSubcategoryResponse>> GetAlProductlSubcategories();


		/// <summary>
		/// Returns a subcategory based on given Id
		/// </summary>
		/// <param name="id">Guid based on which to retrieve subcategory</param>
		/// <returns></returns>
		Task<ProductSubcategoryResponse> GetProductSubcategoryResponseById(Guid id);
	}
}
