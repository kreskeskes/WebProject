using Microsoft.EntityFrameworkCore;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities.ProductFolder;
using WebProjectUniversity.Core.Domain.RepositoryContracts;
using WebProjectUniversity.Infrastructure.AppDbContext;

namespace WebProjectUniversity.Infrastructure.Repositories
{
	public class ProductsRepository : IProductsRepository
	{
		private readonly ApplicationDbContext _db;

		public ProductsRepository(ApplicationDbContext db)
		{
			_db = db;
		}
		public async Task<Product> AddProduct(Product product)
		{
			_db.Products.Add(product);
			await _db.SaveChangesAsync();
			return product;
		}

		public async Task<bool> DeleteProductByProductId(Guid? productId)
		{
			_db.Products.RemoveRange(_db.Products.Where(temp => temp.Id == productId));
			int rowsDeleted = await _db.SaveChangesAsync();
			return rowsDeleted > 0;

		}

		public async Task<List<Product>?> GetAllProducts()
		{
			return await _db.Products.Include(p => p.ProductCategories).Include(p => p.ProductSubcategory).ToListAsync();
		}

		public async Task<List<Product>> GetFilteredProducts(Expression<Func<Product, bool>> predicate)
		{
		var products = await _db.Products
		.Where(predicate)
		.ToListAsync();

			return products;
		}

		public async Task<Product>? GetProductByProductId(Guid? productId)
		{
			return await _db.Products.Include(p => p.ProductCategories).Include(p => p.ProductSubcategory).FirstOrDefaultAsync(temp => temp.Id == productId);
		}

		public async Task<Product> UpdateProduct(Product product)
		{
			Product? matchingProduct = await _db.Products.FirstOrDefaultAsync(temp => temp == product);

			if (matchingProduct != null)
			{
				matchingProduct.ProductSubcategoryId = product.ProductSubcategoryId;
				matchingProduct.Brand = product.Brand;
				matchingProduct.ProductCategories = product.ProductCategories;
				matchingProduct.Color = product.Color;
				matchingProduct.Description = product.Description;
				matchingProduct.Sizes = product.Sizes;
				matchingProduct.Price = product.Price;
				matchingProduct.AgeGenderGroup = product.AgeGenderGroup;
				matchingProduct.Name = product.Name;

				await _db.SaveChangesAsync();
				return matchingProduct;
			}
			return matchingProduct;

		}
	}
}
