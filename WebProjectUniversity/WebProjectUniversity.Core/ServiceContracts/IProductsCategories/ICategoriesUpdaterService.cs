using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.DTO;

namespace WebProjectUniversity.Core.ServiceContracts.IProductsCategories
{
    public interface ICategoriesUpdaterService
    {
        /// <summary>
        /// Updates specified category based on product Id
        /// </summary>
        /// <param name="productCategoryUpdateRequest">product category details to be updated, including category Id</param>
        /// <returns>an object of ProductCategoryResponse after updation</returns>
        Task<ProductCategoryResponse> UpdateProductCategory(ProductCategoryUpdateRequest productCategoryUpdateRequest);
    }
}
