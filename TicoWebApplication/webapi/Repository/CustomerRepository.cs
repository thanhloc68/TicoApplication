using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.DTOs.Customer;
using webapi.Helpers.QueryCustomer;
using webapi.Interface;
using webapi.Models;

namespace webapi.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CustomerRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer?> CreateCustomers(Customer customer)
        {
            await _dbContext.AddAsync(customer);
            await _dbContext.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> DeleteCustomers(int id)
        {
            var query = await _dbContext.Customer.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            _dbContext.Customer.Remove(query);
            await _dbContext.SaveChangesAsync();
            return query;
        }

        public async Task<List<Customer>> GetAllCustomersAsync(QueryObjectCustomer query)
        {
            var list = _dbContext.Customer.AsNoTracking().AsQueryable();
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

        public async Task<Customer?> GetAllCustomersByIdAsync(int id)
        {
            var query = await _dbContext.Customer.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            return query;
        }

        public async Task<Customer?> UpdateCustomers(int id, UpdateCustomerDTO updateCustomer)
        {
            var query = await _dbContext.Customer.FirstOrDefaultAsync(x => x.id == id);
            if (query == null) return null;
            query.name = updateCustomer.name;
            query.shortcutName = updateCustomer.shortcutName;
            await _dbContext.SaveChangesAsync();
            return query;
        }
    }
}
