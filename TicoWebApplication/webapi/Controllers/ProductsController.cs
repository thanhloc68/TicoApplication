using Microsoft.AspNetCore.Mvc;
using webapi.DTOs.Product;
using webapi.DTOs.StruckInfo;
using webapi.Helpers;
using webapi.Helpers.QueryProduct;
using webapi.Interface;
using webapi.Mapper;
using webapi.Wrappers;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repository;
        public ProductsController(IProductRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPage(int pageIndex, int pageSize, [FromQuery] QueryObjectProduct queryObject)
        {
            try
            {
                var list = await _repository.GetAllProductsAsync(queryObject);
                var data = list.Select(x => x.ToProductInfomationDTO()).ToList();
                var totalItem = data.Count();
                var totalPage = (int)Math.Ceiling((double)totalItem / pageSize);
                //Get Page
                var pagedData = PaginationHelper.GetPagedData(data, pageIndex, pageSize);
                var response = new PagedResponse<ProductInfomationDTO>(data, pageIndex, pageSize, totalPage);
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
                var list = await _repository.GetAllProductsByIdAsync(id);
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
        public async Task<IActionResult> CreateProductsInfomation([FromBody] CreateProductDTO createData)
        {
            try
            {
                var query = createData.ToCreateProduct();
                await _repository.CreateProducts(query);
                return CreatedAtAction(nameof(GetPageByID), new { id = query.id }, query.ToProductInfomationDTO());
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
        public async Task<IActionResult> UpdateProducts([FromRoute] int id, UpdateProductDTO updateData)
        {
            try
            {
                var query = await _repository.UpdateProducts(id, updateData);
                if (query == null) return NotFound();
                return Ok(query.ToProductInfomationDTO());
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
                var query = await _repository.DeleteProducts(id);
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
