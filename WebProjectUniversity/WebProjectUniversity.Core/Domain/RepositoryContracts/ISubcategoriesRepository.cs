using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;



namespace WebProjectUniversity.Core.Domain.RepositoryContracts
{
	public interface ISubcategoriesRepository
	{
		/// <summary>
		/// Returns all product subcategories as a list
		/// </summary>
		/// <returns></returns>
		Task<List<ProductSubcategory>?> GetAllProductSubcategories();


		/// <summary>
		/// Returns a product subcategory based on Id
		/// </summary>
		/// <param name="subcategoryId"></param>
		/// <returns>Subcategory that matches with the condition</returns>
		Task<ProductSubcategory> GetProductSubcategoryById(Guid subcategoryId);


		/// <summary>
		/// Adds a subcategory to category list
		/// </summary>
		/// <param name="productSubcategory">ProductSubcategory to be added </param>
		/// <returns>Added subcategory</returns>
		Task<ProductSubcategory> AddProductSubcategory(ProductSubcategory productSubcategory);

		/// <summary>
		/// Deletes a product subcategory based on Id
		/// </summary>
		/// <param name="productCategoryId">subcategory Id of the subcategory to be deleted</param>
		/// <returns>true if deletion was successful</returns>
		Task<bool> DeleteProductSubcategoryBytId(Guid? productSubcategoryId);


		/// <summary>
		/// Updates the specified productSubcategory object
		/// </summary>
		/// <param name="productSubcategory">productSubcategory object to be updated</param>
		/// <returns>Updated product subcategory</returns>
		Task<ProductSubcategory> UpdateProductCategory(ProductSubcategory productSubcategory);
	}
}
