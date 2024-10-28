using ProductService.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace ProductService.RepositoryContracts
{
    public interface IProductTypesRepository
    {
        /// <summary>
        /// Returns all product ProductTypes as a list
        /// </summary>
        /// <returns></returns>
        Task<List<ProductType>?> GetAllProductTypes();


        /// <summary>
        /// Returns a product ProductType based on Id
        /// </summary>
        /// <param name="ProductTypeId"></param>
        /// <returns>ProductType that matches with the condition</returns>
        Task<ProductType> GetProductTypeById(Guid ProductTypeId);


        /// <summary>
        /// Adds a ProductType to category list
        /// </summary>
        /// <param name="ProductType">ProductType to be added </param>
        /// <returns>Added ProductType</returns>
        Task<ProductType> AddProductType(ProductType ProductType);

        /// <summary>
        /// Deletes a product ProductType based on Id
        /// </summary>
        /// <param name="productCategoryId">ProductType Id of the ProductType to be deleted</param>
        /// <returns>true if deletion was successful</returns>
        Task<bool> DeleteProductTypeById(Guid? productTypeId);


        /// <summary>
        /// Updates the specified ProductType object
        /// </summary>
        /// <param name="ProductType">ProductType object to be updated</param>
        /// <returns>Updated product ProductType</returns>
        Task<ProductType> UpdateProductType(ProductType ProductType);

    }
}
