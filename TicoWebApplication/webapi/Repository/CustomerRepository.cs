using webapi.DTOs.Customer;
using webapi.Helpers.QueryCustomer;
using webapi.Interface;
using webapi.Models;

namespace webapi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public Task<Product?> CreateCustomers(Customer customer)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> DeleteCustomers(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Product>> GetAllCustomersAsync(QueryObjectCustomer query)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetAllCustomersByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> UpdateCustomers(int id, UpdateCustomerDTO updateCustomer)
        {
            throw new NotImplementedException();
        }
    }
}
