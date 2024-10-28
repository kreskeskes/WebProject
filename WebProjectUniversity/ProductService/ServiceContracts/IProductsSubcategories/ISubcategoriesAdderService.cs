using ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProductsProductTypes
{
	public interface IProductTypesAdderService
	{
		/// <summary>
		/// Adds a ProductType to the list of ProductTypes
		/// </summary>
		/// <param name="productTypeAddRequest">The ProductType to add</param>
		/// <returns>The just added ProductType</returns>
		Task<ProductTypeResponse> AddProductType(ProductTypeAddRequest productTypeAddRequest);
	}
}
