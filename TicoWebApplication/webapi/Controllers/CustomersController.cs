using Microsoft.AspNetCore.Mvc;
using webapi.DTOs.Product;
using webapi.Helpers;
using webapi.Interface;
using webapi.Wrappers;
using webapi.Helpers.QueryCustomer;
using webapi.Mapper;
using webapi.DTOs.Customer;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _repository;
        public CustomerController(ICustomerRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPage(int pageIndex, int pageSize, [FromQuery] QueryObjectCustomer queryObject)
        {
            try
            {
                var list = await _repository.GetAllCustomersAsync(queryObject);
                var data = list.Select(x => x.ToCustomerInfomationDTO()).ToList();
                var totalItem = data.Count();
                var totalPage = (int)Math.Ceiling((double)totalItem / pageSize);
                //Get Page
                var pagedData = PaginationHelper.GetPagedData(data, pageIndex, pageSize);
                var response = new PagedResponse<CustomerInfomationDTO>(data, pageIndex, pageSize, totalPage);
                return Ok(response);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPageByID([FromRoute] int id)
        {
            try
            {
                var list = await _repository.GetAllCustomersByIdAsync(id);
                if (list == null) return NotFound();
                return Ok(list);
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
        [HttpPost]
        public async Task<IActionResult> CreateProductsInfomation([FromBody] CreateCustomerDTO createData)
        {
            try
            {
                var query = createData.ToCreateCustomerDTO();
                await _repository.CreateCustomers(query);
                return CreatedAtAction(nameof(GetPageByID), new { id = query.id }, query.ToCustomerInfomationDTO());
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducts([FromRoute] int id, UpdateCustomerDTO updateData)
        {
            try
            {
                var query = await _repository.UpdateCustomers(id, updateData);
                if (query == null) return NotFound();
                return Ok(query.ToCustomerInfomationDTO());
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducts([FromRoute] int id)
        {
            try
            {
                var query = await _repository.DeleteCustomers(id);
                if (query == null) return NotFound();
                return NoContent();
            }
            catch (Exception ex)
            {
                var error = new Response<string>
                {
                    Succeeded = false,
                    Message = "An error data.",
                    Errors = new[] { ex.Message }
                };
                return StatusCode(500, error);
            }
        }
    }
}
