using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProductsProductTypes
{
	public interface IProductTypesDeleterService
	{
		/// <summary>
		/// Deletes the specified ProductType based on ProductTypeId
		/// </summary>
		/// <param name="ProductTypeId">ProductTypeId of the ProductType that has to be deleted</param>
		/// <returns>True if deletion is successful</returns>
		Task<bool> DeleteProductType(Guid ProductTypeId);
	}
}
