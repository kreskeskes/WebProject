using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.ServiceContracts.IProductsCategories
{
    public interface ICategoriesDeleterService
    {
 
        /// <summary>
        /// Deletes a category based on given Id
        /// </summary>
        /// <param name="categoryId">Guid based on which to delete a category</param>
        /// <returns>True if deletion is successful</returns>
        Task<bool> DeleteProductCategory(Guid? categoryId);
    }
}
