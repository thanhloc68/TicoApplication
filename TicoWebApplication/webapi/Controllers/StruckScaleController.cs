using Microsoft.AspNetCore.Mvc;
using webapi.DTOs.StruckInfo;
using webapi.DTOs.StruckScale;
using webapi.Helpers;
using webapi.Helpers.QueryStruckScale;
using webapi.Interface;
using webapi.Mapper;
using webapi.Wrappers;

namespace webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StruckScaleController : ControllerBase
    {
        private readonly IStruckScaleRepository _repository;
        public StruckScaleController(IStruckScaleRepository repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllPage(int pageIndex, int pageSize, [FromQuery] QueryObjectStruckScale queryObject)
        {
            try
            {
                var list = await _repository.GetAllStruckScaleAsync(queryObject);
                var data = list.Select(x => x.ToStruckScaleDTO()).ToList();
                var totalItem = data.Count();
                var totalPage = (int)Math.Ceiling((double)totalItem / pageSize);
                //Get Page
                var pagedData = PaginationHelper.GetPagedData(data, pageIndex, pageSize);
                var response = new PagedResponse<StruckScaleDTO>(data, pageIndex, pageSize, totalPage);
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
                var list = await _repository.GetAllStruckScaleByIdAsync(id);
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
        public async Task<IActionResult> CreateStruckScale([FromBody] CreateStruckScaleDTO createData)
        {
            try
            {
                var query = createData.ToCreateStruckScales();
                await _repository.CreateStruckScale(query);
                return CreatedAtAction(nameof(GetPageByID), new { id = query.id }, query.ToStruckScaleDTO());
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
        public async Task<IActionResult> UpdateStruckScale([FromRoute] int id, UpdateStruckScaleDTO updatekInfomation)
        {
            try
            {
                var query = await _repository.UpdateStruckScale(id, updatekInfomation);
                if (query == null) return NotFound();
                return Ok(query.ToStruckScaleDTO());
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
        public async Task<IActionResult> DeleteStruckScale([FromRoute] int id)
        {
            try
            {
                var query = await _repository.DeleteStruckScale(id);
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
