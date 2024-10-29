using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ProductDbContext _db;

        public CategoriesRepository(ProductDbContext db)
        {
            _db = db;
        }
        public async Task<ProductCategory> AddProductCategory(ProductCategory productCategory)
        {
            _db.ProductCategories.Add(productCategory);
            await _db.SaveChangesAsync();
            return productCategory;
        }

        public async Task<bool> DeleteProductCategoryBytId(Guid? productCategoryId)
        {
            _db.ProductCategories.RemoveRange(_db.ProductCategories.Where(x => x.Id == productCategoryId));
            int rowsDeleted = await _db.SaveChangesAsync();
            return rowsDeleted > 0;

        }

        public async Task<List<ProductCategory>?> GetAllProductCategories()
        {
            return await _db.ProductCategories.Include(c => c.Products).Include(c=>c.ProductTypes).ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryById(Guid? categoryId)
        {
            return await _db.ProductCategories.Include(c => c.Products).Include(c => c.ProductTypes).FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<ProductCategory> UpdateProductCategory(ProductCategory productCategory)
        {
            ProductCategory matchingCategory = await _db.ProductCategories.FirstOrDefaultAsync(x => x.Id == productCategory.Id);

            if (matchingCategory != null)
            {
                matchingCategory.Name = productCategory.Name;
                matchingCategory.Products = productCategory.Products;
            }

            await _db.SaveChangesAsync();
            return matchingCategory;

        }
    }
}
