using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProductsSubcategories
{
	public interface ISubcategoriesDeleterService
	{
		/// <summary>
		/// Deletes the specified subcategory based on subcategoryId
		/// </summary>
		/// <param name="subcategoryId">subcategoryId of the subcategory that has to be deleted</param>
		/// <returns>True if deletion is successful</returns>
		Task<bool> DeleteProductSubcategory(Guid? subcategoryId);
	}
}
