using ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProductsProductTypes
{
	public interface IProductTypesGetterService
	{

		/// <summary>
		/// Returns a list of ProductTypes
		/// </summary>
		/// <returns></returns>
		Task<List<ProductTypeResponse>> GetAllProductTypes();


		/// <summary>
		/// Returns a ProductType based on given Id
		/// </summary>
		/// <param name="id">Guid based on which to retrieve ProductType</param>
		/// <returns></returns>
		Task<ProductTypeResponse> GetProductTypeResponseById(Guid id);

    }
}
