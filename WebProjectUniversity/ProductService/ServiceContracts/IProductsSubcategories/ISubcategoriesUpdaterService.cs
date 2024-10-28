using ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProductsProductTypes
{
    public interface IProductTypesUpdaterService
	{/// <summary>
	 /// 
	 /// </summary>
	 /// <param name="productUpdateRequest"></param>
	 /// <returns></returns>
		Task<ProductTypeResponse> UpdateProductType(ProductTypeUpdateRequest productTypeUpdateRequest);
	}
}
