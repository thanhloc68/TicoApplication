using webapi.DTOs.Customer;
using webapi.Helpers.QueryCustomer;
using webapi.Models;

namespace webapi.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Product>> GetAllCustomersAsync(QueryObjectCustomer query);
        Task<Product?> GetAllCustomersByIdAsync(int id);
        Task<Product?> CreateCustomers(Customer customer);
        Task<Product?> UpdateCustomers(int id, UpdateCustomerDTO updateCustomer);
        Task<Product?> DeleteCustomers(int id);
    }
}
