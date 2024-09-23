using webapi.DTOs.Product;
using webapi.Helpers.QueryProduct;
using webapi.Interface;
using webapi.Models;

namespace webapi.Repository
{
    public class ProductRepository : IProductRepository
    {
        public Task<Product?> CreateProducts(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> DeleteProducts(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllProductsAsync(QueryObjectProduct query)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetAllProductsByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> UpdateProducts(int id, UpdateProductDTO updateProduct)
        {
            throw new NotImplementedException();
        }
    }
}
