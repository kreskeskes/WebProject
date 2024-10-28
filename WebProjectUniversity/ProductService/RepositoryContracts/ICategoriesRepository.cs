using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ProductService.RepositoryContracts
{
	public interface ICategoriesRepository
	{
		/// <summary>
		/// Returns all existing product categories as a list
		/// </summary>
		/// <returns></returns>
		Task<List<ProductCategory>?> GetAllProductCategories();


		/// <summary>
		/// Returns a product category based on Id
		/// </summary>
		/// <param name="categoryId">category id to search by</param>
		/// <returns>Category that matches with the condition</returns>
		Task<ProductCategory> GetProductCategoryById(Guid? categoryId);


		/// <summary>
		/// Adds a category to category list
		/// </summary>
		/// <param name="product">ProductCategory to be added </param>
		/// <returns>Added category</returns>
		Task<ProductCategory> AddProductCategory(ProductCategory productCategory);

		/// <summary>
		/// Deletes a product category based on Id
		/// </summary>
		/// <param name="productCategoryId">category Id of the category to be deleted</param>
		/// <returns>true if deletion was successful</returns>
		Task<bool> DeleteProductCategoryBytId(Guid? productCategoryId);


		/// <summary>
		/// Updates the specified productCategory object
		/// </summary>
		/// <param name="productCategory">productCategory object to be updated</param>
		/// <returns>Updated product category</returns>
		Task<ProductCategory> UpdateProductCategory(ProductCategory productCategory);
	}
}
