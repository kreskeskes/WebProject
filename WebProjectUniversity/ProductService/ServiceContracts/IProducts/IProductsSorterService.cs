
using ProductService.DTO;
using ProductService.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProducts
{
    public interface IProductsSorterService
	{
		/// <summary>
		/// Sorts the given list of products by selected parameter in the chosen order
		/// </summary>
		/// <param name="allProducts">List of products to sort</param>
		/// <param name="sortBy">Parameter to sort by</param>
		/// <param name="sortOrder">ASC or DESC</param>
		/// <returns></returns>
		Task<List<ProductResponse>> GetSortedProducts(List<ProductResponse> allProducts, string sortBy, SortOrderOptions sortOrder);
	}
}
