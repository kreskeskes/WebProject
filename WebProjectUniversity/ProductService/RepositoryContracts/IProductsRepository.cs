
using ProductService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;



namespace ProductService.RepositoryContracts
{
    public interface IProductsRepository
    {
        /// <summary>
        /// Returns all existing products as a list
        /// </summary>
        /// <returns>Products as a list of Product type</returns>
        Task<List<Product>?> GetAllProducts();


        /// <summary>
        /// Returns product based on specified Guid
        /// </summary>
        /// <param name="productId">Guid based on which to retrieve a product</param>
        /// <returns>Found instance of Product</returns>
        Task<Product>? GetProductByProductId(Guid? productId);



        /// <summary>
        /// All product object that match with the given expression
        /// </summary>
        /// <param name="predicate">LINQ expression to check</param>
        /// <returns>A list of products that match with the given condition</returns>
        Task<List<Product>> GetFilteredProducts(Expression<Func<Product, bool>> predicate);


        /// <summary>
        /// Adds a product to product list
        /// </summary>
        /// <param name="product">a product to be added</param>
        /// <returns>Returns the same product details, including product details</returns>
        Task<Product> AddProduct(Product product);

        /// <summary>
        /// Deletes a product with the specified Id
        /// </summary>
        /// <param name="productId">ProductId of the products that has to be deleted</param>
        /// <returns>true if deletion is successful</returns>
        Task<bool> DeleteProductByProductId(Guid? productId);


        /// <summary>
        /// Updates specified product based on product Id
        /// </summary>
        /// <param name="product">product details to be updated, including product Id</param>
        /// <returns>an object of Product after updation</returns>
        Task<Product> UpdateProduct(Product product);
    }
}
