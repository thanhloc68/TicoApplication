using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTOs.Product;
using webapi.Helpers.QueryProduct;
using webapi.Interface;
using webapi.Models;

namespace webapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ProductRepository(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<Product?> CreateProducts(Product product)
        {
            await _dbContext.AddAsync(product);
            await _dbContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product?> DeleteProducts(int id)
        {
            var query = await _dbContext.Product.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            _dbContext.Product.Remove(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }

        public async Task<List<Product>> GetAllProductsAsync(QueryObjectProduct query)
        {
            var list = _dbContext.Product.AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.name)) list = list.Where(x => x.name != null && x.name.Contains(query.name));
            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                list = query.SortBy.ToLower() switch
                {
                    "id" => query.isDecsending ? list.OrderByDescending(x => x.id) : list.OrderBy(x => x.id),
                    _ => list
                };
            }
            var data = await list.ToListAsync();
            return data;
        }

        public async Task<Product?> GetAllProductsByIdAsync(int id)
        {
            var query = await _dbContext.Product.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            return query;
        }

        public async Task<Product?> UpdateProducts(int id, UpdateProductDTO updateProduct)
        {
            var query = await _dbContext.Product.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            query.name = updateProduct.name;
            query.shortcutName = updateProduct.shortcutName;
            query.status = updateProduct.status;
            await _dbContext.SaveChangesAsync();
            return query;
        }
    }
}
