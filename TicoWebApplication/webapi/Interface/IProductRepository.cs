using webapi.DTOs.Product;
using webapi.Helpers.QueryProduct;
using webapi.Models;

namespace webapi.Interface
{
    public interface IProductRepository
    {
        Task<List<Product>> GetAllProductsAsync(QueryObjectProduct query);
        Task<Product?> GetAllProductsByIdAsync(int id);
        Task<Product?> CreateProducts(Product product);
        Task<Product?> UpdateProducts(int id, UpdateProductDTO updateProduct);
        Task<Product?> DeleteProducts(int id);
    }
}
