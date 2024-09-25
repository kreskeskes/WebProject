using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebProjectUniversity.Core.ServiceContracts.IProducts
{
	public interface IProductsDeleterService
	{
		/// <summary>
		/// Deletes a product with the specified Id
		/// </summary>
		/// <param name="productId">ProductId of the products that has to be deleted</param>
		/// <returns>true if deletion is successful</returns>
		Task<bool> DeleteProduct(Guid? productId);
	}
}
