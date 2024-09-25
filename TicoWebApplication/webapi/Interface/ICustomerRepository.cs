using webapi.DTOs.Customer;
using webapi.Helpers.QueryCustomer;
using webapi.Models;

namespace webapi.Interface
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomersAsync(QueryObjectCustomer query);
        Task<Customer?> GetAllCustomersByIdAsync(int id);
        Task<Customer?> CreateCustomers(Customer customer);
        Task<Customer?> UpdateCustomers(int id, UpdateCustomerDTO updateCustomer);
        Task<Customer?> DeleteCustomers(int id);
    }
}
