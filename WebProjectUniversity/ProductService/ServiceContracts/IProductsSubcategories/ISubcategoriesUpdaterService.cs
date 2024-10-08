using ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProductsSubcategories
{
    public interface ISubcategoriesUpdaterService
	{/// <summary>
	 /// 
	 /// </summary>
	 /// <param name="productUpdateRequest"></param>
	 /// <returns></returns>
		Task<ProductResponse> UpdateProductSubcategory(ProductUpdateRequest productUpdateRequest);
	}
}
