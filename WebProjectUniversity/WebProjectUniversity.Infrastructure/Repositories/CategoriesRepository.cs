﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebProjectUniversity.Core.Domain.Entities;
using WebProjectUniversity.Core.Domain.RepositoryContracts;
using WebProjectUniversity.Infrastructure.AppDbContext;

namespace WebProjectUniversity.Infrastructure.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriesRepository(ApplicationDbContext db)
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
            return await _db.ProductCategories.Include(c => c.ProductSubcategories).ToListAsync();
        }

        public async Task<ProductCategory> GetProductCategoryById(Guid? categoryId)
        {
            return await _db.ProductCategories.Include(c => c.ProductSubcategories).FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<ProductCategory> UpdateProductCategory(ProductCategory productCategory)
        {
            ProductCategory matchingCategory = await _db.ProductCategories.FirstOrDefaultAsync(x => x.Id == productCategory.Id);

            if (matchingCategory != null)
            {
                matchingCategory.Name = productCategory.Name;
                matchingCategory.ProductSubcategories = productCategory.ProductSubcategories;
            }

            await _db.SaveChangesAsync();
            return matchingCategory;

        }
    }
}
