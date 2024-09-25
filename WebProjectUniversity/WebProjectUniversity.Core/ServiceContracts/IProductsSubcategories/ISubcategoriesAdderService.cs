using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.DTO;

namespace WebProjectUniversity.Core.ServiceContracts.IProductsSubcategories
{
	public interface ISubcategoriesAdderService
	{
		/// <summary>
		/// Adds a subcategory to the list of subcategories
		/// </summary>
		/// <param name="productSubcategoryAddRequest">The subcategory to add</param>
		/// <returns>The just added subcategory</returns>
		Task<ProductSubcategoryResponse> AddProductSubcategory(ProductSubcategoryAddRequest productSubcategoryAddRequest);
	}
}
