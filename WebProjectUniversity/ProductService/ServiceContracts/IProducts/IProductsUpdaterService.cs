using ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.ServiceContracts.IProducts
{
    public interface IProductsUpdaterService
	{
		/// <summary>
		/// Updates specified product based on product Id
		/// </summary>
		/// <param name="productUpdateRequest">product details to be updated, including product Id</param>
		/// <returns>an object of ProductResponse after updation</returns>
		Task<ProductResponse> UpdateProduct(ProductUpdateRequest productUpdateRequest);
	}
}
