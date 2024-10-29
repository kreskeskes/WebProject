using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace ProductService.Repositories
{
	public class ProductsRepository : IProductsRepository
	{
		private readonly ProductDbContext _db;

		public ProductsRepository(ProductDbContext db)
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
			return await _db.Products.Include(p => p.ProductType).Include(p => p.Categories).ToListAsync();
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
			return await _db.Products.Include(p => p.ProductType).Include(p => p.Categories).FirstOrDefaultAsync(temp => temp.Id == productId);
		}

		public async Task<Product> UpdateProduct(Product product)
		{
			Product? matchingProduct = await _db.Products.FirstOrDefaultAsync(temp => temp == product);

			if (matchingProduct != null)
			{
				matchingProduct.ProductType = product.ProductType;
				matchingProduct.Brand = product.Brand;
				matchingProduct.Categories = product.Categories;
				matchingProduct.Colors = product.Colors;
				matchingProduct.Description = product.Description;
				matchingProduct.Sizes = product.Sizes;
				matchingProduct.Price = product.Price;
				matchingProduct.AgeGenderGroup = product.AgeGenderGroup;
				matchingProduct.Name = product.Name;
				matchingProduct.Materials = product.Materials;
				matchingProduct.Styles= product.Styles;
				matchingProduct.Length= product.Length;
				matchingProduct.ProductTypeId = product.ProductTypeId;

				await _db.SaveChangesAsync();
				return matchingProduct;
			}
			return matchingProduct;

		}
	}
}
