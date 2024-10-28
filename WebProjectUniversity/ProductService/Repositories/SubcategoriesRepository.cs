using Microsoft.EntityFrameworkCore;
using ProductService.Data;
using ProductService.DTO;
using ProductService.RepositoryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProductService.Repositories
{
    public class ProductTypesRepository : IProductTypesRepository
    {
        private readonly ProductDbContext _db;
        public ProductTypesRepository(ProductDbContext db)
        {
            _db = db;
        }
        public async Task<ProductType> AddProductType(ProductType ProductType)
        {
            _db.ProductTypes.Add(ProductType);
            await _db.SaveChangesAsync();
            return ProductType;
        }

        public async Task<bool> DeleteProductTypeById(Guid? productTypeId)
        {
            _db.ProductTypes.RemoveRange(_db.ProductTypes.Where(x => x.Id == productTypeId));
            int deletedRows = await _db.SaveChangesAsync();
            return deletedRows > 0;
        }

        public async Task<List<ProductType>?> GetAllProductTypes()
        {
            return await _db.ProductTypes.ToListAsync();

        }

      

        public async Task<ProductType> GetProductTypeById(Guid ProductTypeId)
        {
            return await _db.ProductTypes.FirstOrDefaultAsync(x => x.Id == ProductTypeId);

        }

        public async Task<ProductType> UpdateProductType(ProductType ProductType)
        {
            ProductType matchingProductType = await _db.ProductTypes.FirstOrDefaultAsync(x => x.Id == ProductType.Id);

            if (matchingProductType != null)
            {
                matchingProductType.Name = ProductType.Name;
                matchingProductType.Products = matchingProductType.Products;

                await _db.SaveChangesAsync();
                return matchingProductType;
            }
            else
            {
                return matchingProductType;
            }
        }
    }
}
