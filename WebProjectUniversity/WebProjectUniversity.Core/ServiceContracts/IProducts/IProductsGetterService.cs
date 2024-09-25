using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.DTO;

namespace WebProjectUniversity.Core.ServiceContracts.IProducts
{
    public interface IProductsGetterService
	{
		/// <summary>
		/// Returns all existing products as a list
		/// </summary>
		/// <returns>Products as a list of ProductResponse type</returns>
		Task<List<ProductResponse>?> GetAllProducts();


		/// <summary>
		/// Returns product based on specified Guid
		/// </summary>
		/// <param name="productId">Guid based on which to retrieve a product</param>
		/// <returns>Found instance of ProductResponse</returns>
		Task<ProductResponse> GetProductByProductId(Guid productId);



		/// <summary>
		/// Returns a list of ProductResponse based on searchyBy property and searchString string
		/// </summary>
		/// <param name="searchBy">Parameter name to search based on</param>
		/// <param name="searchString">The literal string value based on which to serach</param>
		/// <returns></returns>
		Task<List<ProductResponse>> GetFilteredProducts(string? searchBy, string? searchString);
	}
}
