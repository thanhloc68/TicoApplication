using webapi.DTOs.Customer;
using webapi.Models;

namespace webapi.Mapper
{
    public static class CustomerMapper
    {
        public static CustomerInfomationDTO ToCustomerInfomationDTO(this Customer customer)
        {
            return new CustomerInfomationDTO
            {
                id = customer.id,
                name = customer.name,
                shortcutName = customer.shortcutName
            };
        }
        public static Customer ToCreateCustomerDTO(this CreateCustomerDTO customer)
        {
            return new Customer
            {
                name = customer.name,
                shortcutName = customer.shortcutName
            };
        }
    }
}
